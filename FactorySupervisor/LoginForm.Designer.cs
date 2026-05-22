namespace FactorySupervisor
{
    partial class LoginForm
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
            btnLogin = new Button();
            lblUsername = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnCancel = new Button();
            lblError = new Label();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(119, 274);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(131, 40);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "登录";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(119, 86);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(75, 28);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "用户名";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(119, 170);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(54, 28);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "密码";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(233, 86);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(175, 34);
            txtUsername.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(233, 170);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(175, 34);
            txtPassword.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(315, 274);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(131, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.ForeColor = Color.IndianRed;
            lblError.Location = new Point(119, 387);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 28);
            lblError.TabIndex = 6;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblError);
            Controls.Add(btnCancel);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Controls.Add(btnLogin);
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogin;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnCancel;
        private Label lblError;
    }
}