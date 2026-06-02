namespace FactorySupervisor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dgvTags = new Sunny.UI.UIDataGridView();
            timerRefresh = new System.Windows.Forms.Timer(components);
            dgvAlarms = new Sunny.UI.UIDataGridView();
            menuStrip1 = new MenuStrip();
            系统ToolStripMenuItem = new ToolStripMenuItem();
            退出ToolStripMenuItem = new ToolStripMenuItem();
            配置管理ToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            start_button = new ToolStripButton();
            stop_button = new ToolStripButton();
            btnOpenHistory = new ToolStripButton();
            toolStripLabelDeviceFilter = new ToolStripLabel();
            toolStripCboDeviceFilter = new ToolStripComboBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelRunState = new ToolStripStatusLabel();
            toolStripStatusLabelUser = new ToolStripStatusLabel();
            toolStripStatusLabelBadCount = new ToolStripStatusLabel();
            toolStripStatusLabelLastUpdate = new ToolStripStatusLabel();
            panelDashboard = new Sunny.UI.UIPanel();
            pnlAlarmBanner = new Sunny.UI.UIPanel();
            lblAlarmBanner = new Sunny.UI.UILabel();
            pnlCardDbState = new Sunny.UI.UIPanel();
            lblCardDbStateValue = new Sunny.UI.UILabel();
            lblCardDbStateTitle = new Sunny.UI.UILabel();
            pnlCardAlarmCount = new Sunny.UI.UIPanel();
            lblCardAlarmCountValue = new Sunny.UI.UILabel();
            lblCardAlarmCountTitle = new Sunny.UI.UILabel();
            pnlCardDeviceState = new Sunny.UI.UIPanel();
            lblCardCommQualityValue = new Sunny.UI.UILabel();
            lblCardCommQualityTitle = new Sunny.UI.UILabel();
            pnlCardRunState = new Sunny.UI.UIPanel();
            lblCardRunStateValue = new Sunny.UI.UILabel();
            lblCardRunStateTitle = new Sunny.UI.UILabel();
            splitMain = new SplitContainer();
            grpDevice = new Sunny.UI.UIGroupBox();
            flowDeviceCards = new FlowLayoutPanel();
            splitContent = new SplitContainer();
            grpTags = new Sunny.UI.UIGroupBox();
            splitTags = new Sunny.UI.UISplitContainer();
            flowTagCards = new Sunny.UI.UIFlowLayoutPanel();
            grpAlarm = new Sunny.UI.UIGroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAlarms).BeginInit();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            panelDashboard.SuspendLayout();
            pnlAlarmBanner.SuspendLayout();
            pnlCardDbState.SuspendLayout();
            pnlCardAlarmCount.SuspendLayout();
            pnlCardDeviceState.SuspendLayout();
            pnlCardRunState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            grpDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContent).BeginInit();
            splitContent.Panel1.SuspendLayout();
            splitContent.Panel2.SuspendLayout();
            splitContent.SuspendLayout();
            grpTags.SuspendLayout();
            (splitTags).BeginInit();
            splitTags.Panel1.SuspendLayout();
            splitTags.Panel2.SuspendLayout();
            splitTags.SuspendLayout();
            grpAlarm.SuspendLayout();
            SuspendLayout();
            // 
            // dgvTags
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dgvTags.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvTags.BackgroundColor = Color.White;
            dgvTags.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvTags.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvTags.DefaultCellStyle = dataGridViewCellStyle3;
            dgvTags.Dock = DockStyle.Fill;
            dgvTags.EnableHeadersVisualStyles = false;
            dgvTags.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvTags.GridColor = Color.FromArgb(80, 160, 255);
            dgvTags.Location = new Point(0, 0);
            dgvTags.Name = "dgvTags";
            dgvTags.ReadOnly = true;
            dgvTags.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvTags.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvTags.RowHeadersWidth = 72;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvTags.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvTags.SelectedIndex = -1;
            dgvTags.Size = new Size(1182, 484);
            dgvTags.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvTags.TabIndex = 2;
            // 
            // timerRefresh
            // 
            timerRefresh.Tick += timerRefresh_Tick;
            // 
            // dgvAlarms
            // 
            dgvAlarms.AllowUserToAddRows = false;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(235, 243, 255);
            dgvAlarms.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            dgvAlarms.BackgroundColor = Color.White;
            dgvAlarms.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle7.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dgvAlarms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dgvAlarms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvAlarms.DefaultCellStyle = dataGridViewCellStyle8;
            dgvAlarms.Dock = DockStyle.Fill;
            dgvAlarms.EnableHeadersVisualStyles = false;
            dgvAlarms.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvAlarms.GridColor = Color.FromArgb(80, 160, 255);
            dgvAlarms.Location = new Point(0, 32);
            dgvAlarms.Name = "dgvAlarms";
            dgvAlarms.ReadOnly = true;
            dgvAlarms.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle9.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dgvAlarms.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dgvAlarms.RowHeadersWidth = 72;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dgvAlarms.RowsDefaultCellStyle = dataGridViewCellStyle10;
            dgvAlarms.SelectedIndex = -1;
            dgvAlarms.Size = new Size(873, 675);
            dgvAlarms.StripeOddColor = Color.FromArgb(235, 243, 255);
            dgvAlarms.TabIndex = 5;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(28, 28);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 系统ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(2447, 36);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            系统ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 退出ToolStripMenuItem, 配置管理ToolStripMenuItem });
            系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            系统ToolStripMenuItem.Size = new Size(72, 32);
            系统ToolStripMenuItem.Text = "系统";
            // 
            // 退出ToolStripMenuItem
            // 
            退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            退出ToolStripMenuItem.Size = new Size(213, 40);
            退出ToolStripMenuItem.Text = "退出";
            // 
            // 配置管理ToolStripMenuItem
            // 
            配置管理ToolStripMenuItem.Name = "配置管理ToolStripMenuItem";
            配置管理ToolStripMenuItem.Size = new Size(213, 40);
            配置管理ToolStripMenuItem.Text = "配置管理";
            配置管理ToolStripMenuItem.Click += 配置管理ToolStripMenuItem_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(28, 28);
            toolStrip1.Items.AddRange(new ToolStripItem[] { start_button, stop_button, btnOpenHistory, toolStripLabelDeviceFilter, toolStripCboDeviceFilter });
            toolStrip1.Location = new Point(0, 36);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(2447, 38);
            toolStrip1.TabIndex = 8;
            toolStrip1.Text = "toolStrip1";
            // 
            // start_button
            // 
            start_button.Name = "start_button";
            start_button.Size = new Size(100, 32);
            start_button.Text = "开始采集";
            start_button.Click += start_button_Click;
            // 
            // stop_button
            // 
            stop_button.Name = "stop_button";
            stop_button.Size = new Size(100, 32);
            stop_button.Text = "停止采集";
            stop_button.Click += stop_button_Click;
            // 
            // btnOpenHistory
            // 
            btnOpenHistory.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnOpenHistory.Image = (Image)resources.GetObject("btnOpenHistory.Image");
            btnOpenHistory.ImageTransparentColor = Color.Magenta;
            btnOpenHistory.Name = "btnOpenHistory";
            btnOpenHistory.Size = new Size(100, 32);
            btnOpenHistory.Text = "历史查询";
            btnOpenHistory.Click += btnOpenHistory_Click;
            // 
            // toolStripLabelDeviceFilter
            // 
            toolStripLabelDeviceFilter.Name = "toolStripLabelDeviceFilter";
            toolStripLabelDeviceFilter.Size = new Size(75, 32);
            toolStripLabelDeviceFilter.Text = "设备：";
            // 
            // toolStripCboDeviceFilter
            // 
            toolStripCboDeviceFilter.AutoSize = false;
            toolStripCboDeviceFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripCboDeviceFilter.Name = "toolStripCboDeviceFilter";
            toolStripCboDeviceFilter.Size = new Size(220, 36);
            toolStripCboDeviceFilter.SelectedIndexChanged += toolStripCboDeviceFilter_SelectedIndexChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(28, 28);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelRunState, toolStripStatusLabelUser, toolStripStatusLabelBadCount, toolStripStatusLabelLastUpdate });
            statusStrip1.Location = new Point(0, 921);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(2447, 37);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelRunState
            // 
            toolStripStatusLabelRunState.Name = "toolStripStatusLabelRunState";
            toolStripStatusLabelRunState.Size = new Size(228, 28);
            toolStripStatusLabelRunState.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabelUser
            // 
            toolStripStatusLabelUser.Name = "toolStripStatusLabelUser";
            toolStripStatusLabelUser.Size = new Size(228, 28);
            toolStripStatusLabelUser.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabelBadCount
            // 
            toolStripStatusLabelBadCount.Name = "toolStripStatusLabelBadCount";
            toolStripStatusLabelBadCount.Size = new Size(228, 28);
            toolStripStatusLabelBadCount.Text = "toolStripStatusLabel3";
            // 
            // toolStripStatusLabelLastUpdate
            // 
            toolStripStatusLabelLastUpdate.Name = "toolStripStatusLabelLastUpdate";
            toolStripStatusLabelLastUpdate.Size = new Size(228, 28);
            toolStripStatusLabelLastUpdate.Text = "toolStripStatusLabel4";
            // 
            // panelDashboard
            // 
            panelDashboard.Controls.Add(pnlAlarmBanner);
            panelDashboard.Controls.Add(pnlCardDbState);
            panelDashboard.Controls.Add(pnlCardAlarmCount);
            panelDashboard.Controls.Add(pnlCardDeviceState);
            panelDashboard.Controls.Add(pnlCardRunState);
            panelDashboard.Dock = DockStyle.Top;
            panelDashboard.FillColor = Color.FromArgb(245, 248, 252);
            panelDashboard.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelDashboard.Location = new Point(0, 74);
            panelDashboard.Margin = new Padding(4, 5, 4, 5);
            panelDashboard.MinimumSize = new Size(1, 1);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.RectColor = Color.FromArgb(220, 225, 232);
            panelDashboard.Size = new Size(2447, 140);
            panelDashboard.TabIndex = 11;
            panelDashboard.Text = null;
            panelDashboard.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pnlAlarmBanner
            // 
            pnlAlarmBanner.Controls.Add(lblAlarmBanner);
            pnlAlarmBanner.FillColor = Color.Honeydew;
            pnlAlarmBanner.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlAlarmBanner.Location = new Point(1110, 5);
            pnlAlarmBanner.Margin = new Padding(4, 5, 4, 5);
            pnlAlarmBanner.MinimumSize = new Size(1, 1);
            pnlAlarmBanner.Name = "pnlAlarmBanner";
            pnlAlarmBanner.RectColor = Color.ForestGreen;
            pnlAlarmBanner.Size = new Size(1236, 135);
            pnlAlarmBanner.TabIndex = 15;
            pnlAlarmBanner.Text = null;
            pnlAlarmBanner.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblAlarmBanner
            // 
            lblAlarmBanner.Dock = DockStyle.Fill;
            lblAlarmBanner.Font = new Font("Microsoft YaHei UI", 11.1428576F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblAlarmBanner.ForeColor = Color.ForestGreen;
            lblAlarmBanner.Location = new Point(0, 0);
            lblAlarmBanner.Name = "lblAlarmBanner";
            lblAlarmBanner.Size = new Size(1236, 135);
            lblAlarmBanner.TabIndex = 0;
            lblAlarmBanner.Text = "系统运行正常";
            lblAlarmBanner.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlCardDbState
            // 
            pnlCardDbState.Controls.Add(lblCardDbStateValue);
            pnlCardDbState.Controls.Add(lblCardDbStateTitle);
            pnlCardDbState.FillColor = Color.White;
            pnlCardDbState.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlCardDbState.Location = new Point(488, 14);
            pnlCardDbState.Margin = new Padding(4, 5, 4, 5);
            pnlCardDbState.MinimumSize = new Size(1, 1);
            pnlCardDbState.Name = "pnlCardDbState";
            pnlCardDbState.RectColor = Color.FromArgb(220, 225, 232);
            pnlCardDbState.Size = new Size(200, 108);
            pnlCardDbState.TabIndex = 14;
            pnlCardDbState.Text = null;
            pnlCardDbState.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblCardDbStateValue
            // 
            lblCardDbStateValue.AutoSize = true;
            lblCardDbStateValue.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblCardDbStateValue.ForeColor = Color.FromArgb(48, 48, 48);
            lblCardDbStateValue.Location = new Point(4, 72);
            lblCardDbStateValue.Name = "lblCardDbStateValue";
            lblCardDbStateValue.Size = new Size(90, 33);
            lblCardDbStateValue.TabIndex = 13;
            lblCardDbStateValue.Text = "label8";
            // 
            // lblCardDbStateTitle
            // 
            lblCardDbStateTitle.AutoSize = true;
            lblCardDbStateTitle.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblCardDbStateTitle.ForeColor = SystemColors.ControlDarkDark;
            lblCardDbStateTitle.Location = new Point(3, 16);
            lblCardDbStateTitle.Name = "lblCardDbStateTitle";
            lblCardDbStateTitle.Size = new Size(96, 28);
            lblCardDbStateTitle.TabIndex = 12;
            lblCardDbStateTitle.Text = "数据库";
            // 
            // pnlCardAlarmCount
            // 
            pnlCardAlarmCount.Controls.Add(lblCardAlarmCountValue);
            pnlCardAlarmCount.Controls.Add(lblCardAlarmCountTitle);
            pnlCardAlarmCount.FillColor = Color.White;
            pnlCardAlarmCount.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlCardAlarmCount.Location = new Point(250, 14);
            pnlCardAlarmCount.Margin = new Padding(4, 5, 4, 5);
            pnlCardAlarmCount.MinimumSize = new Size(1, 1);
            pnlCardAlarmCount.Name = "pnlCardAlarmCount";
            pnlCardAlarmCount.RectColor = Color.FromArgb(220, 225, 232);
            pnlCardAlarmCount.Size = new Size(200, 108);
            pnlCardAlarmCount.TabIndex = 13;
            pnlCardAlarmCount.Text = null;
            pnlCardAlarmCount.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblCardAlarmCountValue
            // 
            lblCardAlarmCountValue.AutoSize = true;
            lblCardAlarmCountValue.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblCardAlarmCountValue.ForeColor = Color.FromArgb(48, 48, 48);
            lblCardAlarmCountValue.Location = new Point(4, 73);
            lblCardAlarmCountValue.Name = "lblCardAlarmCountValue";
            lblCardAlarmCountValue.Size = new Size(90, 33);
            lblCardAlarmCountValue.TabIndex = 13;
            lblCardAlarmCountValue.Text = "label6";
            // 
            // lblCardAlarmCountTitle
            // 
            lblCardAlarmCountTitle.AutoSize = true;
            lblCardAlarmCountTitle.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblCardAlarmCountTitle.ForeColor = SystemColors.ControlDarkDark;
            lblCardAlarmCountTitle.Location = new Point(3, 14);
            lblCardAlarmCountTitle.Name = "lblCardAlarmCountTitle";
            lblCardAlarmCountTitle.Size = new Size(124, 28);
            lblCardAlarmCountTitle.TabIndex = 12;
            lblCardAlarmCountTitle.Text = "当前警报";
            // 
            // pnlCardDeviceState
            // 
            pnlCardDeviceState.Controls.Add(lblCardCommQualityValue);
            pnlCardDeviceState.Controls.Add(lblCardCommQualityTitle);
            pnlCardDeviceState.FillColor = Color.White;
            pnlCardDeviceState.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlCardDeviceState.Location = new Point(13, 14);
            pnlCardDeviceState.Margin = new Padding(4, 5, 4, 5);
            pnlCardDeviceState.MinimumSize = new Size(1, 1);
            pnlCardDeviceState.Name = "pnlCardDeviceState";
            pnlCardDeviceState.RectColor = Color.FromArgb(220, 225, 232);
            pnlCardDeviceState.Size = new Size(200, 108);
            pnlCardDeviceState.TabIndex = 12;
            pnlCardDeviceState.Text = null;
            pnlCardDeviceState.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblCardCommQualityValue
            // 
            lblCardCommQualityValue.AutoSize = true;
            lblCardCommQualityValue.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblCardCommQualityValue.ForeColor = Color.FromArgb(48, 48, 48);
            lblCardCommQualityValue.Location = new Point(3, 65);
            lblCardCommQualityValue.Name = "lblCardCommQualityValue";
            lblCardCommQualityValue.Size = new Size(90, 33);
            lblCardCommQualityValue.TabIndex = 13;
            lblCardCommQualityValue.Text = "label4";
            // 
            // lblCardCommQualityTitle
            // 
            lblCardCommQualityTitle.AutoSize = true;
            lblCardCommQualityTitle.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblCardCommQualityTitle.ForeColor = SystemColors.ControlDarkDark;
            lblCardCommQualityTitle.Location = new Point(3, 15);
            lblCardCommQualityTitle.Name = "lblCardCommQualityTitle";
            lblCardCommQualityTitle.Size = new Size(124, 28);
            lblCardCommQualityTitle.TabIndex = 12;
            lblCardCommQualityTitle.Text = "通信质量";
            // 
            // pnlCardRunState
            // 
            pnlCardRunState.Controls.Add(lblCardRunStateValue);
            pnlCardRunState.Controls.Add(lblCardRunStateTitle);
            pnlCardRunState.FillColor = Color.White;
            pnlCardRunState.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            pnlCardRunState.Location = new Point(729, 14);
            pnlCardRunState.Margin = new Padding(4, 5, 4, 5);
            pnlCardRunState.MinimumSize = new Size(1, 1);
            pnlCardRunState.Name = "pnlCardRunState";
            pnlCardRunState.RectColor = Color.FromArgb(220, 225, 232);
            pnlCardRunState.Size = new Size(200, 108);
            pnlCardRunState.TabIndex = 11;
            pnlCardRunState.Text = null;
            pnlCardRunState.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // lblCardRunStateValue
            // 
            lblCardRunStateValue.AutoSize = true;
            lblCardRunStateValue.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblCardRunStateValue.ForeColor = Color.FromArgb(48, 48, 48);
            lblCardRunStateValue.Location = new Point(-1, 76);
            lblCardRunStateValue.Name = "lblCardRunStateValue";
            lblCardRunStateValue.Size = new Size(90, 33);
            lblCardRunStateValue.TabIndex = 12;
            lblCardRunStateValue.Text = "label2";
            // 
            // lblCardRunStateTitle
            // 
            lblCardRunStateTitle.AutoSize = true;
            lblCardRunStateTitle.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblCardRunStateTitle.ForeColor = SystemColors.ControlDarkDark;
            lblCardRunStateTitle.Location = new Point(3, 10);
            lblCardRunStateTitle.Name = "lblCardRunStateTitle";
            lblCardRunStateTitle.Size = new Size(124, 28);
            lblCardRunStateTitle.TabIndex = 11;
            lblCardRunStateTitle.Text = "采集状态";
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 214);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(grpDevice);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(splitContent);
            splitMain.Size = new Size(2447, 707);
            splitMain.SplitterDistance = 384;
            splitMain.TabIndex = 10;
            // 
            // grpDevice
            // 
            grpDevice.Controls.Add(flowDeviceCards);
            grpDevice.Dock = DockStyle.Fill;
            grpDevice.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpDevice.Location = new Point(0, 0);
            grpDevice.Margin = new Padding(4, 5, 4, 5);
            grpDevice.MinimumSize = new Size(1, 1);
            grpDevice.Name = "grpDevice";
            grpDevice.Padding = new Padding(0, 32, 0, 0);
            grpDevice.Size = new Size(384, 707);
            grpDevice.TabIndex = 0;
            grpDevice.TabStop = false;
            grpDevice.Text = "设备列表";
            grpDevice.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // flowDeviceCards
            // 
            flowDeviceCards.AutoScroll = true;
            flowDeviceCards.Dock = DockStyle.Fill;
            flowDeviceCards.FlowDirection = FlowDirection.TopDown;
            flowDeviceCards.Location = new Point(0, 32);
            flowDeviceCards.Name = "flowDeviceCards";
            flowDeviceCards.Padding = new Padding(8);
            flowDeviceCards.Size = new Size(384, 675);
            flowDeviceCards.TabIndex = 0;
            flowDeviceCards.WrapContents = false;
            // 
            // splitContent
            // 
            splitContent.Dock = DockStyle.Fill;
            splitContent.Location = new Point(0, 0);
            splitContent.Name = "splitContent";
            // 
            // splitContent.Panel1
            // 
            splitContent.Panel1.Controls.Add(grpTags);
            // 
            // splitContent.Panel2
            // 
            splitContent.Panel2.Controls.Add(grpAlarm);
            splitContent.Size = new Size(2059, 707);
            splitContent.SplitterDistance = 1182;
            splitContent.TabIndex = 0;
            // 
            // grpTags
            // 
            grpTags.Controls.Add(splitTags);
            grpTags.Dock = DockStyle.Fill;
            grpTags.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpTags.Location = new Point(0, 0);
            grpTags.Margin = new Padding(4, 5, 4, 5);
            grpTags.MinimumSize = new Size(1, 1);
            grpTags.Name = "grpTags";
            grpTags.Padding = new Padding(0, 32, 0, 0);
            grpTags.Size = new Size(1182, 707);
            grpTags.TabIndex = 0;
            grpTags.TabStop = false;
            grpTags.Text = "实时点位";
            grpTags.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // splitTags
            // 
            splitTags.Dock = DockStyle.Fill;
            splitTags.Location = new Point(0, 32);
            splitTags.MinimumSize = new Size(20, 20);
            splitTags.Name = "splitTags";
            splitTags.Orientation = Orientation.Horizontal;
            // 
            // splitTags.Panel1
            // 
            splitTags.Panel1.Controls.Add(flowTagCards);
            // 
            // splitTags.Panel2
            // 
            splitTags.Panel2.Controls.Add(dgvTags);
            splitTags.Size = new Size(1182, 675);
            splitTags.SplitterDistance = 180;
            splitTags.SplitterWidth = 11;
            splitTags.TabIndex = 3;
            // 
            // flowTagCards
            // 
            flowTagCards.Dock = DockStyle.Fill;
            flowTagCards.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            flowTagCards.Location = new Point(0, 0);
            flowTagCards.Margin = new Padding(4, 5, 4, 5);
            flowTagCards.MinimumSize = new Size(1, 1);
            flowTagCards.Name = "flowTagCards";
            flowTagCards.Padding = new Padding(2);
            flowTagCards.ShowText = false;
            flowTagCards.Size = new Size(1182, 180);
            flowTagCards.TabIndex = 0;
            flowTagCards.Text = "uiFlowLayoutPanel1";
            flowTagCards.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // grpAlarm
            // 
            grpAlarm.Controls.Add(dgvAlarms);
            grpAlarm.Dock = DockStyle.Fill;
            grpAlarm.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpAlarm.Location = new Point(0, 0);
            grpAlarm.Margin = new Padding(4, 5, 4, 5);
            grpAlarm.MinimumSize = new Size(1, 1);
            grpAlarm.Name = "grpAlarm";
            grpAlarm.Padding = new Padding(0, 32, 0, 0);
            grpAlarm.Size = new Size(873, 707);
            grpAlarm.TabIndex = 0;
            grpAlarm.TabStop = false;
            grpAlarm.Text = "当前报警";
            grpAlarm.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2447, 958);
            Controls.Add(splitMain);
            Controls.Add(panelDashboard);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTags).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAlarms).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panelDashboard.ResumeLayout(false);
            pnlAlarmBanner.ResumeLayout(false);
            pnlCardDbState.ResumeLayout(false);
            pnlCardDbState.PerformLayout();
            pnlCardAlarmCount.ResumeLayout(false);
            pnlCardAlarmCount.PerformLayout();
            pnlCardDeviceState.ResumeLayout(false);
            pnlCardDeviceState.PerformLayout();
            pnlCardRunState.ResumeLayout(false);
            pnlCardRunState.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            grpDevice.ResumeLayout(false);
            splitContent.Panel1.ResumeLayout(false);
            splitContent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContent).EndInit();
            splitContent.ResumeLayout(false);
            grpTags.ResumeLayout(false);
            splitTags.Panel1.ResumeLayout(false);
            splitTags.Panel2.ResumeLayout(false);
            (splitTags).EndInit();
            splitTags.ResumeLayout(false);
            grpAlarm.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Sunny.UI.UIDataGridView dgvTags;
        private System.Windows.Forms.Timer timerRefresh;
        private Sunny.UI.UIDataGridView dgvAlarms;      
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 系统ToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton start_button;
        private ToolStripButton stop_button;
        private ToolStripButton btnOpenHistory;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabelRunState;
        private ToolStripStatusLabel toolStripStatusLabelUser;
        private ToolStripStatusLabel toolStripStatusLabelBadCount;
        private ToolStripStatusLabel toolStripStatusLabelLastUpdate;
        private Sunny.UI.UIPanel panelDashboard;
        private SplitContainer splitMain;
        private Sunny.UI.UIGroupBox grpDevice;
        private FlowLayoutPanel flowDeviceCards;
        private SplitContainer splitContent;
        private Sunny.UI.UIGroupBox grpTags;
        private Sunny.UI.UIGroupBox grpAlarm;
        private Sunny.UI.UIPanel pnlCardDbState;
        private Sunny.UI.UIPanel pnlCardAlarmCount;
        private Sunny.UI.UIPanel pnlCardDeviceState;
        private Sunny.UI.UIPanel pnlCardRunState;
        private Sunny.UI.UILabel lblCardRunStateTitle;
        private Sunny.UI.UILabel lblCardDbStateTitle;
        private Sunny.UI.UILabel lblCardAlarmCountValue;
        private Sunny.UI.UILabel lblCardAlarmCountTitle;
        private Sunny.UI.UILabel lblCardCommQualityValue;
        private Sunny.UI.UILabel lblCardCommQualityTitle;
        private Sunny.UI.UILabel lblCardRunStateValue;
        private Sunny.UI.UILabel lblCardDbStateValue;
        private ToolStripMenuItem 配置管理ToolStripMenuItem;
        private ToolStripLabel toolStripLabelDeviceFilter;
        private ToolStripComboBox toolStripCboDeviceFilter;
        private Sunny.UI.UISplitContainer splitTags;
        private Sunny.UI.UIFlowLayoutPanel flowTagCards;
        private Sunny.UI.UIPanel pnlAlarmBanner;
        private Sunny.UI.UILabel lblAlarmBanner;
    }
}
