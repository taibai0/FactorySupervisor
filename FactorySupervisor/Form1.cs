using FactorySupervisor.src.Application.Services;
using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Infrastructure.Caching;
using FactorySupervisor.src.Infrastructure.Protocol;
using FactorySupervisor.src.Infrastructure.Repositories;
using FactorySupervisor.src.UI.WinForms;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;

namespace FactorySupervisor
{
    public partial class Form1 : Form
    {
        private bool _isRunning;
        private DateTimeOffset? _lastUpdateTime;

        private readonly BindingList<TagGridRow> _rows = new();
        private readonly Dictionary<Guid, TagGridRow> _rowMap = new();

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
        }

        private async void start_button_Click(object sender, EventArgs e)
        {
            await InitTagRowsAsync();

            _cts = new CancellationTokenSource();
            _acquisitionService = new AcquisitionService(_deviceRepo, _tagRepo, _factory, _cache);

            _ = Task.Run(() => _acquisitionService.RunAsync(_cts.Token));

            lblStatus.Text = "Running";
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            lblStatus.Text = "Stopped";
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            var hasAnyValue=false;

           foreach(var pair in _rowMap)
            {
                var tagId=pair.Key;
                var row=pair.Value;
                
                if(!_cache.TryGet(tagId, out var value)||value is null)
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
                _lastUpdateTime= DateTimeOffset.Now;
            }

            UpdateStatus();
            dgvTags.Refresh();
            ApplyRowStyle();
        }

        //更新状态
        private void UpdateStatus()
        {
            var badCount = _rows.Count(r => r.Quality == "Bad");
            var lastUpdate = _lastUpdateTime?.ToString("HH:mm;ss") ?? "--";

            lblStatus.Text=_isRunning
                ? $"Running | Last Update: {lastUpdate} | Bad Count: {badCount}"
                : $"Stopped | Last Update: {lastUpdate} | Bad Count: {badCount}";
        }


        //加行颜色
        private void ApplyRowStyle()
        {
            foreach(DataGridViewRow gridRow in dgvTags.Rows)
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


        //初始化表格行
        private async Task InitTagRowsAsync()
        {
            var devices = await _deviceRepo.GetEnableDevicesAsync();
            if (devices.Count == 0) return;

            var tags = await _tagRepo.GetByDeviceAsync(devices[0].Id);

            _rows.Clear();
            _rowMap.Clear();

            foreach(var tag in tags)
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
    }
}
