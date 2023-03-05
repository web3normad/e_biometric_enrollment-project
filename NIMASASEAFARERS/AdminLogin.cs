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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        #region LogIN
        private void Login()
        {
            DbConnection.checkConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from AdminReg where Uname='" + this.textBoxUsername.Text + "' AND Upass='" + this.textBoxPassword.Text + "';", DbConnection.con);
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
                    AdminDashboard formMainMenu = new AdminDashboard();
                    formMainMenu.Show();
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

      

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            textBoxUsername.Text="";
            textBoxPassword.Text= "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {

            textBoxUsername.Text = "";
            textBoxPassword.Text = "";

        }

        private void btnHide_Click(object sender, EventArgs e)
        {
        

            if (textBoxPassword.PasswordChar == '*')
            {
                btnHide.BringToFront();
                textBoxPassword.PasswordChar = '\0';
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.PasswordChar == '\0')
            {
                btnView.BringToFront();
                textBoxPassword.PasswordChar = '*';
            }
        }

        private void Admin_CheckedChanged(object sender, EventArgs e)
        {
            if (Admin.Checked)
            {
                AdminLogin adminlogin = new AdminLogin();
                adminlogin.Visible=true;
                
            }
        }

        private void User_CheckedChanged(object sender, EventArgs e)
        {
            if (User.Checked)
            {
                UserLogin userlogin = new UserLogin();
                userlogin.Visible = true;
            }
        }
    }
}
