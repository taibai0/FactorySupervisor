using FactorySupervisor.src.Application.Services;
using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Infrastructure.Caching;
using FactorySupervisor.src.Infrastructure.Protocol;
using FactorySupervisor.src.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace FactorySupervisor
{
    public partial class Form1 : Form
    {
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

        private void start_button_Click(object sender, EventArgs e)
        {
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

        private async void timer1_Tick(object sender, EventArgs e)
        {
            var devices = await _deviceRepo.GetEnableDevicesAsync();
            lblStatus.Text = $"Devices={devices.Count}";
            if (devices.Count == 0) return;

            var device = devices[0];
            var tags = await _tagRepo.GetByDeviceAsync(device.Id);
            var rows = new List<object>();
            
            foreach (var tag in tags)
            {
                _cache.TryGet(tag.Id, out var value);

                rows.Add(new
                {
                    tag.Name,
                    tag.Address,
                    Value = value?.Value,
                    Quality = value?.Quality.ToString() ?? "None",
                    Time = value?.Timestamp.ToString("HH:mm:ss") ?? "",
                    Error = value?.Error ?? ""
                });
                label1.Text = ($"Tags:{tags.Count},Rows:{rows.Count}");
                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = true;
                BindRows(rows);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void BindRows(List<object> rows)
        {
            var firstBind = dataGridView1.DataSource == null;

            dataGridView1.DataSource = rows;

            if (firstBind)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        
    }
}
