using FactorySupervisor.src.Application.Services;
using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Infrastructure.Caching;
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

        private CancellationTokenSource? _cts;
        private AcquisitionService? _acquisitionService;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvTags.AutoGenerateColumns = true;
            dgvTags.DataSource = _rows;

            timerRefresh.Interval = 1000;
            timerRefresh.Start();
            UpdateStatus();
        }

        private async void start_button_Click(object sender, EventArgs e)
        {
            if (_isRunning) return;

            await InitTagRowsAsync();
            

            _cts = new CancellationTokenSource();
          
            _acquisitionService = new AcquisitionService(_deviceRepo, _tagRepo, _factory, _cache);
            _alarmEvaluationService = new AlarmEvaluationService(
                        _alarmRuleRepository,
                        _cache,
                        _alarmStateStore);
            _isRunning = true;
            UpdateStatus();

            _acquisitionTask = RunBackgroundAsync(_acquisitionService.RunAsync);
            _alarmTask=RunBackgroundAsync(_alarmEvaluationService.RunAsync);
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (!_isRunning) return;

            _cts?.Cancel();
            _isRunning = false;
            UpdateStatus();
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
                    // Normal stop.
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
    }
}
