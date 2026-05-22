using FactorySupervisor.src.Contracts.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactorySupervisor
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _authService;

        public LoginForm(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            if (_authService.Login(username, password))
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            lblError.Text = "用户名或密码错误";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
            Close();
        }
    }
}
