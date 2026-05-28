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
            dgvDevices = new DataGridView();
            tabTags = new TabPage();
            dgvTags = new DataGridView();
            tabAlarmRules = new TabPage();
            dgvAlarmRules = new DataGridView();
            tabConfig.SuspendLayout();
            tabDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).BeginInit();
            tabTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            tabAlarmRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlarmRules).BeginInit();
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
            tabDevices.Location = new Point(4, 37);
            tabDevices.Name = "tabDevices";
            tabDevices.Padding = new Padding(3);
            tabDevices.Size = new Size(2379, 1096);
            tabDevices.TabIndex = 0;
            tabDevices.Text = "设备配置";
            tabDevices.UseVisualStyleBackColor = true;
            // 
            // dgvDevices
            // 
            dgvDevices.AllowUserToAddRows = false;
            dgvDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDevices.Dock = DockStyle.Fill;
            dgvDevices.Location = new Point(3, 3);
            dgvDevices.Name = "dgvDevices";
            dgvDevices.ReadOnly = true;
            dgvDevices.RowHeadersWidth = 72;
            dgvDevices.Size = new Size(2373, 1090);
            dgvDevices.TabIndex = 0;
            // 
            // tabTags
            // 
            tabTags.Controls.Add(dgvTags);
            tabTags.Location = new Point(4, 37);
            tabTags.Name = "tabTags";
            tabTags.Padding = new Padding(3);
            tabTags.Size = new Size(1443, 782);
            tabTags.TabIndex = 1;
            tabTags.Text = "点位配置";
            tabTags.UseVisualStyleBackColor = true;
            // 
            // dgvTags
            // 
            dgvTags.AllowUserToAddRows = false;
            dgvTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTags.Dock = DockStyle.Fill;
            dgvTags.Location = new Point(3, 3);
            dgvTags.Name = "dgvTags";
            dgvTags.ReadOnly = true;
            dgvTags.RowHeadersWidth = 72;
            dgvTags.Size = new Size(1437, 776);
            dgvTags.TabIndex = 1;
            // 
            // tabAlarmRules
            // 
            tabAlarmRules.Controls.Add(dgvAlarmRules);
            tabAlarmRules.Location = new Point(4, 37);
            tabAlarmRules.Name = "tabAlarmRules";
            tabAlarmRules.Padding = new Padding(3);
            tabAlarmRules.Size = new Size(1443, 782);
            tabAlarmRules.TabIndex = 2;
            tabAlarmRules.Text = "报警规则";
            tabAlarmRules.UseVisualStyleBackColor = true;
            // 
            // dgvAlarmRules
            // 
            dgvAlarmRules.AllowUserToAddRows = false;
            dgvAlarmRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlarmRules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlarmRules.Dock = DockStyle.Fill;
            dgvAlarmRules.Location = new Point(3, 3);
            dgvAlarmRules.Name = "dgvAlarmRules";
            dgvAlarmRules.ReadOnly = true;
            dgvAlarmRules.RowHeadersWidth = 72;
            dgvAlarmRules.Size = new Size(1437, 776);
            dgvAlarmRules.TabIndex = 1;
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
            ((System.ComponentModel.ISupportInitialize)dgvDevices).EndInit();
            tabTags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTags).EndInit();
            tabAlarmRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAlarmRules).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabConfig;
        private TabPage tabDevices;
        private TabPage tabTags;
        private DataGridView dgvDevices;
        private TabPage tabAlarmRules;
        private DataGridView dgvTags;
        private DataGridView dgvAlarmRules;
    }
}