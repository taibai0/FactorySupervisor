namespace FactorySupervisor
{
    partial class HistoryForm
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
            dgvHistory = new DataGridView();
            btnQueryAuditLog = new Button();
            btnQueryAlarmHistory = new Button();
            btnQueryTagHistory = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();
            // 
            // dgvHistory
            // 
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistory.Location = new Point(0, 228);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersWidth = 72;
            dgvHistory.Size = new Size(932, 547);
            dgvHistory.TabIndex = 3;
            // 
            // btnQueryAuditLog
            // 
            btnQueryAuditLog.Location = new Point(47, 33);
            btnQueryAuditLog.Name = "btnQueryAuditLog";
            btnQueryAuditLog.Size = new Size(125, 97);
            btnQueryAuditLog.TabIndex = 2;
            btnQueryAuditLog.Text = "查询审计日志";
            btnQueryAuditLog.UseVisualStyleBackColor = true;
            btnQueryAuditLog.Click += btnQueryAuditLog_Click;
            // 
            // btnQueryAlarmHistory
            // 
            btnQueryAlarmHistory.Location = new Point(479, 33);
            btnQueryAlarmHistory.Name = "btnQueryAlarmHistory";
            btnQueryAlarmHistory.Size = new Size(138, 103);
            btnQueryAlarmHistory.TabIndex = 1;
            btnQueryAlarmHistory.Text = "查询报警历史数据";
            btnQueryAlarmHistory.UseVisualStyleBackColor = true;
            btnQueryAlarmHistory.Click += btnQueryAlarmHistory_Click;
            // 
            // btnQueryTagHistory
            // 
            btnQueryTagHistory.Location = new Point(230, 27);
            btnQueryTagHistory.Name = "btnQueryTagHistory";
            btnQueryTagHistory.Size = new Size(139, 103);
            btnQueryTagHistory.TabIndex = 0;
            btnQueryTagHistory.Text = "查询点位历史数据";
            btnQueryTagHistory.UseVisualStyleBackColor = true;
            btnQueryTagHistory.Click += btnQueryTagHistory_Click;
            // 
            // HistoryForm
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1198, 775);
            Controls.Add(btnQueryAuditLog);
            Controls.Add(btnQueryAlarmHistory);
            Controls.Add(btnQueryTagHistory);
            Controls.Add(dgvHistory);
            Name = "HistoryForm";
            Text = "HistoryForm";
            Load += HistoryForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dgvHistory;
        private Button btnQueryAuditLog;
        private Button btnQueryAlarmHistory;
        private Button btnQueryTagHistory;
    }
}