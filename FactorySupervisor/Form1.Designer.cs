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
            dataGridView1 = new DataGridView();
            lblStatus = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // start_button
            // 
            start_button.Location = new Point(388, 393);
            start_button.Name = "start_button";
            start_button.Size = new Size(183, 40);
            start_button.TabIndex = 0;
            start_button.Text = "启动采集服务";
            start_button.UseVisualStyleBackColor = true;
            start_button.Click += start_button_Click;
            // 
            // stop_button
            // 
            stop_button.Location = new Point(701, 457);
            stop_button.Name = "stop_button";
            stop_button.Size = new Size(183, 40);
            stop_button.TabIndex = 1;
            stop_button.Text = "停止采集服务";
            stop_button.UseVisualStyleBackColor = true;
            stop_button.Click += stop_button_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(20, 11);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 72;
            dataGridView1.Size = new Size(1579, 362);
            dataGridView1.TabIndex = 2;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(247, 399);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(73, 28);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "label1";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(61, 399);
            label1.Name = "label1";
            label1.Size = new Size(73, 28);
            label1.TabIndex = 4;
            label1.Text = "label1";
            
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1645, 704);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(dataGridView1);
            Controls.Add(stop_button);
            Controls.Add(start_button);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button start_button;
        private Button stop_button;
        private DataGridView dataGridView1;
        private Label lblStatus;
        private System.Windows.Forms.Timer timer1;
        private Label label1;
    }
}
