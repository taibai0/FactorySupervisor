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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dgvTags = new DataGridView();
            timerRefresh = new System.Windows.Forms.Timer(components);
            dgvAlarms = new DataGridView();
            menuStrip1 = new MenuStrip();
            系统ToolStripMenuItem = new ToolStripMenuItem();
            退出ToolStripMenuItem = new ToolStripMenuItem();
            配置管理ToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            start_button = new ToolStripButton();
            stop_button = new ToolStripButton();
            btnOpenHistory = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelRunState = new ToolStripStatusLabel();
            toolStripStatusLabelUser = new ToolStripStatusLabel();
            toolStripStatusLabelBadCount = new ToolStripStatusLabel();
            toolStripStatusLabelLastUpdate = new ToolStripStatusLabel();
            splitMain = new SplitContainer();
            grpDevice = new GroupBox();
            pnlCardDbState = new Panel();
            lblCardDbStateValue = new Label();
            lblCardDbStateTitle = new Label();
            pnlCardAlarmCount = new Panel();
            lblCardAlarmCountValue = new Label();
            lblCardAlarmCountTitle = new Label();
            pnlCardDeviceState = new Panel();
            lblCardCommQualityValue = new Label();
            lblCardCommQualityTitle = new Label();
            pnlCardRunState = new Panel();
            lblCardRunStateValue = new Label();
            lblCardRunStateTitle = new Label();
            lblComPort = new Label();
            lblProtocol = new Label();
            lblDeviceName = new Label();
            splitContent = new SplitContainer();
            grpTags = new GroupBox();
            grpAlarm = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAlarms).BeginInit();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            grpDevice.SuspendLayout();
            pnlCardDbState.SuspendLayout();
            pnlCardAlarmCount.SuspendLayout();
            pnlCardDeviceState.SuspendLayout();
            pnlCardRunState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContent).BeginInit();
            splitContent.Panel1.SuspendLayout();
            splitContent.Panel2.SuspendLayout();
            splitContent.SuspendLayout();
            grpTags.SuspendLayout();
            grpAlarm.SuspendLayout();
            SuspendLayout();
            // 
            // dgvTags
            // 
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTags.Dock = DockStyle.Fill;
            dgvTags.Location = new Point(3, 30);
            dgvTags.Name = "dgvTags";
            dgvTags.ReadOnly = true;
            dgvTags.RowHeadersWidth = 72;
            dgvTags.Size = new Size(665, 733);
            dgvTags.TabIndex = 2;
            // 
            // timerRefresh
            // 
            timerRefresh.Tick += timerRefresh_Tick;
            // 
            // dgvAlarms
            // 
            dgvAlarms.AllowUserToAddRows = false;
            dgvAlarms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlarms.Dock = DockStyle.Fill;
            dgvAlarms.Location = new Point(3, 30);
            dgvAlarms.Name = "dgvAlarms";
            dgvAlarms.ReadOnly = true;
            dgvAlarms.RowHeadersWidth = 72;
            dgvAlarms.Size = new Size(487, 733);
            dgvAlarms.TabIndex = 5;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(28, 28);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 系统ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1391, 37);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            系统ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 退出ToolStripMenuItem, 配置管理ToolStripMenuItem });
            系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            系统ToolStripMenuItem.Size = new Size(72, 33);
            系统ToolStripMenuItem.Text = "系统";
            // 
            // 退出ToolStripMenuItem
            // 
            退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            退出ToolStripMenuItem.Size = new Size(315, 40);
            退出ToolStripMenuItem.Text = "退出";
            // 
            // 配置管理ToolStripMenuItem
            // 
            配置管理ToolStripMenuItem.Name = "配置管理ToolStripMenuItem";
            配置管理ToolStripMenuItem.Size = new Size(315, 40);
            配置管理ToolStripMenuItem.Text = "配置管理";
            配置管理ToolStripMenuItem.Click += 配置管理ToolStripMenuItem_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(28, 28);
            toolStrip1.Items.AddRange(new ToolStripItem[] { start_button, stop_button, btnOpenHistory });
            toolStrip1.Location = new Point(0, 37);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1391, 38);
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
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(28, 28);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelRunState, toolStripStatusLabelUser, toolStripStatusLabelBadCount, toolStripStatusLabelLastUpdate });
            statusStrip1.Location = new Point(0, 841);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1391, 37);
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
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 75);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(grpDevice);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(splitContent);
            splitMain.Size = new Size(1391, 766);
            splitMain.SplitterDistance = 219;
            splitMain.TabIndex = 10;
            // 
            // grpDevice
            // 
            grpDevice.Controls.Add(pnlCardDbState);
            grpDevice.Controls.Add(pnlCardAlarmCount);
            grpDevice.Controls.Add(pnlCardDeviceState);
            grpDevice.Controls.Add(pnlCardRunState);
            grpDevice.Controls.Add(lblComPort);
            grpDevice.Controls.Add(lblProtocol);
            grpDevice.Controls.Add(lblDeviceName);
            grpDevice.Dock = DockStyle.Fill;
            grpDevice.Location = new Point(0, 0);
            grpDevice.Name = "grpDevice";
            grpDevice.Size = new Size(219, 766);
            grpDevice.TabIndex = 0;
            grpDevice.TabStop = false;
            grpDevice.Text = "设备状态";
            // 
            // pnlCardDbState
            // 
            pnlCardDbState.BorderStyle = BorderStyle.Fixed3D;
            pnlCardDbState.Controls.Add(lblCardDbStateValue);
            pnlCardDbState.Controls.Add(lblCardDbStateTitle);
            pnlCardDbState.Location = new Point(12, 630);
            pnlCardDbState.Name = "pnlCardDbState";
            pnlCardDbState.Size = new Size(201, 114);
            pnlCardDbState.TabIndex = 14;
            // 
            // lblCardDbStateValue
            // 
            lblCardDbStateValue.AutoSize = true;
            lblCardDbStateValue.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblCardDbStateValue.Location = new Point(4, 72);
            lblCardDbStateValue.Name = "lblCardDbStateValue";
            lblCardDbStateValue.Size = new Size(90, 33);
            lblCardDbStateValue.TabIndex = 13;
            lblCardDbStateValue.Text = "label8";
            // 
            // lblCardDbStateTitle
            // 
            lblCardDbStateTitle.AutoSize = true;
            lblCardDbStateTitle.ForeColor = SystemColors.ControlDarkDark;
            lblCardDbStateTitle.Location = new Point(3, 16);
            lblCardDbStateTitle.Name = "lblCardDbStateTitle";
            lblCardDbStateTitle.Size = new Size(75, 28);
            lblCardDbStateTitle.TabIndex = 12;
            lblCardDbStateTitle.Text = "数据库";
            // 
            // pnlCardAlarmCount
            // 
            pnlCardAlarmCount.BorderStyle = BorderStyle.Fixed3D;
            pnlCardAlarmCount.Controls.Add(lblCardAlarmCountValue);
            pnlCardAlarmCount.Controls.Add(lblCardAlarmCountTitle);
            pnlCardAlarmCount.Location = new Point(12, 482);
            pnlCardAlarmCount.Name = "pnlCardAlarmCount";
            pnlCardAlarmCount.Size = new Size(201, 109);
            pnlCardAlarmCount.TabIndex = 13;
            // 
            // lblCardAlarmCountValue
            // 
            lblCardAlarmCountValue.AutoSize = true;
            lblCardAlarmCountValue.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblCardAlarmCountValue.Location = new Point(4, 73);
            lblCardAlarmCountValue.Name = "lblCardAlarmCountValue";
            lblCardAlarmCountValue.Size = new Size(90, 33);
            lblCardAlarmCountValue.TabIndex = 13;
            lblCardAlarmCountValue.Text = "label6";
            // 
            // lblCardAlarmCountTitle
            // 
            lblCardAlarmCountTitle.AutoSize = true;
            lblCardAlarmCountTitle.ForeColor = SystemColors.ControlDarkDark;
            lblCardAlarmCountTitle.Location = new Point(3, 14);
            lblCardAlarmCountTitle.Name = "lblCardAlarmCountTitle";
            lblCardAlarmCountTitle.Size = new Size(96, 28);
            lblCardAlarmCountTitle.TabIndex = 12;
            lblCardAlarmCountTitle.Text = "当前警报";
            // 
            // pnlCardDeviceState
            // 
            pnlCardDeviceState.BorderStyle = BorderStyle.Fixed3D;
            pnlCardDeviceState.Controls.Add(lblCardCommQualityValue);
            pnlCardDeviceState.Controls.Add(lblCardCommQualityTitle);
            pnlCardDeviceState.Location = new Point(12, 342);
            pnlCardDeviceState.Name = "pnlCardDeviceState";
            pnlCardDeviceState.Size = new Size(201, 107);
            pnlCardDeviceState.TabIndex = 12;
            // 
            // lblCardCommQualityValue
            // 
            lblCardCommQualityValue.AutoSize = true;
            lblCardCommQualityValue.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblCardCommQualityValue.Location = new Point(3, 65);
            lblCardCommQualityValue.Name = "lblCardCommQualityValue";
            lblCardCommQualityValue.Size = new Size(90, 33);
            lblCardCommQualityValue.TabIndex = 13;
            lblCardCommQualityValue.Text = "label4";
            // 
            // lblCardCommQualityTitle
            // 
            lblCardCommQualityTitle.AutoSize = true;
            lblCardCommQualityTitle.ForeColor = SystemColors.ControlDarkDark;
            lblCardCommQualityTitle.Location = new Point(3, 15);
            lblCardCommQualityTitle.Name = "lblCardCommQualityTitle";
            lblCardCommQualityTitle.Size = new Size(96, 28);
            lblCardCommQualityTitle.TabIndex = 12;
            lblCardCommQualityTitle.Text = "通信质量";
            // 
            // pnlCardRunState
            // 
            pnlCardRunState.BorderStyle = BorderStyle.Fixed3D;
            pnlCardRunState.Controls.Add(lblCardRunStateValue);
            pnlCardRunState.Controls.Add(lblCardRunStateTitle);
            pnlCardRunState.Location = new Point(12, 199);
            pnlCardRunState.Name = "pnlCardRunState";
            pnlCardRunState.Size = new Size(201, 111);
            pnlCardRunState.TabIndex = 11;
            // 
            // lblCardRunStateValue
            // 
            lblCardRunStateValue.AutoSize = true;
            lblCardRunStateValue.Font = new Font("Microsoft YaHei UI", 10.7142859F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblCardRunStateValue.Location = new Point(-1, 76);
            lblCardRunStateValue.Name = "lblCardRunStateValue";
            lblCardRunStateValue.Size = new Size(90, 33);
            lblCardRunStateValue.TabIndex = 12;
            lblCardRunStateValue.Text = "label2";
            // 
            // lblCardRunStateTitle
            // 
            lblCardRunStateTitle.AutoSize = true;
            lblCardRunStateTitle.ForeColor = SystemColors.ControlDarkDark;
            lblCardRunStateTitle.Location = new Point(3, 10);
            lblCardRunStateTitle.Name = "lblCardRunStateTitle";
            lblCardRunStateTitle.Size = new Size(96, 28);
            lblCardRunStateTitle.TabIndex = 11;
            lblCardRunStateTitle.Text = "采集状态";
            // 
            // lblComPort
            // 
            lblComPort.AutoSize = true;
            lblComPort.Location = new Point(12, 138);
            lblComPort.Name = "lblComPort";
            lblComPort.Size = new Size(126, 28);
            lblComPort.TabIndex = 0;
            lblComPort.Text = "lblComPort";
            // 
            // lblProtocol
            // 
            lblProtocol.AutoSize = true;
            lblProtocol.Location = new Point(13, 89);
            lblProtocol.Name = "lblProtocol";
            lblProtocol.Size = new Size(122, 28);
            lblProtocol.TabIndex = 1;
            lblProtocol.Text = "lblProtocol";
            // 
            // lblDeviceName
            // 
            lblDeviceName.AutoSize = true;
            lblDeviceName.Location = new Point(12, 44);
            lblDeviceName.Name = "lblDeviceName";
            lblDeviceName.Size = new Size(166, 28);
            lblDeviceName.TabIndex = 0;
            lblDeviceName.Text = "lblDeviceName";
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
            splitContent.Size = new Size(1168, 766);
            splitContent.SplitterDistance = 671;
            splitContent.TabIndex = 0;
            // 
            // grpTags
            // 
            grpTags.Controls.Add(dgvTags);
            grpTags.Dock = DockStyle.Fill;
            grpTags.Location = new Point(0, 0);
            grpTags.Name = "grpTags";
            grpTags.Size = new Size(671, 766);
            grpTags.TabIndex = 0;
            grpTags.TabStop = false;
            grpTags.Text = "实时点位";
            // 
            // grpAlarm
            // 
            grpAlarm.Controls.Add(dgvAlarms);
            grpAlarm.Dock = DockStyle.Fill;
            grpAlarm.Location = new Point(0, 0);
            grpAlarm.Name = "grpAlarm";
            grpAlarm.Size = new Size(493, 766);
            grpAlarm.TabIndex = 0;
            grpAlarm.TabStop = false;
            grpAlarm.Text = "当前报警";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1391, 878);
            Controls.Add(splitMain);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTags).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAlarms).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            grpDevice.ResumeLayout(false);
            grpDevice.PerformLayout();
            pnlCardDbState.ResumeLayout(false);
            pnlCardDbState.PerformLayout();
            pnlCardAlarmCount.ResumeLayout(false);
            pnlCardAlarmCount.PerformLayout();
            pnlCardDeviceState.ResumeLayout(false);
            pnlCardDeviceState.PerformLayout();
            pnlCardRunState.ResumeLayout(false);
            pnlCardRunState.PerformLayout();
            splitContent.Panel1.ResumeLayout(false);
            splitContent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContent).EndInit();
            splitContent.ResumeLayout(false);
            grpTags.ResumeLayout(false);
            grpAlarm.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvTags;
        private System.Windows.Forms.Timer timerRefresh;
        private DataGridView dgvAlarms;      
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
        private SplitContainer splitMain;
        private GroupBox grpDevice;
        private SplitContainer splitContent;
        private Label lblComPort;
        private Label lblProtocol;
        private Label lblDeviceName;
        private GroupBox grpTags;
        private GroupBox grpAlarm;
        private Panel pnlCardDbState;
        private Panel pnlCardAlarmCount;
        private Panel pnlCardDeviceState;
        private Panel pnlCardRunState;
        private Label lblCardRunStateTitle;
        private Label lblCardDbStateTitle;
        private Label lblCardAlarmCountValue;
        private Label lblCardAlarmCountTitle;
        private Label lblCardCommQualityValue;
        private Label lblCardCommQualityTitle;
        private Label lblCardRunStateValue;
        private Label lblCardDbStateValue;
        private ToolStripMenuItem 配置管理ToolStripMenuItem;
    }
}
