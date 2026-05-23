using FactorySupervisor.src.Application.Services;
using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using FactorySupervisor.src.Infrastructure.Auth;
using FactorySupervisor.src.Infrastructure.Caching;
using FactorySupervisor.src.Infrastructure.Data;
using FactorySupervisor.src.Infrastructure.Protocol;
using FactorySupervisor.src.Infrastructure.Repositories;
using FactorySupervisor.src.UI.WinForms;
using System.ComponentModel;

namespace FactorySupervisor
{
    public partial class Form1 : Form
    {
        //报警服务
        private readonly IAlarmRuleRepository _alarmRuleRepository=new InMemoryAlarmRuleRepository();
        private readonly IAlarmStateStore _alarmStateStore=new InMemoryAlarmStateStore();
        private AlarmEvaluationService? _alarmEvaluationService;
        private IAlarmHistoryRepository _alarmHistoryRepository = new SqlAlarmHistoryRepository();
        //接收方法返回的数据
        private Task? _acquisitionTask;
        private Task? _alarmTask;

        private bool _isRunning;
        private DateTimeOffset? _lastUpdateTime;

        //datagridview显示元素
        private readonly BindingList<TagGridRow> _rows = new();
        private readonly Dictionary<Guid, TagGridRow> _rowMap = new();

        //状态读取服务
        private readonly ITagValueCache _cache = new InMemoryTagValueCache();
        private readonly IDeviceRepository _deviceRepo = new InMemoryDeviceRepository();
        private readonly ITagRepository _tagRepo = new InMemoryTagRepository();
        private readonly IProtocolClientFactory _factory = new ProtocolClientFactory();
        private readonly ITagHistoryRepository _tagHistoryRepository = new SqlTagHistoryRepository();

        //操作日志记录
        private readonly IAuditLogRepository _auditLogRepository = new SqlAuditLogRepository();
        private readonly IAuthService _authService = new InMemoryAuthService();

        private CancellationTokenSource? _cts;
        private AcquisitionService? _acquisitionService;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await SqlDatabaseInitializer.InitializeAsync();

            using var loginFrom = new LoginForm(_authService);
            if (loginFrom.ShowDialog(this) != DialogResult.OK)
            {
                Close();
                return;
            }
            ApplyPermissions();

            dgvTags.AutoGenerateColumns = true;
            dgvTags.DataSource = _rows;

            timerRefresh.Interval = 1000;
            timerRefresh.Start();
            UpdateStatus();
        }  

        private async void start_button_Click(object sender, EventArgs e)
        {
            if (!_authService.HasRole(UserRole.Admin, UserRole.Engineer))
            {
                MessageBox.Show("当前用户没有启动采集的权限");
                return;
            }

            if (_isRunning) return;

            await InitTagRowsAsync();
            

            _cts = new CancellationTokenSource();
          
            _acquisitionService = new AcquisitionService(_deviceRepo, _tagRepo, _factory, _cache,_tagHistoryRepository);
            _alarmEvaluationService = new AlarmEvaluationService(
                        _alarmRuleRepository,
                        _cache,
                        _alarmStateStore,
                        _alarmHistoryRepository);
            _isRunning = true;
            UpdateStatus();

            _acquisitionTask = RunBackgroundAsync(_acquisitionService.RunAsync);
            _alarmTask=RunBackgroundAsync(_alarmEvaluationService.RunAsync);

            await WriteAuditAsync("StartAcquisition", "启动采集服务");
        }

        private async void stop_button_Click(object sender, EventArgs e)
        {
            if (!_authService.HasRole(UserRole.Admin, UserRole.Engineer))
            {
                MessageBox.Show("当前用户没有停止采集的权限");
                return;
            }

            if (!_isRunning) return;

            _cts?.Cancel();
            _isRunning = false;
            UpdateStatus();

            await WriteAuditAsync("StopAcquisition", "停止采集服务");
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            var hasAnyValue = false;

            foreach (var pair in _rowMap)
            {
                var tagId = pair.Key;
                var row = pair.Value;

                if (!_cache.TryGet(tagId, out var value) || value is null)
                {
                    row.Value = "";
                    row.Quality = "None";
                    row.Time = "";
                    row.Error = "";
                    continue;
                }

                hasAnyValue = true;

                row.Value = value.Value?.ToString() ?? "";
                row.Quality = value.Quality.ToString();
                row.Time = value.Timestamp.ToString("HH:mm:ss");
                row.Error = value.Error ?? "";
            }

            if (hasAnyValue)
            {
                _lastUpdateTime = DateTimeOffset.Now;
            }

            UpdateStatus();
            dgvTags.Refresh();
            ApplyRowStyle();
            RefreshAlarmGrid();
        }

