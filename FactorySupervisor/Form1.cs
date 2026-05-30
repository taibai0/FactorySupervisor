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
        private readonly IAlarmRuleRepository _alarmRuleRepository = new SqlAlarmRuleRepository();
        private readonly IAlarmStateStore _alarmStateStore = new InMemoryAlarmStateStore();
        private AlarmEvaluationService? _alarmEvaluationService;
        private IAlarmHistoryRepository _alarmHistoryRepository = new SqlAlarmHistoryRepository();
        //接收方法返回的数据
        private Task? _acquisitionTask;
        private Task? _alarmTask;

        private bool _isRunning;
        private DateTimeOffset? _lastUpdateTime;

        //datagridview显示元素
        private readonly BindingList<TagGridRow> _tagRows = new();
        private readonly Dictionary<Guid, TagGridRow> _tagRowMap = new();
        private readonly List<TagGridRow> _allTagRows = new();
        private readonly BindingList<AlarmGridRow> _alarmRows = new();
        private readonly Dictionary<Guid, AlarmGridRow> _alarmRowMap = new();

        //状态读取服务
        private readonly ITagValueCache _cache = new InMemoryTagValueCache();
        private readonly IDeviceRepository _deviceRepo = new SqlDeviceRepository();
        private readonly ITagRepository _tagRepo = new SqlTagRepository();
        private readonly IProtocolClientFactory _factory = new ProtocolClientFactory();
        private readonly ITagHistoryRepository _tagHistoryRepository = new SqlTagHistoryRepository();

        //操作日志记录
        private readonly IAuditLogRepository _auditLogRepository = new SqlAuditLogRepository();
        private readonly IAuthService _authService = new InMemoryAuthService();

        private CancellationTokenSource? _cts;
        private AcquisitionService? _acquisitionService;

        private ConfigForm? _configForm;

        private Guid? _selectedDeviceId;
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
            await LoadDeviceFilterAsync();
            //await LoadDeviceInfoAsync();

            dgvTags.AutoGenerateColumns = true;
            dgvTags.DataSource = _tagRows;

            dgvAlarms.AutoGenerateColumns = true;
            dgvAlarms.DataSource = _alarmRows;

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

            _acquisitionService = new AcquisitionService(_deviceRepo, _tagRepo, _factory, _cache, _tagHistoryRepository);
            _alarmEvaluationService = new AlarmEvaluationService(
                        _alarmRuleRepository,
                        _cache,
                        _alarmStateStore,
                        _alarmHistoryRepository);
            _isRunning = true;
            UpdateStatus();
            await LoadDeviceInfoAsync();

            _acquisitionTask = RunBackgroundAsync(_acquisitionService.RunAsync);
            _alarmTask = RunBackgroundAsync(_alarmEvaluationService.RunAsync);

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
            await LoadDeviceInfoAsync();
            MarkUiTagsStopped();
            await WriteAuditAsync("StopAcquisition", "停止采集服务");
        }

        private void MarkUiTagsStopped()
        {
            foreach (var row in _tagRows)
            {
                row.Quality = "None";
                row.Error = "采集已停止";
                row.Time = "";
            }

            dgvTags.Refresh();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (!_isRunning)
            {
                UpdateStatus();
                return;
            }

            var hasAnyValue = false;

            foreach (var pair in _tagRowMap)
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
            var alarms = _alarmStateStore.GetCurrentAlarms();

            var activeRuleIds = alarms.Select(a => a.RuleId).ToHashSet();

            // 删除已经恢复、不再当前报警列表里的行
            foreach (var ruleId in _alarmRowMap.Keys.ToList())
            {
                if (activeRuleIds.Contains(ruleId)) continue;

                var row = _alarmRowMap[ruleId];
                _alarmRows.Remove(row);
                _alarmRowMap.Remove(ruleId);
            }

            //新增或更新当前报警
            foreach (var alarm in alarms)
            {
                if (!_alarmRowMap.TryGetValue(alarm.RuleId, out var row))
                {
                    row = new AlarmGridRow
                    {
                        RuleId = alarm.RuleId
                    };
                    _alarmRows.Add(row);
                    _alarmRowMap[alarm.RuleId] = row;
                }

                row.RuleName = alarm.RuleName;
                row.Level = alarm.Level.ToString();
                row.State = alarm.State.ToString();
                row.TriggerValue = alarm.TriggerValue?.ToString() ?? "";
                row.TriggerTime = alarm.TriggerTime.ToString("HH:mm:ss");
            }
            dgvAlarms.Refresh();
            ApplyAlarmRowStyle();
        }

        //更新点位采集状态
        private void UpdateStatus()
        {
            var badCount = _tagRows.Count(r => r.Quality == "Bad");
            var lastUpdate = _lastUpdateTime?.ToString("HH:mm:ss") ?? "--";

            toolStripStatusLabelRunState.Text = _isRunning ? "运行中" : "已停止";
            toolStripStatusLabelBadCount.Text = $"Bad：{badCount}";
            toolStripStatusLabelLastUpdate.Text = $"最后刷新时间：{lastUpdate}";

            var alarmCount = _alarmStateStore.GetCurrentAlarms().Count();

            lblCardRunStateValue.Text = _isRunning ? "运行中" : "已停止";
            lblCardAlarmCountValue.Text = $"{alarmCount}条";

            lblCardRunStateValue.ForeColor = _isRunning
                ? Color.ForestGreen
                : Color.DimGray;

            lblCardAlarmCountValue.ForeColor = alarmCount > 0
                ? Color.Firebrick
                : Color.ForestGreen;

            lblCardCommQualityValue.Text = !_isRunning
                ? "未启动"
               : badCount > 0 ? "异常" : "正常";


            lblCardCommQualityValue.ForeColor = lblCardCommQualityValue.Text == "异常"
                ? Color.Firebrick
                : Color.ForestGreen;

            //TODO 后续修改
            lblCardDbStateValue.Text = "已连接";
            lblCardDbStateValue.ForeColor = Color.ForestGreen;
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

            _allTagRows.Clear();

            foreach (var device in devices)
            {
                var tags = await _tagRepo.GetByDeviceAsync(device.Id);

                foreach (var tag in tags)
                {
                    var row = new TagGridRow
                    {
                        TagId = tag.Id,
                        DeviceId = device.Id,
                        DeviceName = device.Name,
                        Name = tag.Name,
                        Address = tag.Address,
                        Value = "--",
                        Quality = "None",
                        Time = "",
                        Error = ""
                    };
                    _allTagRows.Add(row);
                }
            }
            ApplyTagFilter();
            ConfigureTagGrid();
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
                        _cts?.Cancel();
                        toolStripStatusLabelRunState.Text = $"错误：{ex.Message}";
                    });
                }
            });
        }

        //审计辅助方法
        private async Task WriteAuditAsync(string action, string detail)
        {
            var user = _authService.CurrentUser;

            if (user == null) return;

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

            var user = _authService.CurrentUser;
            toolStripStatusLabelUser.Text = user is null
                ? "未登录"
        : $"{user.DisplayName} ({user.Role})";
        }


        //查询历史数据
        private void btnOpenHistory_Click(object sender, EventArgs e)
        {
            using var form = new HistoryForm();
            form.ShowDialog(this);
        }


        //获取当前设备信息
        private async Task LoadDeviceInfoAsync()
        {
            var devices = await _deviceRepo.GetEnableDevicesAsync();

            if (devices.Count == 0)
            {
                lblDeviceName.Text = "设备名称： --";
                lblProtocol.Text = "协议：--";
                lblComPort.Text = "串口：--";

            }
            var device = devices[0];

            lblDeviceName.Text = $"设备名称：{device.Name}";
            lblProtocol.Text = $"协议：{device.ProtocolType}";

            lblComPort.Text = $"串口：{device.ComPort ?? "--"}";
        }

        //给报警表加颜色
        private void ApplyAlarmRowStyle()
        {
            foreach (DataGridViewRow gridRow in dgvAlarms.Rows)
            {
                var level = gridRow.Cells["Level"]?.Value?.ToString();

                if (level == "Critical")
                {
                    gridRow.DefaultCellStyle.BackColor = Color.MistyRose;
                    gridRow.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else if (level == "High")
                {
                    gridRow.DefaultCellStyle.BackColor = Color.LemonChiffon;
                    gridRow.DefaultCellStyle.ForeColor = Color.DarkOrange;
                }
                else
                {
                    gridRow.DefaultCellStyle.BackColor = Color.White;
                    gridRow.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        //打开配置窗口
        private void 配置管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_configForm is not null && !_configForm.IsDisposed)
            {
                _configForm.Activate();
                return;
            }

            _configForm = new ConfigForm(_authService, _auditLogRepository,_factory,_tagRepo);

            _configForm.ConfigSaved += ConfigForm_ConfigSaved;

            //窗口关闭后，把引用清空，方便下次重新打开
            _configForm.FormClosed += (_, _) =>
            {
                _configForm = null;
            };

            //不阻塞主窗口
            _configForm.Show();
        }

        /// <summary>
        /// 配置更新后点位表跟着更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ConfigForm_ConfigSaved(object? sender, EventArgs e)
        {
            var oldSelectedDevicedId = _selectedDeviceId;

            await LoadDeviceFilterAsync();

            if(oldSelectedDevicedId is not null)
            {
                for(var i=0;i<toolStripCboDeviceFilter.ComboBox.Items.Count;i++)
                {
                    if (toolStripCboDeviceFilter.ComboBox.Items[i] is DeviceFilterItem item &&
                        item.DeviceId== oldSelectedDevicedId)
                    {
                        toolStripCboDeviceFilter.ComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }

            await InitTagRowsAsync();

            dgvTags.Refresh();

            if (_isRunning)
            {
                MessageBox.Show("点位配置已刷新，采集服务将在下一轮循环使用最新配置。");
            }
        }

        //关闭主窗口的同时关闭其他窗口
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_configForm is not null && !_configForm.IsDisposed)
            {
                _configForm.Close();
            }
        }

        private void ConfigureTagGrid()
        {
            if (dgvTags.Columns.Contains("DeviceId"))
            {
                dgvTags.Columns["DeviceId"].Visible = false;
            }

            if (dgvTags.Columns.Contains("DeviceName"))
            {
                dgvTags.Columns["DeviceName"].HeaderText = "设备";
            }

            if (dgvTags.Columns.Contains("Name"))
            {
                dgvTags.Columns["Name"].HeaderText = "点位";
            }

            if (dgvTags.Columns.Contains("Address"))
            {
                dgvTags.Columns["Address"].HeaderText = "地址";
            }

            if (dgvTags.Columns.Contains("Value"))
            {
                dgvTags.Columns["Value"].HeaderText = "值";
            }

            if (dgvTags.Columns.Contains("Quality"))
            {
                dgvTags.Columns["Quality"].HeaderText = "质量";
            }

            if (dgvTags.Columns.Contains("Time"))
            {
                dgvTags.Columns["Time"].HeaderText = "时间";
            }

            if (dgvTags.Columns.Contains("Error"))
            {
                dgvTags.Columns["Error"].HeaderText = "错误";
            }
        }

        private void toolStripCboDeviceFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(toolStripCboDeviceFilter.ComboBox.SelectedItem is not DeviceFilterItem item)
            {
                return;
            }

            _selectedDeviceId = item.DeviceId;
            ApplyTagFilter();
        }

        /// <summary>
        /// 用于决定显示哪些点位的信息
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ApplyTagFilter()
        {
            _tagRows.Clear();
            _tagRowMap.Clear();

            var rows = _selectedDeviceId is null
                ? _allTagRows
                : _allTagRows.Where(r => r.DeviceId == _selectedDeviceId.Value).ToList();

            foreach(var row in rows )
            {
                _tagRows.Add(row);
                _tagRowMap[row.TagId] = row;
            }

            dgvTags.Refresh();
        }

        /// <summary>
        /// 下拉选项模型
        /// </summary>
        private sealed class DeviceFilterItem
        {
            public Guid? DeviceId { get; init; }

            public string Name { get; init; } = "";

            public override string ToString()
            {
                return Name;
            }
        }

        
        /// <summary>
        /// 加载当前可选的设备选项
        /// </summary>
        /// <returns></returns>
        private async Task LoadDeviceFilterAsync()
        {
            var devices = await _deviceRepo.GetEnableDevicesAsync();

            var items = new List<DeviceFilterItem>
            {
                new DeviceFilterItem
                {
                    DeviceId=null,
                    Name="全部设备"
                }
            };

            items.AddRange(devices.Select(d => new DeviceFilterItem
            {
                DeviceId = d.Id,
                Name = d.Name
            }));

            toolStripCboDeviceFilter.SelectedIndexChanged -= toolStripCboDeviceFilter_SelectedIndexChanged;

            toolStripCboDeviceFilter.ComboBox.DataSource = items;
            toolStripCboDeviceFilter.ComboBox.DisplayMember = nameof(DeviceFilterItem.Name);

            toolStripCboDeviceFilter.SelectedIndexChanged+= toolStripCboDeviceFilter_SelectedIndexChanged;

            toolStripCboDeviceFilter.ComboBox.SelectedIndex = 0;
            _selectedDeviceId = null;
        }
    }

   
}
