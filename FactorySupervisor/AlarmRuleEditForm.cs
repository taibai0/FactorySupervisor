using FactorySupervisor.src.Contracts.Models;
using FactorySupervisor.src.Domain.Enums;
using Sunny.UI;

namespace FactorySupervisor
{
    public sealed class AlarmRuleEditForm : UIForm
    {
        private readonly UIComboBox cboTag = new();
        private readonly UITextBox txtName = new();
        private readonly UIComboBox cboLevel = new();
        private readonly UIComboBox cboConditionType = new();
        private readonly UIDoubleUpDown numThreshold = new();
        private readonly UICheckBox chkEnabled = new();
        private readonly UIButton btnOk = new();
        private readonly UIButton btnCancel = new();

        public AlarmRuleConfigRow? CreatedRule { get; private set; }

        public AlarmRuleEditForm(
            IReadOnlyList<DeviceConfigRow> devices,
            IReadOnlyList<TagConfigRow> tags)
        {
            InitializeUi();
            InitializeDefaults(devices, tags);
        }

        private void InitializeUi()
        {
            Text = "新增报警规则";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            ClientSize = new Size(720, 430);
            MinimumSize = new Size(620, 390);

            var root = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24)
            };

            var table = new UITableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6
            };

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            AddRow(table, 0, "报警点位", cboTag);
            AddRow(table, 1, "规则名称", txtName);
            AddRow(table, 2, "报警等级", cboLevel);
            AddRow(table, 3, "触发条件", cboConditionType);
            AddRow(table, 4, "阈值", numThreshold);
            AddRow(table, 5, "启用", chkEnabled);

            var buttonPanel = new UIFlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 8, 0, 0)
            };

            btnOk.Text = "确定";
            btnOk.Width = 96;
            btnOk.Click += btnOk_Click;

            btnCancel.Text = "取消";
            btnCancel.Width = 96;
            btnCancel.Click += (_, _) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            buttonPanel.Controls.Add(btnOk);
            buttonPanel.Controls.Add(btnCancel);

            root.Controls.Add(table);
            root.Controls.Add(buttonPanel);
            Controls.Add(root);
        }

        private static void AddRow(TableLayoutPanel table, int rowIndex, string labelText, Control editor)
        {
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

            var label = new UILabel
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            editor.Dock = DockStyle.Fill;
            editor.Margin = new Padding(0, 5, 0, 5);

            table.Controls.Add(label, 0, rowIndex);
            table.Controls.Add(editor, 1, rowIndex);
        }

        private void InitializeDefaults(
            IReadOnlyList<DeviceConfigRow> devices,
            IReadOnlyList<TagConfigRow> tags)
        {
            cboTag.DropDownStyle = UIDropDownStyle.DropDownList;
            cboTag.DataSource = BuildTagOptions(devices, tags);
            cboTag.DisplayMember = nameof(AlarmTagOption.DisplayName);
            cboTag.SelectedIndexChanged += (_, _) => FillDefaultRuleName();

            txtName.Watermark = "例如：液位过低";

            cboLevel.DropDownStyle = UIDropDownStyle.DropDownList;
            cboLevel.Items.AddRange(Enum.GetNames(typeof(AlarmLevel)));
            cboLevel.SelectedItem = AlarmLevel.High.ToString();

            cboConditionType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboConditionType.Items.AddRange(Enum.GetNames(typeof(AlarmConditionType)));
            cboConditionType.SelectedItem = AlarmConditionType.GreaterThan.ToString();

            numThreshold.Minimum = -999999;
            numThreshold.Maximum = 999999;
            numThreshold.DecimalPlaces = 2;
            numThreshold.Value = 80;

            chkEnabled.Checked = true;
            FillDefaultRuleName();
        }

        private static List<AlarmTagOption> BuildTagOptions(
            IReadOnlyList<DeviceConfigRow> devices,
            IReadOnlyList<TagConfigRow> tags)
        {
            var deviceNameMap = devices.ToDictionary(d => d.Id, d => d.Name);

            return tags
                .Where(t => t.Enabled)
                .OrderBy(t => deviceNameMap.TryGetValue(t.DeviceId, out var deviceName) ? deviceName : "")
                .ThenBy(t => t.Name)
                .Select(t =>
                {
                    var deviceName = deviceNameMap.TryGetValue(t.DeviceId, out var name)
                        ? name
                        : "未知设备";

                    return new AlarmTagOption
                    {
                        TagId = t.Id,
                        DeviceName = deviceName,
                        TagName = t.Name,
                        Address = t.Address,
                        DisplayName = $"{deviceName} - {t.Name} - {t.Address}"
                    };
                })
                .ToList();
        }

        private void FillDefaultRuleName()
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                return;
            }

            if (cboTag.SelectedItem is AlarmTagOption option)
            {
                txtName.Text = $"{option.DeviceName}-{option.TagName}报警";
            }
        }

        private void btnOk_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            var tag = (AlarmTagOption)cboTag.SelectedItem!;

            CreatedRule = new AlarmRuleConfigRow
            {
                Id = Guid.NewGuid(),
                TagId = tag.TagId,
                Name = txtName.Text.Trim(),
                Level = cboLevel.Text,
                ConditionType = cboConditionType.Text,
                Threshold = numThreshold.Value.ToString(),
                Enabled = chkEnabled.Checked
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateInput()
        {
            if (cboTag.SelectedItem is not AlarmTagOption)
            {
                MessageBox.Show("请选择报警点位。");
                cboTag.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("报警规则名称不能为空。");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboLevel.Text))
            {
                MessageBox.Show("请选择报警等级。");
                cboLevel.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboConditionType.Text))
            {
                MessageBox.Show("请选择触发条件。");
                cboConditionType.Focus();
                return false;
            }

            return true;
        }

        private sealed class AlarmTagOption
        {
            public Guid TagId { get; init; }
            public string DeviceName { get; init; } = "";
            public string TagName { get; init; } = "";
            public string Address { get; init; } = "";
            public string DisplayName { get; init; } = "";
        }
    }
}
