using FactorySupervisor.src.Contracts.Models;

namespace FactorySupervisor
{
    public sealed class TagEditForm : Form
    {
        private readonly ComboBox cboDevice = new();
        private readonly TextBox txtName = new();
        private readonly TextBox txtAddress = new();
        private readonly ComboBox cboDataType = new();
        private readonly NumericUpDown numScanMs = new();
        private readonly CheckBox chkArchiveEnabled = new();
        private readonly CheckBox chkEnabled = new();
        private readonly CheckBox chkShowOnDashboard = new();
        private readonly Button btnOk = new();
        private readonly Button btnCancel = new();

        private readonly TagConfigRow? _editingTag;

        public TagConfigRow? CreatedTag { get; private set; }

        public TagEditForm(IReadOnlyList<DeviceConfigRow> devices)
        {
            InitializeUi();
            InitializeDefaults(devices);
        }

        public TagEditForm(IReadOnlyList<DeviceConfigRow> devices, TagConfigRow tag)
        {
            InitializeUi();
            InitializeDefaults(devices);

            _editingTag = tag;
            Text = "编辑点位";

            cboDevice.SelectedValue = tag.DeviceId;
            txtName.Text = tag.Name;
            txtAddress.Text = tag.Address;
            cboDataType.Text = tag.DataType;

            if (int.TryParse(tag.ScanMs, out var scanMs))
            {
                numScanMs.Value = Math.Clamp(scanMs, (int)numScanMs.Minimum, (int)numScanMs.Maximum);
            }

            chkArchiveEnabled.Checked = tag.ArchiveEnabled;
            chkShowOnDashboard.Checked = tag.ShowOnDashboard;
            chkEnabled.Checked = tag.Enabled;
        }

        private void InitializeUi()
        {
            Text = "新增点位";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            ClientSize = new Size(620, 430);
            MinimumSize = new Size(520, 380);

            var root = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(18)
            };

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 8
            };

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            AddRow(table, 0, "所属设备", cboDevice);
            AddRow(table, 1, "点位名称", txtName);
            AddRow(table, 2, "地址", txtAddress);
            AddRow(table, 3, "数据类型", cboDataType);
            AddRow(table, 4, "采集周期(ms)", numScanMs);
            AddRow(table, 5, "历史归档", chkArchiveEnabled);
            AddRow(table, 6, "首页卡片", chkShowOnDashboard);
            AddRow(table, 7, "启用", chkEnabled);

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 48,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 8, 0, 0)
            };

            btnOk.Text = "确定";
            btnOk.Width = 90;
            btnOk.Click += btnOk_Click;

            btnCancel.Text = "取消";
            btnCancel.Width = 90;
            btnCancel.DialogResult = DialogResult.Cancel;

            buttonPanel.Controls.Add(btnOk);
            buttonPanel.Controls.Add(btnCancel);

            root.Controls.Add(table);
            root.Controls.Add(buttonPanel);
            Controls.Add(root);

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private static void AddRow(TableLayoutPanel table, int rowIndex, string labelText, Control editor)
        {
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            var label = new Label
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

        private void InitializeDefaults(IReadOnlyList<DeviceConfigRow> devices)
        {
            cboDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDevice.DataSource = devices.ToList();
            cboDevice.DisplayMember = nameof(DeviceConfigRow.Name);
            cboDevice.ValueMember = nameof(DeviceConfigRow.Id);

            cboDataType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDataType.Items.AddRange(new object[]
            {
                "Bool",
                "Int16",
                "UInt16",
                "Int32",
                "UInt32",
                "Float",
                "Double",
                "String"
            });
            cboDataType.SelectedItem = "UInt16";

            txtName.Text = $"Tag-{DateTime.Now:HHmmss}";
            txtAddress.Text = "40001";

            numScanMs.Minimum = 100;
            numScanMs.Maximum = 600000;
            numScanMs.Increment = 100;
            numScanMs.Value = 1000;

            chkArchiveEnabled.Checked = true;
            chkShowOnDashboard.Checked = false;
            chkEnabled.Checked = true;
        }

        private void btnOk_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            var device = (DeviceConfigRow)cboDevice.SelectedItem!;

            CreatedTag = new TagConfigRow
            {
                Id = _editingTag?.Id ?? Guid.NewGuid(),
                DeviceId = device.Id,
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                DataType = cboDataType.Text,
                ScanMs = ((int)numScanMs.Value).ToString(),
                ArchiveEnabled = chkArchiveEnabled.Checked,
                ShowOnDashboard = chkShowOnDashboard.Checked,
                Enabled = chkEnabled.Checked
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateInput()
        {
            if (cboDevice.SelectedItem is not DeviceConfigRow)
            {
                MessageBox.Show("请选择所属设备");
                cboDevice.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("点位名称不能为空");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("地址不能为空");
                txtAddress.Focus();
                return false;
            }

            return true;
        }
    }
}
