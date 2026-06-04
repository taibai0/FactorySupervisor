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
        private readonly NumericUpDown numTimeoutMs = new();
        private readonly Panel pnlIp = new();
        private readonly Panel pnlPort = new();

        private readonly GroupBox grpModbusRtu = new();
        private readonly TextBox txtComPort = new();
        private readonly ComboBox cboBaudRate = new();
        private readonly ComboBox cboDataBits = new();
        private readonly ComboBox cboParity = new();
        private readonly ComboBox cboStopBits = new();
        private readonly NumericUpDown numRtuUnitId = new();

        private readonly GroupBox grpModbusTcp = new();
        private readonly NumericUpDown numTcpUnitId = new();

        private readonly GroupBox grpS7 = new();
        private readonly ComboBox cboS7CpuType = new();
        private readonly NumericUpDown numS7Rack = new();
        private readonly NumericUpDown numS7Slot = new();

        private readonly Button btnOk = new();
        private readonly Button btnCancel = new();

        private readonly DeviceConfigRow? _editingDevice;

        public DeviceConfigRow? Device { get; private set; }

        public DeviceEditForm()
        {
            InitializeUi();
            InitializeDefaults();
            UpdateProtocolParameterVisible();
        }

        public DeviceEditForm(DeviceConfigRow device)
        {
            InitializeUi();
            InitializeDefaults();

            _editingDevice = device;

            Text = "编辑设备";

            txtName.Text = device.Name;
            cboProtocolType.Text = device.ProtocolType;
            chkEnabled.Checked = device.Enabled;
            txtIp.Text = device.Ip;
            numPort.Value = ClampToNumeric(device.Port, numPort);
            txtComPort.Text = device.ComPort;
            cboBaudRate.Text = device.BaudRate.ToString();
            cboDataBits.Text = device.DataBits.ToString();
            cboParity.Text = device.Parity;
            cboStopBits.Text = device.StopBits;
            numRtuUnitId.Value = ClampToNumeric(device.UnitId, numRtuUnitId);
            numTcpUnitId.Value = ClampToNumeric(device.UnitId, numTcpUnitId);
            numTimeoutMs.Value = ClampToNumeric(device.TimeoutMs, numTimeoutMs);
            cboS7CpuType.Text = string.IsNullOrWhiteSpace(device.S7CpuType) ? "S71200" : device.S7CpuType;
            numS7Rack.Value = ClampToNumeric(device.S7Rack, numS7Rack);
            numS7Slot.Value = ClampToNumeric(device.S7Slot, numS7Slot);

            UpdateProtocolParameterVisible();
        }

        private void InitializeUi()
        {
            Text = "新增设备";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            ClientSize = new Size(560, 620);
            MinimumSize = new Size(520, 460);

            var root = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(18)
            };

            var contentPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0, 0, 0, 8)
            };

            var common = CreateCommonPanel();
            ConfigureModbusRtuGroup();
            ConfigureModbusTcpGroup();
            ConfigureS7Group();

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
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

            contentPanel.Controls.Add(common);
            contentPanel.Controls.Add(grpModbusRtu);
            contentPanel.Controls.Add(grpModbusTcp);
            contentPanel.Controls.Add(grpS7);

            root.Controls.Add(contentPanel);
            root.Controls.Add(buttonPanel);

            Controls.Add(root);

            AcceptButton = btnOk;
            CancelButton = btnCancel;
        }

        private GroupBox CreateCommonPanel()
        {
            var group = CreateGroup("公共参数", 6);
            var fields = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(12, 18, 12, 8)
            };

            fields.Controls.Add(CreateFieldPanel("设备名称", txtName));
            fields.Controls.Add(CreateFieldPanel("协议", cboProtocolType));
            fields.Controls.Add(CreateFieldPanel("启用", chkEnabled));
            ConfigureFieldPanel(pnlIp, "IP", txtIp);
            ConfigureFieldPanel(pnlPort, "端口", numPort);
            fields.Controls.Add(pnlIp);
            fields.Controls.Add(pnlPort);
            fields.Controls.Add(CreateFieldPanel("超时(ms)", numTimeoutMs));

            group.Controls.Add(fields);
            return group;
        }

        private void ConfigureModbusRtuGroup()
        {
            grpModbusRtu.Text = "Modbus RTU 参数";
            grpModbusRtu.Width = 500;
            grpModbusRtu.Height = 300;

            var table = CreateInnerTable(6);
            AddRow(table, 0, "串口", txtComPort);
            AddRow(table, 1, "波特率", cboBaudRate);
            AddRow(table, 2, "数据位", cboDataBits);
            AddRow(table, 3, "校验位", cboParity);
            AddRow(table, 4, "停止位", cboStopBits);
            AddRow(table, 5, "从站 Id", numRtuUnitId);

            grpModbusRtu.Controls.Add(table);
        }

        private void ConfigureModbusTcpGroup()
        {
            grpModbusTcp.Text = "Modbus TCP 参数";
            grpModbusTcp.Width = 500;
            grpModbusTcp.Height = 100;

            var table = CreateInnerTable(1);
            AddRow(table, 0, "从站 Id", numTcpUnitId);

            grpModbusTcp.Controls.Add(table);
        }

        private void ConfigureS7Group()
        {
            grpS7.Text = "S7 参数";
            grpS7.Width = 500;
            grpS7.Height = 200;

            var table = CreateInnerTable(3);
            AddRow(table, 0, "CPU 类型", cboS7CpuType);
            AddRow(table, 1, "Rack", numS7Rack);
            AddRow(table, 2, "Slot", numS7Slot);

            grpS7.Controls.Add(table);
        }

        private static GroupBox CreateGroup(string title, int rowCount)
        {
            return new GroupBox
            {
                Text = title,
                Width = 500,
                Height = 42 + rowCount * 36
            };
        }

        private static TableLayoutPanel CreateInnerTable(int rowCount)
        {
            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12, 18, 12, 8),
                ColumnCount = 2,
                RowCount = rowCount
            };

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            return table;
        }

        private static void AddRow(TableLayoutPanel table, int rowIndex, string labelText, Control editor)
        {
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));

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

        private static Panel CreateFieldPanel(string labelText, Control editor)
        {
            var panel = new Panel
            {
                Width = 460,
                Height = 34
            };

            ConfigureFieldPanel(panel, labelText, editor);
            return panel;
        }

        private static void ConfigureFieldPanel(Panel panel, string labelText, Control editor)
        {
            panel.Controls.Clear();

            var label = new Label
            {
                Text = labelText,
                Location = new Point(0, 4),
                Size = new Size(120, 24),
                TextAlign = ContentAlignment.MiddleLeft
            };

            editor.Location = new Point(120, 2);
            editor.Size = new Size(500, 100);
            editor.Dock = DockStyle.None;

            panel.Controls.Add(label);
            panel.Controls.Add(editor);

            panel.Width = label.Width + editor.Width;

        }

        private void InitializeDefaults()
        {
            cboProtocolType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProtocolType.Items.AddRange(new object[]
            {
                "ModbusRtu",
                "ModbusTcp",
                "S7"
            });
            cboProtocolType.SelectedIndexChanged += (_, _) => UpdateProtocolParameterVisible();
            cboProtocolType.SelectedItem = "ModbusRtu";

            txtName.Text = $"Device-{DateTime.Now:HHmmss}";
            txtIp.Text = "127.0.0.1";
            txtComPort.Text = "COM21";

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

            numRtuUnitId.Minimum = 1;
            numRtuUnitId.Maximum = 247;
            numRtuUnitId.Value = 1;

            numTcpUnitId.Minimum = 1;
            numTcpUnitId.Maximum = 247;
            numTcpUnitId.Value = 1;

            numTimeoutMs.Minimum = 100;
            numTimeoutMs.Maximum = 30000;
            numTimeoutMs.Increment = 100;
            numTimeoutMs.Value = 1000;

            cboS7CpuType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboS7CpuType.Items.AddRange(new object[] { "S7-200", "S7-200Smart", "S7-300", "S7-400", "S7-1200", "S7-1500" });
            cboS7CpuType.SelectedItem = "S71200";

            numS7Rack.Minimum = 0;
            numS7Rack.Maximum = 10;
            numS7Rack.Value = 0;

            numS7Slot.Minimum = 0;
            numS7Slot.Maximum = 10;
            numS7Slot.Value = 1;

            chkEnabled.Checked = false;
        }

        private void UpdateProtocolParameterVisible()
        {
            var protocol = cboProtocolType.Text;

            grpModbusRtu.Visible = protocol == "ModbusRtu";
            grpModbusTcp.Visible = protocol == "ModbusTcp";
            grpS7.Visible = protocol == "S7";
            pnlIp.Visible = protocol is "ModbusTcp" or "S7";
            pnlPort.Visible = protocol is "ModbusTcp" or "S7";

            if (protocol == "ModbusRtu")
            {
                txtIp.Text = string.IsNullOrWhiteSpace(txtIp.Text) ? "127.0.0.1" : txtIp.Text;
                numPort.Value = 1;
            }
            else if (protocol == "ModbusTcp")
            {
                txtIp.Text = string.IsNullOrWhiteSpace(txtIp.Text) || txtIp.Text == "127.0.0.1"
                    ? "192.168.0.10"
                    : txtIp.Text;
                numPort.Value = 502;
            }
            else if (protocol == "S7")
            {
                txtIp.Text = string.IsNullOrWhiteSpace(txtIp.Text) || txtIp.Text == "127.0.0.1"
                    ? "192.168.0.10"
                    : txtIp.Text;
                numPort.Value = 102;
            }
        }

        private void btnOk_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            Device = new DeviceConfigRow
            {
                Id = _editingDevice?.Id ?? Guid.NewGuid(),
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
                UnitId = cboProtocolType.Text == "ModbusTcp"
                    ? (byte)numTcpUnitId.Value
                    : (byte)numRtuUnitId.Value,
                TimeoutMs = (int)numTimeoutMs.Value,
                S7CpuType = cboS7CpuType.Text,
                S7Rack = (int)numS7Rack.Value,
                S7Slot = (int)numS7Slot.Value
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void InitializeComponent()
        {
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("设备名称不能为空");
                txtName.Focus();
                return false;
            }

            var protocol = cboProtocolType.Text;

            if (protocol is "ModbusTcp" or "S7" && string.IsNullOrWhiteSpace(txtIp.Text))
            {
                MessageBox.Show($"{protocol} 设备必须填写 IP 地址");
                txtIp.Focus();
                return false;
            }

            if (protocol == "ModbusRtu" && string.IsNullOrWhiteSpace(txtComPort.Text))
            {
                MessageBox.Show("Modbus RTU 设备必须填写串口");
                txtComPort.Focus();
                return false;
            }

            if (protocol == "S7" && string.IsNullOrWhiteSpace(cboS7CpuType.Text))
            {
                MessageBox.Show("S7 设备必须选择 CPU 类型");
                cboS7CpuType.Focus();
                return false;
            }

            return true;
        }

        private static decimal ClampToNumeric(decimal value, NumericUpDown numeric)
        {
            if (value < numeric.Minimum) return numeric.Minimum;
            if (value > numeric.Maximum) return numeric.Maximum;
            return value;
        }
    }
}
