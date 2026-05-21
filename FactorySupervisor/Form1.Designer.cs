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
            start_button = new Button();
            stop_button = new Button();
            dgvTags = new DataGridView();
            lblStatus = new Label();
            timerRefresh = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            dgvAlarms = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAlarms).BeginInit();
            SuspendLayout();
            // 
            // start_button
            // 
            start_button.Location = new Point(20, 603);
            start_button.Name = "start_button";
            start_button.Size = new Size(183, 40);
            start_button.TabIndex = 0;
            start_button.Text = "启动采集服务";
            start_button.UseVisualStyleBackColor = true;
            start_button.Click += start_button_Click;
            // 
            // stop_button
            // 
            stop_button.Location = new Point(342, 603);
            stop_button.Name = "stop_button";
            stop_button.Size = new Size(183, 40);
            stop_button.TabIndex = 1;
            stop_button.Text = "停止采集服务";
            stop_button.UseVisualStyleBackColor = true;
            stop_button.Click += stop_button_Click;
            // 
            // dgvTags
            // 
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTags.Location = new Point(20, 11);
            dgvTags.Name = "dgvTags";
            dgvTags.ReadOnly = true;
            dgvTags.RowHeadersWidth = 72;
            dgvTags.Size = new Size(758, 362);
            dgvTags.TabIndex = 2;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblStatus.Location = new Point(403, 453);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(93, 36);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "status";
            // 
            // timerRefresh
            // 
            timerRefresh.Tick += timerRefresh_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 444);
            label1.Name = "label1";
            label1.Size = new Size(73, 28);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // dgvAlarms
            // 
            dgvAlarms.AllowUserToAddRows = false;
            dgvAlarms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlarms.Location = new Point(842, 12);
            dgvAlarms.Name = "dgvAlarms";
            dgvAlarms.ReadOnly = true;
            dgvAlarms.RowHeadersWidth = 72;
            dgvAlarms.Size = new Size(673, 361);
            dgvAlarms.TabIndex = 5;
            dgvAlarms.AutoGenerateColumns = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1645, 704);
            Controls.Add(dgvAlarms);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(dgvTags);
            Controls.Add(stop_button);
            Controls.Add(start_button);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTags).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAlarms).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button start_button;
        private Button stop_button;
        private DataGridView dgvTags;
        private Label lblStatus;
        private System.Windows.Forms.Timer timerRefresh;
        private Label label1;
        private DataGridView dgvAlarms;
    }
}