        //刷新报警表
        private void RefreshAlarmGrid()
        {
            var rows = _alarmStateStore.GetCurrentAlarms()
                .Select(a => new
                {
                    a.RuleName,
                    Leave = a.Level.ToString(),
                    State = a.State.ToString(),
                    a.TriggerValue,
                    TriggerTime = a.TriggerTime.ToString("HH:mm;ss")
                }).ToList();



            dgvAlarms.DataSource = null;
            dgvAlarms.DataSource = rows;
        }

        private void UpdateStatus()
        {
            var badCount = _rows.Count(r => r.Quality == "Bad");
            var lastUpdate = _lastUpdateTime?.ToString("HH:mm:ss") ?? "--";

            lblStatus.Text = _isRunning
                ? $"Running | Last Update: {lastUpdate} | Bad Count: {badCount}"
                : $"Stopped | Last Update: {lastUpdate} | Bad Count: {badCount}";
        }

        private void ApplyRowStyle()
        {
            foreach (DataGridViewRow gridRow in dgvTags.Rows)
            {
                if (gridRow.DataBoundItem is not TagGridRow row) continue;

                if (row.Quality == "Bad")
                {
                    gridRow.DefaultCellStyle.BackColor = Color.MistyRose;
                    gridRow.DefaultCellStyle.ForeColor = Color.DarkRed;
                    gridRow.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
                    gridRow.DefaultCellStyle.SelectionForeColor = Color.White;
                }
                else if (row.Quality == "Good")
                {
                    gridRow.DefaultCellStyle.BackColor = Color.White;
                    gridRow.DefaultCellStyle.ForeColor = Color.Black;
                    gridRow.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                    gridRow.DefaultCellStyle.SelectionForeColor = Color.White;
                }
                else
                {
                    gridRow.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    gridRow.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        //初始化点位显示元素
        private async Task InitTagRowsAsync()
        {
            var devices = await _deviceRepo.GetEnableDevicesAsync();
            if (devices.Count == 0) return;

            var tags = await _tagRepo.GetByDeviceAsync(devices[0].Id);

            _rows.Clear();
            _rowMap.Clear();

            foreach (var tag in tags)
            {
                var row = new TagGridRow
                {
                    TagId = tag.Id,
                    Name = tag.Name,
                    Address = tag.Address
                };

                _rows.Add(row);
                _rowMap[tag.Id] = row;
            }
        }

        private Task RunBackgroundAsync(Func<CancellationToken, Task> action)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (_cts is null) return;
                    await action(_cts.Token);
                }
                catch (OperationCanceledException)
                {
                    // 正常停止
                }
                catch (Exception ex)
                {
                    BeginInvoke(() =>
                    {
                        _isRunning = false;
                        lblStatus.Text = $"Error: {ex.Message}";
                    });
                }
            });
        }

        //审计辅助方法
        private async Task WriteAuditAsync(string action,string detail)
        {
            var user = _authService.CurrentUser;

            if(user==null) return;

            var log = new AuditLog
            {
                UserId = user.Id,
                Username = user.Username,
                Action = action,
                Detail = detail,
                CreatedAt = DateTimeOffset.Now
            };

            await _auditLogRepository.InsertAsync(log);
        }

        //权限验证
        private void ApplyPermissions()
        {
            //只有admin和engineer可以控制采集服务
            var canControlAcquisition = _authService.HasRole(UserRole.Admin, UserRole.Engineer);

            start_button.Enabled = canControlAcquisition;
            stop_button.Enabled = canControlAcquisition;

            var user=_authService.CurrentUser;
            Text=user is null
                ? "FactorySupervisor"
        :       $"FactorySupervisor - {user.DisplayName} ({user.Role})";
        }
    }
}
