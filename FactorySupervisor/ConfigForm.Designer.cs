using System.ComponentModel.Design.Serialization;

namespace FactorySupervisor
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            tabConfig = new Sunny.UI.UITabControl();
            tabDevices = new TabPage();
            dgvDevices = new Sunny.UI.UIDataGridView();
            panelDeviceActions = new Sunny.UI.UIPanel();
            btnTestDeviceConnection = new Sunny.UI.UIButton();
            btnEditDevice = new Sunny.UI.UIButton();
            btnAddDevice = new Sunny.UI.UIButton();
            btnSaveDevices = new Sunny.UI.UIButton();
            tabTags = new TabPage();
            dgvTags = new Sunny.UI.UIDataGridView();
            panelTagActions = new Sunny.UI.UIPanel();
            btnEditTag = new Sunny.UI.UIButton();
            btnAddTag = new Sunny.UI.UIButton();
            btnSaveTags = new Sunny.UI.UIButton();
            tabAlarmRules = new TabPage();
            dgvAlarmRules = new Sunny.UI.UIDataGridView();
            panelAlarmActions = new Sunny.UI.UIPanel();
            btnAddAlarmRule = new Sunny.UI.UIButton();
            btnAlarmRuleSave = new Sunny.UI.UIButton();
            toolTipPermissions = new ToolTip(components);
            tabConfig.SuspendLayout();
            tabDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).BeginInit();
            panelDeviceActions.SuspendLayout();
            tabTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            panelTagActions.SuspendLayout();
            tabAlarmRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlarmRules).BeginInit();
            panelAlarmActions.SuspendLayout();
            SuspendLayout();
            // 
            // tabConfig
            // 
            tabConfig.Controls.Add(tabDevices);
            tabConfig.Controls.Add(tabTags);
            tabConfig.Controls.Add(tabAlarmRules);
            tabConfig.Dock = DockStyle.Fill;
            tabConfig.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabConfig.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            tabConfig.ItemSize = new Size(150, 40);
            tabConfig.Location = new Point(0, 0);
            tabConfig.MainPage = "";
            tabConfig.Name = "tabConfig";
            tabConfig.SelectedIndex = 0;
            tabConfig.Size = new Size(2378, 1137);
            tabConfig.SizeMode = TabSizeMode.Fixed;
            tabConfig.TabIndex = 0;
            tabConfig.TabUnSelectedForeColor = Color.FromArgb(240, 240, 240);
            tabConfig.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // tabDevices
            // 
            tabDevices.Controls.Add(dgvDevices);
            tabDevices.Controls.Add(panelDeviceActions);
            tabDevices.Location = new Point(0, 40);
            tabDevices.Name = "tabDevices";
            tabDevices.Size = new Size(2378, 1097);
            tabDevices.TabIndex = 0;
            tabDevices.Text = "设备配置";
            tabDevices.UseVisualStyleBackColor = true;
            // 
            // dgvDevices
            // 
            dgvDevices.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dgvDevices.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDevices.BackgroundColor = Color.White;
            dgvDevices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvDevices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvDevices.DefaultCellStyle = dataGridViewCellStyle3;
            dgvDevices.Dock = DockStyle.Fill;
            dgvDevices.EnableHeadersVisualStyles = false;
            dgvDevices.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvDevices.GridColor = Color.FromArgb(80, 160, 255);
            dgvDevices.Location = new Point(0, 45);
            dgvDevices.Name = "dgvDevices";
            dgvDevices.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvDevices.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvDevices.RowHeadersWidth = 72;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvDevices.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvDevices.SelectedIndex = -1;
            dgvDevices.Size = new Size(2378, 1052);
            dgvDevices.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvDevices.TabIndex = 0;
            dgvDevices.CellValueChanged += dgvDevices_CellValueChanged;
            dgvDevices.CurrentCellDirtyStateChanged += dgvDevices_CurrentCellDirtyStateChanged;
            // 
            // panelDeviceActions
            // 
            panelDeviceActions.Controls.Add(btnTestDeviceConnection);
            panelDeviceActions.Controls.Add(btnEditDevice);
            panelDeviceActions.Controls.Add(btnAddDevice);
            panelDeviceActions.Controls.Add(btnSaveDevices);
            panelDeviceActions.Dock = DockStyle.Top;
            panelDeviceActions.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelDeviceActions.Location = new Point(0, 0);
            panelDeviceActions.Margin = new Padding(4, 5, 4, 5);
            panelDeviceActions.MinimumSize = new Size(1, 1);
            panelDeviceActions.Name = "panelDeviceActions";
            panelDeviceActions.Size = new Size(2378, 45);
            panelDeviceActions.TabIndex = 1;
            panelDeviceActions.Text = null;
            panelDeviceActions.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnTestDeviceConnection
            // 
            btnTestDeviceConnection.BackColor = Color.PaleGreen;
            btnTestDeviceConnection.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnTestDeviceConnection.Location = new Point(5, 2);
            btnTestDeviceConnection.MinimumSize = new Size(1, 1);
            btnTestDeviceConnection.Name = "btnTestDeviceConnection";
            btnTestDeviceConnection.Size = new Size(131, 40);
            btnTestDeviceConnection.TabIndex = 3;
            btnTestDeviceConnection.Text = "测试连接";
            btnTestDeviceConnection.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnTestDeviceConnection.Click += btnTestDeviceConnection_Click;
            // 
            // btnEditDevice
            // 
            btnEditDevice.BackColor = Color.LightCoral;
            btnEditDevice.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEditDevice.Location = new Point(1852, 2);
            btnEditDevice.MinimumSize = new Size(1, 1);
            btnEditDevice.Name = "btnEditDevice";
            btnEditDevice.Size = new Size(123, 37);
            btnEditDevice.TabIndex = 2;
            btnEditDevice.Text = "编辑设备";
            btnEditDevice.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEditDevice.Click += btnEditDevice_Click;
            // 
            // btnAddDevice
            // 
            btnAddDevice.BackColor = Color.LightCoral;
            btnAddDevice.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddDevice.Location = new Point(2027, 2);
            btnAddDevice.MinimumSize = new Size(1, 1);
            btnAddDevice.Name = "btnAddDevice";
            btnAddDevice.Size = new Size(131, 40);
            btnAddDevice.TabIndex = 1;
            btnAddDevice.Text = "新增设备";
            btnAddDevice.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddDevice.Click += btnAddDevice_Click;
            // 
            // btnSaveDevices
            // 
            btnSaveDevices.BackColor = Color.Salmon;
            btnSaveDevices.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSaveDevices.Location = new Point(2204, 3);
            btnSaveDevices.MinimumSize = new Size(1, 1);
            btnSaveDevices.Name = "btnSaveDevices";
            btnSaveDevices.Size = new Size(166, 39);
            btnSaveDevices.TabIndex = 0;
            btnSaveDevices.Text = "保存设备配置";
            btnSaveDevices.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSaveDevices.Click += btnSaveDevices_Click;
            // 
            // tabTags
            // 
            tabTags.Controls.Add(dgvTags);
            tabTags.Controls.Add(panelTagActions);
            tabTags.Location = new Point(0, 40);
            tabTags.Name = "tabTags";
            tabTags.Size = new Size(200, 60);
            tabTags.TabIndex = 1;
            tabTags.Text = "点位配置";
            tabTags.UseVisualStyleBackColor = true;
            // 
            // dgvTags
            // 
            dgvTags.AllowUserToAddRows = false;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(235, 243, 255);
            dgvTags.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            dgvTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTags.BackgroundColor = Color.White;
            dgvTags.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle7.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dgvTags.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvTags.DefaultCellStyle = dataGridViewCellStyle8;
            dgvTags.Dock = DockStyle.Fill;
            dgvTags.EnableHeadersVisualStyles = false;
            dgvTags.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvTags.GridColor = Color.FromArgb(80, 160, 255);
            dgvTags.Location = new Point(0, 45);
            dgvTags.Name = "dgvTags";
            dgvTags.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle9.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dgvTags.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dgvTags.RowHeadersWidth = 72;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvTags.RowsDefaultCellStyle = dataGridViewCellStyle10;
            dgvTags.SelectedIndex = -1;
            dgvTags.Size = new Size(200, 15);
            dgvTags.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvTags.TabIndex = 1;
            dgvTags.CellBeginEdit += dgvConfig_CellBeginEdit;
            dgvTags.CellEndEdit += dgvConfig_CellEndEdit;
            // 
            // panelTagActions
            // 
            panelTagActions.Controls.Add(btnEditTag);
            panelTagActions.Controls.Add(btnAddTag);
            panelTagActions.Controls.Add(btnSaveTags);
            panelTagActions.Dock = DockStyle.Top;
            panelTagActions.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelTagActions.Location = new Point(0, 0);
            panelTagActions.Margin = new Padding(4, 5, 4, 5);
            panelTagActions.MinimumSize = new Size(1, 1);
            panelTagActions.Name = "panelTagActions";
            panelTagActions.Size = new Size(200, 45);
            panelTagActions.TabIndex = 2;
            panelTagActions.Text = null;
            panelTagActions.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnEditTag
            // 
            btnEditTag.BackColor = Color.LightCoral;
            btnEditTag.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEditTag.Location = new Point(1857, 2);
            btnEditTag.MinimumSize = new Size(1, 1);
            btnEditTag.Name = "btnEditTag";
            btnEditTag.Size = new Size(131, 40);
            btnEditTag.TabIndex = 2;
            btnEditTag.Text = "编辑点位";
            btnEditTag.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEditTag.Click += btnEditTag_Click;
            // 
            // btnAddTag
            // 
            btnAddTag.BackColor = Color.LightCoral;
            btnAddTag.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddTag.Location = new Point(2018, 2);
            btnAddTag.MinimumSize = new Size(1, 1);
            btnAddTag.Name = "btnAddTag";
            btnAddTag.Size = new Size(131, 40);
            btnAddTag.TabIndex = 1;
            btnAddTag.Text = "新增点位";
            btnAddTag.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddTag.Click += btnAddTag_Click;
            // 
            // btnSaveTags
            // 
            btnSaveTags.BackColor = Color.LightCoral;
            btnSaveTags.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSaveTags.Location = new Point(2170, 2);
            btnSaveTags.MinimumSize = new Size(1, 1);
            btnSaveTags.Name = "btnSaveTags";
            btnSaveTags.Size = new Size(189, 40);
            btnSaveTags.TabIndex = 0;
            btnSaveTags.Text = "保存点位配置";
            btnSaveTags.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSaveTags.Click += btnSaveTags_Click;
            // 
            // tabAlarmRules
            // 
            tabAlarmRules.Controls.Add(dgvAlarmRules);
            tabAlarmRules.Controls.Add(panelAlarmActions);
            tabAlarmRules.Location = new Point(0, 40);
            tabAlarmRules.Name = "tabAlarmRules";
            tabAlarmRules.Size = new Size(2378, 1097);
            tabAlarmRules.TabIndex = 2;
            tabAlarmRules.Text = "报警规则";
            tabAlarmRules.UseVisualStyleBackColor = true;
            // 
            // dgvAlarmRules
            // 
            dgvAlarmRules.AllowUserToAddRows = false;
            dataGridViewCellStyle11.BackColor = Color.FromArgb(235, 243, 255);
            dgvAlarmRules.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            dgvAlarmRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlarmRules.BackgroundColor = Color.White;
            dgvAlarmRules.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle12.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle12.ForeColor = Color.White;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
            dgvAlarmRules.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            dgvAlarmRules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = SystemColors.Window;
            dataGridViewCellStyle13.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle13.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = DataGridViewTriState.False;
            dgvAlarmRules.DefaultCellStyle = dataGridViewCellStyle13;
            dgvAlarmRules.Dock = DockStyle.Fill;
            dgvAlarmRules.EnableHeadersVisualStyles = false;
            dgvAlarmRules.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvAlarmRules.GridColor = Color.FromArgb(80, 160, 255);
            dgvAlarmRules.Location = new Point(0, 45);
            dgvAlarmRules.Name = "dgvAlarmRules";
            dgvAlarmRules.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle14.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle14.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle14.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle14.SelectionForeColor = Color.White;
            dataGridViewCellStyle14.WrapMode = DataGridViewTriState.True;
            dgvAlarmRules.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            dgvAlarmRules.RowHeadersWidth = 72;
            dataGridViewCellStyle15.BackColor = Color.White;
            dataGridViewCellStyle15.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvAlarmRules.RowsDefaultCellStyle = dataGridViewCellStyle15;
            dgvAlarmRules.SelectedIndex = -1;
            dgvAlarmRules.Size = new Size(2378, 1052);
            dgvAlarmRules.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvAlarmRules.TabIndex = 1;
            dgvAlarmRules.CellBeginEdit += dgvConfig_CellBeginEdit;
            dgvAlarmRules.CellEndEdit += dgvConfig_CellEndEdit;
            dgvAlarmRules.CellValueChanged += dgvAlarmRules_CellValueChanged;
            dgvAlarmRules.CurrentCellDirtyStateChanged += dgvAlarmRules_CurrentCellDirtyStateChanged;
            // 
            // panelAlarmActions
            // 
            panelAlarmActions.Controls.Add(btnAddAlarmRule);
            panelAlarmActions.Controls.Add(btnAlarmRuleSave);
            panelAlarmActions.Dock = DockStyle.Top;
            panelAlarmActions.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelAlarmActions.Location = new Point(0, 0);
            panelAlarmActions.Margin = new Padding(4, 5, 4, 5);
            panelAlarmActions.MinimumSize = new Size(1, 1);
            panelAlarmActions.Name = "panelAlarmActions";
            panelAlarmActions.Size = new Size(2378, 45);
            panelAlarmActions.TabIndex = 2;
            panelAlarmActions.Text = null;
            panelAlarmActions.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnAddAlarmRule
            // 
            btnAddAlarmRule.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddAlarmRule.Location = new Point(1993, 3);
            btnAddAlarmRule.MinimumSize = new Size(1, 1);
            btnAddAlarmRule.Name = "btnAddAlarmRule";
            btnAddAlarmRule.Size = new Size(176, 39);
            btnAddAlarmRule.TabIndex = 3;
            btnAddAlarmRule.Text = "添加报警规则";
            btnAddAlarmRule.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAddAlarmRule.Click += btnAddAlarmRule_Click;
            // 
            // btnAlarmRuleSave
            // 
            btnAlarmRuleSave.BackColor = Color.LightCoral;
            btnAlarmRuleSave.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAlarmRuleSave.Location = new Point(2189, 3);
            btnAlarmRuleSave.MinimumSize = new Size(1, 1);
            btnAlarmRuleSave.Name = "btnAlarmRuleSave";
            btnAlarmRuleSave.Size = new Size(179, 40);
            btnAlarmRuleSave.TabIndex = 0;
            btnAlarmRuleSave.Text = "保存报警规则";
            btnAlarmRuleSave.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnAlarmRuleSave.Click += btnAlarmRuleSave_Click;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2378, 1137);
            Controls.Add(tabConfig);
            Name = "ConfigForm";
            Text = "ConfigForm";
            Load += ConfigForm_Load;
            tabConfig.ResumeLayout(false);
            tabDevices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDevices).EndInit();
            panelDeviceActions.ResumeLayout(false);
            tabTags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTags).EndInit();
            panelTagActions.ResumeLayout(false);
            tabAlarmRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAlarmRules).EndInit();
            panelAlarmActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UITabControl tabConfig;
        private TabPage tabDevices;
        private TabPage tabTags;
        private Sunny.UI.UIDataGridView   dgvDevices;
        private TabPage tabAlarmRules;
        private Sunny.UI.UIDataGridView dgvTags;
        private Sunny.UI.UIDataGridView dgvAlarmRules;
        private Sunny.UI.UIPanel panelDeviceActions;
        private Sunny.UI.UIButton btnSaveDevices;
        private Sunny.UI.UIPanel panelAlarmActions;
        private Sunny.UI.UIButton btnAlarmRuleSave;
        private Sunny.UI.UIPanel panelTagActions;
        private Sunny.UI.UIButton btnSaveTags;
        private Sunny.UI.UIButton btnAddDevice;
        private Sunny.UI.UIButton btnAddTag;
        private ToolTip toolTipPermissions;
        private Sunny.UI.UIButton btnEditDevice;
        private Sunny.UI.UIButton btnEditTag;
        private Sunny.UI.UIButton btnTestDeviceConnection;
        private Sunny.UI.UIButton btnAddAlarmRule;
    }
}
