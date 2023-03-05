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

namespace NIMASASEAFARERS.ChildForms
{
    public partial class SuperAdmin : UserControl
    {
        public SuperAdmin()
        {
            InitializeComponent();
        }

        #region Create Admin
        private void CreateUser()
        {
            DbConnection.checkConnection();
            if (textName.Text == "" || textLName.Text == "" || textEmail.Text == "" || textPnum.Text == "" || textUsername.Text == "" || textPassword.Text == "" || textCPassword.Text == "")
            {
                MessageBox.Show("Please fill all fields", "Some field empty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (textPassword.Text != textCPassword.Text)
            {
                MessageBox.Show("Both password must be match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                SqlCommand cmdChk = new SqlCommand("select NIN from Dockworkers where NIN='" + textEmail.Text + "';", DbConnection.con);
                SqlParameter param = new SqlParameter();
                if (DbConnection.con.State != ConnectionState.Open)
                {
                    DbConnection.con.Open();
                }
                param.Value = this.textPnum.Text;
                cmdChk.Parameters.Add(param);
                SqlDataReader read = cmdChk.ExecuteReader();
                if (read.HasRows)
                {
                    DbConnection.con.Close();
                    Verify.Duplicate();
                }
                else
                {
                    try
                    {
                        DbConnection.con.Close();
                        if (DbConnection.con.State != ConnectionState.Open)
                        {
                            DbConnection.con.Open();
                        }

                        SqlCommand cmd = new SqlCommand();

                        SqlCommand comd = new SqlCommand("insert into [AdminReg] (Fname, Lname, Email, Pnum,Uname, Zone, Upass)values (@firstname,@lastname,@email,@phone,@username,@zone,@password)", DbConnection.con);
                        comd.Parameters.Add(new SqlParameter("@firstname", textName.Text));
                        comd.Parameters.Add(new SqlParameter("@lastname", textLName.Text));
                        comd.Parameters.Add(new SqlParameter("@email", textEmail.Text));
                        comd.Parameters.Add(new SqlParameter("@phone", textPnum.Text));
                        comd.Parameters.Add(new SqlParameter("@username", textUsername.Text));
                        comd.Parameters.Add(new SqlParameter("@zone", textZone.Text));
                        comd.Parameters.Add(new SqlParameter("@password", textPassword.Text));

                        if (DbConnection.con.State == System.Data.ConnectionState.Closed)
                        {
                            DbConnection.con.Open();
                        }
                        comd.ExecuteNonQuery();

                        Verify.Save();
                        DbConnection.con.Close();

                        Reset();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        public void Reset()
        {
            textName.Text = "";
            textLName.Text = "";
            textUsername.Text = "";
            textEmail.Text = "";
            textPnum.Text = "";
            textZone.Text = "";
            textPassword.Text = "";
            textCPassword.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CreateUser();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {

            if (textPassword.PasswordChar == '\0')
            {
                btnView.BringToFront();
                textPassword.PasswordChar = '*';
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (textPassword.PasswordChar == '*')
            {
                btnHide.BringToFront();
                textPassword.PasswordChar = '\0';
            }
        }

        private void btnHide2_Click(object sender, EventArgs e)
        {
            if (textCPassword.PasswordChar == '\0')
            {
                btnView.BringToFront();
                textCPassword.PasswordChar = '*';
            }
        }

        private void btnView2_Click(object sender, EventArgs e)
        {
            if (textPassword.PasswordChar == '*')
            {
                btnHide.BringToFront();
                textPassword.PasswordChar = '\0';
            }
        }
    }
}
