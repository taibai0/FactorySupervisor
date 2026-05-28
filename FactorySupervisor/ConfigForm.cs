using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Contracts.Models;
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

        private readonly HashSet<Guid> _dirtyTagIds = new();  //记录脏数据
        private readonly HashSet<Guid> _dirtyDeviceIds = new();
        private readonly HashSet<Guid> _dirtyAlarmRulesIds = new();

        private object? _editingOldValue;

        public event EventHandler? ConfigSaved;
        public ConfigForm()
        {
            InitializeComponent();

            dgvDevices.AutoGenerateColumns = true;
            dgvTags.AutoGenerateColumns = true;
            dgvAlarmRules.AutoGenerateColumns = true;

            

            Load += ConfigForm_Load;
            dgvTags.CellValueChanged += dvgTags_CellValueChaged;
            dgvTags.CurrentCellDirtyStateChanged += dgvTags_CurrentCellDirtyStateChanged;
            dgvDevices.DataError += dgvConfig_DataError;
            dgvTags.DataError += dgvConfig_DataError;
            dgvAlarmRules.DataError += dgvConfig_DataError;

            
        }

        private void dgvConfig_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            // 编辑过程中允许临时非法输入，保存时再统一校验。
            e.ThrowException = false;
            e.Cancel = false;
        }

        //修改完checkBox列后立即提交编辑
        private void dgvTags_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (dgvTags.IsCurrentCellDirty)
            {
                dgvTags.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //记录被修改的行
        private void dvgTags_CellValueChaged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvTags.Rows[e.RowIndex].DataBoundItem is not TagConfigRow row)
            {
                return;
            }

            _dirtyTagIds.Add(row.Id);
        }

        private async void ConfigForm_Load(object sender, EventArgs e)
        {
            await LoadDevicesAsync();
            ConfigureDeviceGrid();

            await LoadTagsAsync();
            ConfigureTagGrid();

            await LoadAlarmRulesAsync();
            ConfigureAlarmRuleGrid();
        }

        private async Task LoadAlarmRulesAsync()
        {
            var rows = await _configRepository.GetAlarmRulesAsync();


            dgvAlarmRules.DataSource = null;
            dgvAlarmRules.DataSource = rows;

            _dirtyAlarmRulesIds.Clear();
        }

        private async Task LoadTagsAsync()
        {
            var rows = await _configRepository.GetTagsAsync();

            dgvTags.DataSource = null;
            dgvTags.DataSource = rows.ToList();

            _dirtyTagIds.Clear();
        }

        private async Task LoadDevicesAsync()
        {
            var rows = await _configRepository.GetDevicesAsync();


            dgvDevices.DataSource = null;
            dgvDevices.DataSource = rows;

            _dirtyDeviceIds.Clear();
        }

        private void ConfigureDeviceGrid()
        {
            foreach (DataGridViewColumn column in dgvDevices.Columns)
            {
                column.ReadOnly = true;
            }
            dgvDevices.Columns["Enabled"].ReadOnly = false;
        }

        private void ConfigureTagGrid()
        {
            foreach (DataGridViewColumn col in dgvTags.Columns)
            {
                col.ReadOnly = true;
            }

            dgvTags.Columns["ScanMs"].ReadOnly = false;
            dgvTags.Columns["ArchiveEnabled"].ReadOnly = false;
            dgvTags.Columns["Enabled"].ReadOnly = false;
        }

        private void ConfigureAlarmRuleGrid()
        {
            foreach (DataGridViewColumn col in dgvAlarmRules.Columns)
            {
                col.ReadOnly = true;
            }

            dgvAlarmRules.Columns["Threshold"].ReadOnly = false;
            dgvAlarmRules.Columns["Enabled"].ReadOnly = false;
        }


        //保存点位配置
        private async void btnSaveTags_Click(object sender, EventArgs e)
        {
            dgvTags.EndEdit();

            if (_dirtyTagIds.Count == 0)
            {
                MessageBox.Show("没有修改需要保存");
                return;
            }

            if (dgvTags.DataSource is not IEnumerable<TagConfigRow> rows)
            {
                MessageBox.Show("没有可保存的点位配置");
                return;
            }

            var changedRows = rows
                .Where(r => _dirtyTagIds.Contains(r.Id))
                .ToList();

            foreach (var row in changedRows)
            {
                if (!int.TryParse(row.ScanMs, out var scanMs) || scanMs <= 0)
                {
                    MessageBox.Show($"点位 {row.Name} 的 ScanMs 必须是大于 0 的整数");
                    return;
                }

                await _configRepository.UpdateTagAsync(row);
            }

            _dirtyTagIds.Clear();

            MessageBox.Show($"点位配置保存成功，共保存 {changedRows.Count} 行");
            ConfigSaved?.Invoke(this, EventArgs.Empty);

            await LoadTagsAsync();
            ConfigureTagGrid();
            
        }

        //保存设备配置
        private async void btnSaveDevices_Click(object sender, EventArgs e)
        {
            dgvDevices.EndEdit();

            if (_dirtyDeviceIds.Count == 0)
            {
                MessageBox.Show("没有修改需要保存");
                return;
            }

            if (dgvDevices.DataSource is not IEnumerable<DeviceConfigRow> rows)
            {
                MessageBox.Show("没有可保存的设备配置");
                return;
            }

            var changedRows = rows.Where(r => _dirtyDeviceIds.Contains(r.Id)).ToList();

            foreach (var row in changedRows)
            {
                await _configRepository.UpdateDeviceAsync(row);
            }

            _dirtyDeviceIds.Clear();

            MessageBox.Show($"设备配置保存成功，共保存 {changedRows.Count} 行");
            await LoadDevicesAsync();
            ConfigureDeviceGrid();
        }

        //保存报警规则配置
        private async void btnAlarmRuleSave_Click(object sender, EventArgs e)
        {
            dgvAlarmRules.EndEdit();

            if (_dirtyAlarmRulesIds.Count == 0)
            {
                MessageBox.Show("没有修改需要保存");
                return;
            }

            if (dgvAlarmRules.DataSource is not IEnumerable<AlarmRuleConfigRow> rows)
            {
                MessageBox.Show("没有可保存的点位配置");
                return;
            }

            var changedRows = rows
                .Where(r => _dirtyAlarmRulesIds.Contains(r.Id))
                .ToList();

            foreach (var row in changedRows)
            {
                if (!double.TryParse(row.Threshold, out _))
                {
                    MessageBox.Show($"报警规则 {row.Name} 的阈值必须是数字");
                    return;
                }

                await _configRepository.UpdateAlarmRuleAsync(row);
            }

            _dirtyAlarmRulesIds.Clear();

            MessageBox.Show($"报警规则保存成功，共保存 {changedRows.Count} 行");

            await LoadAlarmRulesAsync();
            ConfigureAlarmRuleGrid();
        }

        private void dgvDevices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvDevices.Rows[e.RowIndex].DataBoundItem is not DeviceConfigRow row)
            {
                return;
            }

            _dirtyDeviceIds.Add(row.Id);
        }


        private void dgvDevices_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDevices.IsCurrentCellDirty)
            {
                dgvDevices.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvAlarmRules_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvAlarmRules.IsCurrentCellDirty)
            {
                dgvAlarmRules.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvAlarmRules_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvAlarmRules.Rows[e.RowIndex].DataBoundItem is not AlarmRuleConfigRow row)
            {
                return;
            }
            _dirtyAlarmRulesIds.Add(row.Id);
        }

        //在开始编辑单元格时触发，用来记录原始值
        private void dgvConfig_CellBeginEdit(object? sender,DataGridViewCellCancelEventArgs e)
        {
            if (sender is not DataGridView dgv) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            _editingOldValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        //在结束编辑单元格时触发，用来恢复原始值
        private void dgvConfig_CellEndEdit(object? sender,DataGridViewCellEventArgs e)
        {
            if(sender is not DataGridView dgv) return;
            if(e.ColumnIndex<0||e.RowIndex < 0) return;

            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var value=cell.Value?.ToString();

            if (string.IsNullOrWhiteSpace(value))
            {
                cell.Value = _editingOldValue;
            }

            _editingOldValue = null;
        }
    }
}
