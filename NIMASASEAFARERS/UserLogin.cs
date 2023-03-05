using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace NIMASASEAFARERS
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        #region LogIN
        private void Login()
        {
            DbConnection.checkConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from UserReg where Uname='" + this.textBoxUsername.Text + "' AND Upass='" + this.textBoxPassword.Text + "';", DbConnection.con);
                SqlDataReader mreader;
                DbConnection.con.Open();
                mreader = cmd.ExecuteReader();
                int count = 0;
                while (mreader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    AdminDashboard2 formMainMenu2 = new AdminDashboard2();
                    formMainMenu2.Show();
                    this.Hide();
                }
                else
                {
                    Verify.InvalidUser();
                }
                DbConnection.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            textBoxUsername.Text = "";
            textBoxPassword.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.PasswordChar == '\0')
            {
                btnView.BringToFront();
                textBoxPassword.PasswordChar = '*';
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {

            if (textBoxPassword.PasswordChar == '*')
            {
                btnHide.BringToFront();
                textBoxPassword.PasswordChar = '\0';
            }
        }

        private void Admin_CheckedChanged(object sender, EventArgs e)
        {
            if (Admin.Checked)
            {
                AdminLogin adminlogin = new AdminLogin();
                adminlogin.Visible = true;
                this.Hide();



            }
        }

        private void User_CheckedChanged(object sender, EventArgs e)
        {
            if (User.Checked)
            {
                UserLogin userlogin = new UserLogin();
                userlogin.Visible = true;
                this.Hide();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
