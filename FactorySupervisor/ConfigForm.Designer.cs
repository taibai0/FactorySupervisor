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
            tabConfig = new TabControl();
            tabDevices = new TabPage();
            panelDeviceActions = new Panel();
            btnSaveDevices = new Button();
            dgvDevices = new DataGridView();
            tabTags = new TabPage();
            dgvTags = new DataGridView();
            tabAlarmRules = new TabPage();
            panelAlarmActions = new Panel();
            btnAlarmRuleSave = new Button();
            dgvAlarmRules = new DataGridView();
            btnSaveTags = new Button();
            panelTagActions = new Panel();
            tabConfig.SuspendLayout();
            tabDevices.SuspendLayout();
            panelDeviceActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).BeginInit();
            tabTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            tabAlarmRules.SuspendLayout();
            panelAlarmActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlarmRules).BeginInit();
            panelTagActions.SuspendLayout();
            SuspendLayout();
            // 
            // tabConfig
            // 
            tabConfig.Controls.Add(tabDevices);
            tabConfig.Controls.Add(tabTags);
            tabConfig.Controls.Add(tabAlarmRules);
            tabConfig.Dock = DockStyle.Fill;
            tabConfig.Location = new Point(0, 0);
            tabConfig.Name = "tabConfig";
            tabConfig.SelectedIndex = 0;
            tabConfig.Size = new Size(2387, 1137);
            tabConfig.TabIndex = 0;
            // 
            // tabDevices
            // 
            tabDevices.Controls.Add(dgvDevices);
            tabDevices.Controls.Add(panelDeviceActions);
            tabDevices.Location = new Point(4, 37);
            tabDevices.Name = "tabDevices";
            tabDevices.Padding = new Padding(3);
            tabDevices.Size = new Size(2379, 1096);
            tabDevices.TabIndex = 0;
            tabDevices.Text = "设备配置";
            tabDevices.UseVisualStyleBackColor = true;
            // 
            // panelDeviceActions
            // 
            panelDeviceActions.Controls.Add(btnSaveDevices);
            panelDeviceActions.Dock = DockStyle.Top;
            panelDeviceActions.Location = new Point(3, 3);
            panelDeviceActions.Name = "panelDeviceActions";
            panelDeviceActions.Size = new Size(2373, 45);
            panelDeviceActions.TabIndex = 1;
            // 
            // btnSaveDevices
            // 
            btnSaveDevices.BackColor = Color.Salmon;
            btnSaveDevices.Location = new Point(2204, 3);
            btnSaveDevices.Name = "btnSaveDevices";
            btnSaveDevices.Size = new Size(166, 39);
            btnSaveDevices.TabIndex = 0;
            btnSaveDevices.Text = "保存设备配置";
            btnSaveDevices.UseVisualStyleBackColor = false;
            btnSaveDevices.Click += btnSaveDevices_Click;
            // 
            // dgvDevices
            // 
            dgvDevices.AllowUserToAddRows = false;
            dgvDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDevices.Dock = DockStyle.Fill;
            dgvDevices.Location = new Point(3, 3);
            dgvDevices.Name = "dgvDevices";
            dgvDevices.RowHeadersWidth = 72;
            dgvDevices.Size = new Size(2373, 1090);
            dgvDevices.TabIndex = 0;
            dgvDevices.CellValueChanged += dgvDevices_CellValueChanged;
            dgvDevices.CurrentCellDirtyStateChanged += dgvDevices_CurrentCellDirtyStateChanged;
            // 
            // tabTags
            // 
            tabTags.Controls.Add(dgvTags);
            tabTags.Controls.Add(panelTagActions);
            tabTags.Location = new Point(4, 37);
            tabTags.Name = "tabTags";
            tabTags.Padding = new Padding(3);
            tabTags.Size = new Size(2379, 1096);
            tabTags.TabIndex = 1;
            tabTags.Text = "点位配置";
            tabTags.UseVisualStyleBackColor = true;

            // 
            // panelTagActions
            // 
            panelTagActions.Controls.Add(btnSaveTags);
            panelTagActions.Dock = DockStyle.Top;
            panelTagActions.Location = new Point(3, 3);
            panelTagActions.Name = "panelTagActions";
            panelTagActions.Size = new Size(2373, 45);
            panelTagActions.TabIndex = 2;

            // 
            // dgvTags
            // 
            dgvTags.AllowUserToAddRows = false;
            dgvTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTags.Dock = DockStyle.Fill;
            dgvTags.Location = new Point(3, 48);
            dgvTags.Name = "dgvTags";
            dgvTags.RowHeadersWidth = 72;
            dgvTags.Size = new Size(2373, 1045);
            dgvTags.TabIndex = 1;
            dgvTags.CellBeginEdit += dgvConfig_CellBeginEdit;
            dgvTags.CellEndEdit += dgvConfig_CellEndEdit;

           
            // 
            // tabAlarmRules
            // 
            tabAlarmRules.Controls.Add(dgvAlarmRules);
            tabAlarmRules.Controls.Add(panelAlarmActions);
            tabAlarmRules.Location = new Point(4, 37);
            tabAlarmRules.Name = "tabAlarmRules";
            tabAlarmRules.Padding = new Padding(3);
            tabAlarmRules.Size = new Size(2379, 1096);
            tabAlarmRules.TabIndex = 2;
            tabAlarmRules.Text = "报警规则";
            tabAlarmRules.UseVisualStyleBackColor = true;
            // 
            // panelAlarmActions
            // 
            panelAlarmActions.Controls.Add(btnAlarmRuleSave);
            panelAlarmActions.Dock = DockStyle.Top;
            panelAlarmActions.Location = new Point(3, 3);
            panelAlarmActions.Name = "panelAlarmActions";
            panelAlarmActions.Size = new Size(2373, 45);
            panelAlarmActions.TabIndex = 2;
            // 
            // btnAlarmRuleSave
            // 
            btnAlarmRuleSave.BackColor = Color.LightCoral;
            btnAlarmRuleSave.Location = new Point(2189, 3);
            btnAlarmRuleSave.Name = "btnAlarmRuleSave";
            btnAlarmRuleSave.Size = new Size(179, 40);
            btnAlarmRuleSave.TabIndex = 0;
            btnAlarmRuleSave.Text = "保存报警规则";
            btnAlarmRuleSave.UseVisualStyleBackColor = false;
            btnAlarmRuleSave.Click += btnAlarmRuleSave_Click;
            // 
            // dgvAlarmRules
            // 
            dgvAlarmRules.AllowUserToAddRows = false;
            dgvAlarmRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlarmRules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlarmRules.Dock = DockStyle.Fill;
            dgvAlarmRules.Location = new Point(3, 3);
            dgvAlarmRules.Name = "dgvAlarmRules";
            dgvAlarmRules.RowHeadersWidth = 72;
            dgvAlarmRules.Size = new Size(2373, 1090);
            dgvAlarmRules.TabIndex = 1;
            dgvAlarmRules.CellValueChanged += dgvAlarmRules_CellValueChanged;
            dgvAlarmRules.CurrentCellDirtyStateChanged += dgvAlarmRules_CurrentCellDirtyStateChanged;
            dgvAlarmRules.CellBeginEdit += dgvConfig_CellBeginEdit;
            dgvAlarmRules.CellEndEdit += dgvConfig_CellEndEdit;
            // 
            // btnSaveTags
            // 
            btnSaveTags.BackColor = Color.LightCoral;
            btnSaveTags.Location = new Point(2170, 2);
            btnSaveTags.Name = "btnSaveTags";
            btnSaveTags.Size = new Size(189, 40);
            btnSaveTags.TabIndex = 0;
            btnSaveTags.Text = "保存点位配置";
            btnSaveTags.UseVisualStyleBackColor = false;
            btnSaveTags.Click += btnSaveTags_Click;
          
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2387, 1137);
            Controls.Add(tabConfig);
            Name = "ConfigForm";
            Text = "ConfigForm";
            Load += ConfigForm_Load;
            tabConfig.ResumeLayout(false);
            tabDevices.ResumeLayout(false);
            panelDeviceActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDevices).EndInit();
            tabTags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTags).EndInit();
            tabAlarmRules.ResumeLayout(false);
            panelAlarmActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAlarmRules).EndInit();
            panelTagActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void DgvTags_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private TabControl tabConfig;
        private TabPage tabDevices;
        private TabPage tabTags;
        private DataGridView dgvDevices;
        private TabPage tabAlarmRules;
        private DataGridView dgvTags;
        private DataGridView dgvAlarmRules;
        private Panel panelDeviceActions;
        private Button btnSaveDevices;
        private Panel panelAlarmActions;
        private Button btnAlarmRuleSave;
        private Panel panelTagActions;
        private Button btnSaveTags;
    }
}
