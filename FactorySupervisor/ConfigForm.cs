using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactorySupervisor
{
    public partial class ConfigForm : Form
    {
        private readonly IConfigRepository _configRepository = new SqlConfigRepository();

        public ConfigForm()
        {
            InitializeComponent();

            dgvDevices.AutoGenerateColumns = true;
            dgvTags.AutoGenerateColumns = true;
            dgvAlarmRules.AutoGenerateColumns = true;

            Load += ConfigForm_Load;
        }

        private async void ConfigForm_Load(object sender, EventArgs e)
        {
            await LoadDevicesAsync();
            await LoadTagsAsync();
            await LoadAlarmRulesAsync();
        }

        private async Task LoadAlarmRulesAsync()
        {
            var rows = await _configRepository.GetAlarmRulesAsync();
            dgvAlarmRules.DataSource = null;
            dgvAlarmRules.DataSource = rows;
        }

        private async Task LoadTagsAsync()
        {
            var rows = await _configRepository.GetTagsAsync();
            dgvTags.DataSource = null;
            dgvTags.DataSource = rows;
        }

        private async Task LoadDevicesAsync()
        {
            var rows = await _configRepository.GetDevicesAsync();
            dgvDevices.DataSource = null;
            dgvDevices.DataSource = rows;
        }
    }
}
