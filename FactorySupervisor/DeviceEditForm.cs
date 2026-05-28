using FactorySupervisor.src.Contracts.Models;

namespace FactorySupervisor
{
    public sealed class DeviceEditForm : Form
    {
        private readonly TextBox txtName = new();
        private readonly ComboBox cboProtocolType = new();
        private readonly CheckBox chkEnabled = new();
        private readonly TextBox txtIp = new();
        private readonly NumericUpDown numPort = new();
        private readonly TextBox txtComPort = new();
        private readonly ComboBox cboBaudRate = new();
        private readonly ComboBox cboDataBits = new();
        private readonly ComboBox cboParity = new();
        private readonly ComboBox cboStopBits = new();
        private readonly NumericUpDown numUnitId = new();
        private readonly NumericUpDown numTimeoutMs = new();
        private readonly Button btnOk = new();
        private readonly Button btnCancel = new();

        public DeviceConfigRow? Device { get; private set; }

        public DeviceEditForm()
        {
            InitializeUi();
            InitializeDefaults();
        }

        private void InitializeUi()
        {
            Text = "新增设备";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(520, 520);

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(18),
                ColumnCount = 2,
                RowCount = 13
            };

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            AddRow(table, 0, "设备名称", txtName);
            AddRow(table, 1, "协议", cboProtocolType);
            AddRow(table, 2, "启用", chkEnabled);
            AddRow(table, 3, "IP", txtIp);
            AddRow(table, 4, "端口", numPort);
            AddRow(table, 5, "串口", txtComPort);
            AddRow(table, 6, "波特率", cboBaudRate);
            AddRow(table, 7, "数据位", cboDataBits);
            AddRow(table, 8, "校验位", cboParity);
            AddRow(table, 9, "停止位", cboStopBits);
            AddRow(table, 10, "从站 Id", numUnitId);
            AddRow(table, 11, "超时(ms)", numTimeoutMs);

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };

            btnOk.Text = "确定";
            btnOk.Width = 90;
            btnOk.Click += btnOk_Click;

            btnCancel.Text = "取消";
            btnCancel.Width = 90;
            btnCancel.DialogResult = DialogResult.Cancel;

            buttonPanel.Controls.Add(btnOk);
            buttonPanel.Controls.Add(btnCancel);

            table.Controls.Add(buttonPanel, 1, 12);
            Controls.Add(table);

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private static void AddRow(TableLayoutPanel table, int rowIndex, string labelText, Control editor)
        {
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));

            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            editor.Dock = DockStyle.Fill;

            table.Controls.Add(label, 0, rowIndex);
            table.Controls.Add(editor, 1, rowIndex);
        }

        private void InitializeDefaults()
        {
            // 目前采集层只实现了 Modbus RTU，先限制新增设备类型，避免误选未实现协议。
            cboProtocolType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProtocolType.Items.Add("ModbusRtu");
            cboProtocolType.SelectedIndex = 0;

            txtName.Text = $"RTU-SLAVE-{DateTime.Now:HHmmss}";
            txtIp.Text = "127.0.0.1";
            txtComPort.Text = "COM1";

            numPort.Minimum = 1;
            numPort.Maximum = 65535;
            numPort.Value = 1;

            cboBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBaudRate.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            cboBaudRate.SelectedItem = "9600";

            cboDataBits.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDataBits.Items.AddRange(new object[] { "7", "8" });
            cboDataBits.SelectedItem = "8";

            cboParity.DropDownStyle = ComboBoxStyle.DropDownList;
            cboParity.Items.AddRange(new object[] { "None", "Odd", "Even" });
            cboParity.SelectedItem = "None";

            cboStopBits.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStopBits.Items.AddRange(new object[] { "One", "Two" });
            cboStopBits.SelectedItem = "One";

            numUnitId.Minimum = 1;
            numUnitId.Maximum = 247;
            numUnitId.Value = 1;

            numTimeoutMs.Minimum = 100;
            numTimeoutMs.Maximum = 30000;
            numTimeoutMs.Increment = 100;
            numTimeoutMs.Value = 1000;

            chkEnabled.Checked = false;
        }

        private void btnOk_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            Device = new DeviceConfigRow
            {
                Id = Guid.NewGuid(),
                Name = txtName.Text.Trim(),
                ProtocolType = cboProtocolType.Text,
                Ip = txtIp.Text.Trim(),
                Port = (int)numPort.Value,
                Enabled = chkEnabled.Checked,
                ComPort = txtComPort.Text.Trim(),
                BaudRate = int.Parse(cboBaudRate.Text),
                DataBits = int.Parse(cboDataBits.Text),
                Parity = cboParity.Text,
                StopBits = cboStopBits.Text,
                UnitId = (byte)numUnitId.Value,
                TimeoutMs = (int)numTimeoutMs.Value
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("设备名称不能为空");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIp.Text))
            {
                MessageBox.Show("IP 不能为空");
                txtIp.Focus();
                return false;
            }

            if (cboProtocolType.Text == "ModbusRtu" && string.IsNullOrWhiteSpace(txtComPort.Text))
            {
                MessageBox.Show("Modbus RTU 设备必须填写串口");
                txtComPort.Focus();
                return false;
            }

            return true;
        }
    }
}
