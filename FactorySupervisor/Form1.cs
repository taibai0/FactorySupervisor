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
using static System.Windows.Forms.AxHost;

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

        /// <summary>
        /// 关键点位卡片的控件集合
        /// </summary>
        private readonly Dictionary<Guid, TagCardControls> _tagCardMap = new();


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
            await RefreshDeviceCardsAsync();
            //await LoadDeviceInfoAsync();

            dgvTags.AutoGenerateColumns = true;
            dgvTags.DataSource = _tagRows;
            await InitTagRowsAsync();

            dgvAlarms.AutoGenerateColumns = true;
            dgvAlarms.DataSource = _alarmRows;
            ConfigureAlarmGrid();

            timerRefresh.Interval = 1000;
            timerRefresh.Start();
            UpdateStatus();
            UpdateAlarmBanner();
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
            MarkUiTagsStopped();
            await WriteAuditAsync("StopAcquisition", "停止采集服务");
            UpdateAlarmBanner();
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

                UpdateTagCard(row);
            }

            if (hasAnyValue)
            {
                _lastUpdateTime = DateTimeOffset.Now;
            }

            UpdateStatus();
            dgvTags.Refresh();
            ApplyRowStyle();
            RefreshAlarmGrid();
            UpdateAlarmBanner();
        }

        /// <summary>
        /// 更新点位卡片显示的值和状态
        /// </summary>
        /// <param name="row"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void UpdateTagCard(TagGridRow row)
        {
           if(!_tagCardMap.TryGetValue(row.TagId,out var card))
            {
                return;
            }

            card.ValueLabel.Text = string.IsNullOrWhiteSpace(row.Value) ? "--" : row.Value;
            card.QualityLabel.Text = row.Quality;
            card.AddressLabel.Text = $"{row.Name} / {row.Address}";

            if (row.Quality == "Good")
            {
                card.Panel.FillColor = Color.White;
                card.Panel.RectColor = Color.FromArgb(80, 160, 255);
                card.ValueLabel.ForeColor = Color.DodgerBlue;
                card.QualityLabel.ForeColor = Color.ForestGreen;
            }
            else if (row.Quality == "Bad")
            {
                card.Panel.FillColor = Color.MistyRose;
                card.Panel.RectColor = Color.Firebrick;
                card.ValueLabel.ForeColor = Color.Firebrick;
                card.QualityLabel.ForeColor = Color.Firebrick;
            }
            else
            {
                card.Panel.FillColor = Color.WhiteSmoke;
                card.Panel.RectColor = Color.LightGray;
                card.ValueLabel.ForeColor = Color.Gray;
                card.QualityLabel.ForeColor = Color.Gray;
            }
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
                var tagRow=_allTagRows.FirstOrDefault(t=>t.TagId == alarm.TagId);

                row.DeviceName = tagRow?.DeviceName ?? "--";
                row.TagName = tagRow?.Name ?? "--";
                row.Address = tagRow?.Address ?? "--";
                row.RuleName = alarm.RuleName;
                row.Level = ToAlarmLevelText(alarm.Level.ToString());
                row.State = ToAlarmStateText(alarm.State.ToString());
                row.TriggerValue = alarm.TriggerValue?.ToString() ?? "";
                row.TriggerTime = alarm.TriggerTime.ToString("HH:mm:ss");
            }
            ConfigureAlarmGrid();
            dgvAlarms.Refresh();
            ApplyAlarmRowStyle();
        }

        private string ToAlarmStateText(string state)
        {
            return state switch
            {
                "Active" => "触发中",
                "Recovered" => "已恢复",
                "Acknowledged" => "已确认",
                _ => state
            };
        }

        private string ToAlarmLevelText(string level)
        {
            return level switch
            {
                "Low" => "低",
                "Medium" => "中",
                "High" => "高",
                "Critical" => "严重",
                _ => level
            };
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
                        Error = "",
                        ShowOnDashboard = tag.ShowOnDashboard
                    };
                    _allTagRows.Add(row);
                }
            }
            ApplyTagFilter();            
            ConfigureTagGrid();
        }

        private void BuildTagCards()
        {
            var host = flowTagCards.FlowLayoutPanel;
            host.Controls.Clear();         
            _tagCardMap.Clear();

            foreach (var row in _tagRows.Where(r => r.ShowOnDashboard))
            {
                var card = CreateTagCard(row);

                host.Controls.Add(card.Panel);
                _tagCardMap[row.TagId] = card;
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


      

        //给报警表加颜色
        private void ApplyAlarmRowStyle()
        {
            foreach (DataGridViewRow gridRow in dgvAlarms.Rows)
            {
                var level = gridRow.Cells["Level"]?.Value?.ToString();

                if (level == "严重")
                {
                    gridRow.DefaultCellStyle.BackColor = Color.MistyRose;
                    gridRow.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else if (level == "高")
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

            _configForm = new ConfigForm(_authService, _auditLogRepository, _factory, _tagRepo);

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
            await RefreshDeviceCardsAsync();

            if (oldSelectedDevicedId is not null)
            {
                for (var i = 0; i < toolStripCboDeviceFilter.ComboBox.Items.Count; i++)
                {
                    if (toolStripCboDeviceFilter.ComboBox.Items[i] is DeviceFilterItem item &&
                        item.DeviceId == oldSelectedDevicedId)
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
            if(dgvTags.Columns.Contains("ShowOnDashboard"))
            {
                dgvTags.Columns["ShowOnDashboard"].Visible = false;
            }
        }

        private async void toolStripCboDeviceFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripCboDeviceFilter.ComboBox.SelectedItem is not DeviceFilterItem item)
            {
                return;
            }

            _selectedDeviceId = item.DeviceId;
            await RefreshDeviceCardsAsync();
            ApplyTagFilter();
        }

        /// <summary>
        /// 设备卡片刷新方法
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task RefreshDeviceCardsAsync()
        {
            var devices = await _deviceRepo.GetEnableDevicesAsync();

            flowDeviceCards.Controls.Clear();

            foreach (var device in devices)
            {
                var card = CreateDeviceCard(device);
                flowDeviceCards.Controls.Add(card);
            }
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

            foreach (var row in rows)
            {
                _tagRows.Add(row);
                _tagRowMap[row.TagId] = row;
            }

            dgvTags.Refresh();
            BuildTagCards();
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

            toolStripCboDeviceFilter.SelectedIndexChanged += toolStripCboDeviceFilter_SelectedIndexChanged;

            toolStripCboDeviceFilter.ComboBox.SelectedIndex = 0;
            _selectedDeviceId = null;
        }

       

        /// <summary>
        /// 创建设备卡片
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Control? CreateDeviceCard(Device device)
        {
            var isSelected=_selectedDeviceId== device.Id;

            var card = new Sunny.UI.UIPanel
            {
                Width = flowDeviceCards.ClientSize.Width - 25,
                Height = 86,
                Margin = new Padding(4, 4, 4, 8),
                FillColor = isSelected ? Color.FromArgb(230, 245, 255) : Color.Wheat,
                RectColor = isSelected ? Color.DodgerBlue : Color.FromArgb(220, 225, 232),
                Cursor = Cursors.Hand,
                Tag = device.Id
            };

            var lblName = new Sunny.UI.UIPanel
            {
                Text = device.Name,
                Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold),
                Location = new Point(12, 10),
                Size = new Size(card.Width - 24, 26)
            };

            var lblInfo = new Sunny.UI.UILabel
            {
                Text = $"{device.ProtocolType} | {device.ComPort ?? device.Ip}",
                ForeColor = Color.DimGray,
                Location = new Point(12, 40),
                Size = new Size(card.Width - 24, 24)
            };

            card.Controls.Add(lblName);
            card.Controls.Add(lblInfo);

            card.Click += DeviceCard_Click;
            lblName.Click += DeviceCard_Click;
            lblInfo.Click += DeviceCard_Click;

            return card;
        }

        /// <summary>
        /// 点击卡片切换筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void DeviceCard_Click(object? sender, EventArgs e)
        {
            Guid? deviceId = null;

            if(sender is Control control)
            {
                if(control.Tag is Guid id)
                {
                    deviceId = id;
                }
                else if(control.Parent?.Tag is Guid parentId)
                {
                    deviceId = parentId;
                }
            }

            if(deviceId is null)
            {
                return;
            }

            SelectDeviceFilter(deviceId.Value);
        }

        /// <summary>
        /// 选择设备的筛选方法
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SelectDeviceFilter(Guid deviceId)
        {
            for(var i=0;i<toolStripCboDeviceFilter.ComboBox.Items.Count;i++)
            {
                if (toolStripCboDeviceFilter.ComboBox.Items[i] is DeviceFilterItem item &&
                        item.DeviceId == deviceId)
                {
                    toolStripCboDeviceFilter.ComboBox.SelectedIndex = i;
                    return;
                }
            }
            toolStripCboDeviceFilter.ComboBox.SelectedIndex = 0;
        }


        private void ConfigureAlarmGrid()
        {
            dgvAlarms.AutoGenerateColumns = true;

            dgvAlarms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvAlarms.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvAlarms.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvAlarms.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvAlarms.ScrollBars = ScrollBars.Both;

            if (dgvAlarms.Columns.Contains("RuleId"))
            {
                dgvAlarms.Columns["RuleId"].Visible = false;
            }

            SetAlarmColumn("DeviceName", "设备", 250);
            SetAlarmColumn("TagName", "点位", 120);
            SetAlarmColumn("Address", "地址", 100);
            SetAlarmColumn("RuleName", "报警规则", 200);
            SetAlarmColumn("Level", "等级", 90);
            SetAlarmColumn("State", "状态", 90);
            SetAlarmColumn("TriggerValue", "触发值", 100);
            SetAlarmColumn("TriggerTime", "触发时间", 130);
        }

        private void SetAlarmColumn(string columnName, string headerText, int width)
        {
            if (!dgvAlarms.Columns.Contains(columnName))
            {
                return;
            }

            var column = dgvAlarms.Columns[columnName];
            column.HeaderText = headerText;
            column.Width = width;
        }

        /// <summary>
        /// 创建点位卡片
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private TagCardControls CreateTagCard(TagGridRow row)
        {
            var panel = new Sunny.UI.UIPanel
            {
                Width = 230,
                Height = 135,
                Margin = new Padding(8),
                FillColor = Color.White,
                RectColor = Color.FromArgb(210, 220, 230)
            };

            var lblName = new Sunny.UI.UILabel
            {
                Text = row.DeviceName,
                Location = new Point(10, 8),
                Size = new Size(210, 24),
                Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 45, 60)
            };

            var lblAddress = new Sunny.UI.UILabel
            {
                Text = $"{row.Name} / {row.Address}",
                Location = new Point(10, 34),
                Size = new Size(210, 22),
                Font = new Font("Microsoft YaHei UI", 8F, FontStyle.Regular),
                ForeColor = Color.DimGray
            };

            var lblValue = new Sunny.UI.UILabel
            {
                Text = "--",
                Location = new Point(10, 58),
                Size = new Size(210, 42),
                Font = new Font("Consolas", 18F, FontStyle.Bold),
                ForeColor = Color.DodgerBlue
            };

            var lblQuality = new Sunny.UI.UILabel
            {
                Text = "None",
                Location = new Point(10, 104),
                Size = new Size(210, 22),
                Font = new Font("Microsoft YaHei UI", 8F, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            panel.Controls.Add(lblName);
            panel.Controls.Add(lblAddress);
            panel.Controls.Add(lblValue);
            panel.Controls.Add(lblQuality);

            return new TagCardControls
            {
                Panel = panel,
                NameLabel = lblName,
                AddressLabel = lblAddress,
                ValueLabel = lblValue,
                QualityLabel = lblQuality
            };
        }

        /// <summary>
        /// 刷新报警
        /// </summary>
        private void UpdateAlarmBanner()
        {
            var alarms = _alarmStateStore.GetCurrentAlarms().ToList();

            if (alarms.Count == 0)
            {
                pnlAlarmBanner.FillColor = Color.Honeydew;
                pnlAlarmBanner.RectColor = Color.ForestGreen;
                lblAlarmBanner.ForeColor = Color.ForestGreen;
                lblAlarmBanner.Text = "系统运行正常";
                return;
            }

            var topAlarm=alarms
               .OrderByDescending(a=>GetAlarmLevelWeight(a.Level.ToString()))
               .ThenByDescending(a => a.TriggerTime)
               .First();

            var tagRow=_allTagRows.FirstOrDefault(t => t.TagId == topAlarm.TagId);

            var deviceName = tagRow?.DeviceName ?? "--";
            var tagName = tagRow?.Name ?? "--";
            var value = topAlarm.TriggerValue?.ToString() ?? "--";

            pnlAlarmBanner.FillColor = Color.MistyRose;
            pnlAlarmBanner.RectColor = Color.Firebrick;
            lblAlarmBanner.ForeColor = Color.Firebrick;
            lblAlarmBanner.Text = $"报警：{deviceName} / {tagName}，{topAlarm.RuleName}，当前值：{value}";
        }

        /// <summary>
        /// 获取报警等级权重，用于排序显示最严重的报警在前面
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private object GetAlarmLevelWeight(string level)
        {
            return level switch
            {
                "Critical" => 4,
                "High" => 3,
                "Medium" => 2,
                "Low" => 1,
                _ => 0
            };
        }
    }


}
