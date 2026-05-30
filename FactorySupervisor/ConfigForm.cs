using FactorySupervisor.src.Contracts.Abstractions;
using FactorySupervisor.src.Contracts.Models;
using FactorySupervisor.src.Domain.Entities;
using FactorySupervisor.src.Domain.Enums;
using FactorySupervisor.src.Infrastructure.Auth;
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

        private readonly IAuthService _authService;
        private readonly IAuditLogRepository _auditLogRepository;

        private object? _editingOldValue;

        public event EventHandler? ConfigSaved;
        public ConfigForm(IAuthService authService,IAuditLogRepository auditLogRepository)
        {
            InitializeComponent();
            _authService = authService;
            _auditLogRepository = auditLogRepository;

            dgvDevices.AutoGenerateColumns = true;
            dgvTags.AutoGenerateColumns = true;
            dgvAlarmRules.AutoGenerateColumns = true;

            

            Load += ConfigForm_Load;
            dgvTags.CellValueChanged += dvgTags_CellValueChaged;
            dgvTags.CurrentCellDirtyStateChanged += dgvTags_CurrentCellDirtyStateChanged;
            dgvDevices.DataError += dgvConfig_DataError;
            dgvTags.DataError += dgvConfig_DataError;
            dgvAlarmRules.DataError += dgvConfig_DataError;

            ApplyConfigPermissions();

        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <returns></returns>
        private bool CanManageDevices()
            => _authService.HasRole(UserRole.Admin);

        private bool CanManageTags()
            => _authService.HasRole(UserRole.Admin, UserRole.Engineer);

        private bool CanManageTagDefinition()
            => _authService.HasRole(UserRole.Admin);

        private bool CanManageAlarmRules()
            => _authService.HasRole(UserRole.Admin);



        private void ApplyConfigPermissions()
        {
            // 配置权限做成三档：Admin 管全局配置，Engineer 管点位，Operator 只读。
            ApplyButtonPermissionStyle(btnAddDevice, CanManageDevices(), "仅 Admin 可新增设备");
            ApplyButtonPermissionStyle(btnSaveDevices, CanManageDevices(), "仅 Admin 可保存设备配置");

            ApplyButtonPermissionStyle(btnAddTag, CanManageTags(), "仅 Admin / Engineer 可新增点位");
            ApplyButtonPermissionStyle(btnSaveTags, CanManageTags(), "仅 Admin / Engineer 可保存点位配置");

            ApplyButtonPermissionStyle(btnAlarmRuleSave, CanManageAlarmRules(), "仅 Admin 可保存报警规则");
        }

        private void ApplyButtonPermissionStyle(Button button, bool allowed, string deniedTip)
        {
            button.Enabled = allowed;

            if (allowed)
            {
                button.BackColor = Color.LightCoral;
                button.ForeColor = Color.Black;
                button.FlatStyle = FlatStyle.Standard;
                toolTipPermissions.SetToolTip(button, "");
                return;
            }

            // WinForms 禁用按钮默认对比度不明显，这里统一做成可识别的“无权限”样式。
            button.BackColor = Color.Gainsboro;
            button.ForeColor = Color.DimGray;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.DarkGray;
            toolTipPermissions.SetToolTip(button, deniedTip);
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

        private async void ConfigForm_Load(object? sender, EventArgs e)
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

            if (CanManageDevices())
            {
                dgvDevices.Columns["Enabled"].ReadOnly = false;
            }
        }

        private void ConfigureTagGrid()
        {
            foreach (DataGridViewColumn col in dgvTags.Columns)
            {
                col.ReadOnly = true;
            }

            if (CanManageTags())
            {
                dgvTags.Columns["ScanMs"].ReadOnly = false;
                dgvTags.Columns["ArchiveEnabled"].ReadOnly = false;
                dgvTags.Columns["Enabled"].ReadOnly = false;
            }

            if (CanManageTagDefinition())
            {
                dgvTags.Columns["Name"].ReadOnly = false;
                dgvTags.Columns["Address"].ReadOnly = false;
                dgvTags.Columns["DataType"].ReadOnly = false;
            }
        }

        private void ConfigureAlarmRuleGrid()
        {
            foreach (DataGridViewColumn col in dgvAlarmRules.Columns)
            {
                col.ReadOnly = true;
            }

            if (CanManageAlarmRules())
            {
                dgvAlarmRules.Columns["Threshold"].ReadOnly = false;
                dgvAlarmRules.Columns["Enabled"].ReadOnly = false;
            }
        }


        //保存点位配置
        private async void btnSaveTags_Click(object sender, EventArgs e)
        {
            if (!CanManageTags())
            {
                MessageBox.Show("当前用户没有保存点位配置的权限");
                return;
            }

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
                if (string.IsNullOrWhiteSpace(row.Name))
                {
                    MessageBox.Show("点位名称不能为空");
                    return;
                }

                if (string.IsNullOrWhiteSpace(row.Address))
                {
                    MessageBox.Show($"点位 {row.Name} 的地址不能为空");
                    return;
                }

                if (!Enum.TryParse<TagDataType>(row.DataType, out _))
                {
                    MessageBox.Show($"点位 {row.Name} 的数据类型无效");
                    return;
                }

                if (!int.TryParse(row.ScanMs, out var scanMs) || scanMs <= 0)
                {
                    MessageBox.Show($"点位 {row.Name} 的 ScanMs 必须是大于 0 的整数");
                    return;
                }

                await _configRepository.UpdateTagAsync(row);
            }
            var names = string.Join(", ", changedRows.Select(r => r.Name));

            await WriteAuditAsync(
                "UpdateTagConfig",
                $"保存点位配置：{names}");

            _dirtyTagIds.Clear();

            MessageBox.Show($"点位配置保存成功，共保存 {changedRows.Count} 行");
            ConfigSaved?.Invoke(this, EventArgs.Empty);

            await LoadTagsAsync();
            ConfigureTagGrid();

        }

        //保存设备配置
        private async void btnSaveDevices_Click(object sender, EventArgs e)
        {
            if (!CanManageDevices())
            {
                MessageBox.Show("当前用户没有保存设备配置的权限");
                return;
            }

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
            ConfigSaved?.Invoke(this, EventArgs.Empty);

            var names = string.Join(", ", changedRows.Select(r => r.Name));

            await WriteAuditAsync(
                "UpdateDeviceConfig",
                $"保存设备配置：{names}");

            await LoadDevicesAsync();
            ConfigureDeviceGrid();
        }

        //保存报警规则配置
        private async void btnAlarmRuleSave_Click(object sender, EventArgs e)
        {
            if (!CanManageAlarmRules())
            {
                MessageBox.Show("当前用户没有保存报警规则的权限");
                return;
            }

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
            var names = string.Join(", ", changedRows.Select(r => r.Name));

            await WriteAuditAsync(
                "UpdateAlarmRuleConfig",
                $"保存报警规则配置：{names}");

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
        private void dgvConfig_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            if (sender is not DataGridView dgv) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            _editingOldValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        //在结束编辑单元格时触发，用来恢复原始值
        private void dgvConfig_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (sender is not DataGridView dgv) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var value = cell.Value?.ToString();

            if (string.IsNullOrWhiteSpace(value))
            {
                cell.Value = _editingOldValue;
            }

            _editingOldValue = null;
        }

        /// <summary>
        /// 新增设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAddDevice_Click(object sender, EventArgs e)
        {
            if (!CanManageDevices())
            {
                MessageBox.Show("当前用户没有新增设备的权限");
                return;
            }

            using var form = new DeviceEditForm();
            if (form.ShowDialog(this) != DialogResult.OK || form.Device is null)
            {
                return;
            }

            await _configRepository.InsertDeviceAsync(form.Device);
            await WriteAuditAsync(
                "AddDevice",
                $"新增设备：{form.Device.Name},协议:{form.Device.ProtocolType}");

            MessageBox.Show("新增设备成功");
            ConfigSaved?.Invoke(this, EventArgs.Empty);

            await LoadDevicesAsync();
            ConfigureDeviceGrid();
        }

        /// <summary>
        /// 新增点位
        /// </summary>
        private async void btnAddTag_Click(object sender, EventArgs e)
        {
            if (!CanManageTags())
            {
                MessageBox.Show("当前用户没有新增点位的权限");
                return;
            }

            var devices = await _configRepository.GetDevicesAsync();
            if (devices.Count == 0)
            {
                MessageBox.Show("请先新增设备，再新增点位");
                return;
            }

            using var form = new TagEditForm(devices);
            if (form.ShowDialog(this) != DialogResult.OK || form.CreatedTag is null)
            {
                return;
            }

            await _configRepository.InsertTagAsync(form.CreatedTag);
            await WriteAuditAsync(
                "AddTag",
                $"新增点位：{form.CreatedTag.Name}，地址：{form.CreatedTag.Address}，设备Id：{form.CreatedTag.DeviceId}");

            MessageBox.Show("新增点位成功");
            ConfigSaved?.Invoke(this, EventArgs.Empty);

            await LoadTagsAsync();
            ConfigureTagGrid();
        }

        /// <summary>
        /// 审计辅助方法
        /// </summary>
        /// <param name="action">行为</param>
        /// <param name="detail">细节</param>
        /// <returns></returns>
        private async Task WriteAuditAsync(string action,string detail)
        {
            var user = _authService.CurrentUser;

            if(user is null)
            {
                return;
            }

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
    }
}
