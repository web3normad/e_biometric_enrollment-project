using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIMASASEAFARERS
{
    public partial class EditSuperAdmin : UserControl
    {
        public EditSuperAdmin()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtUname.Text == "")
            {
                Verify.Input();
            }

            else
                DbConnection.con.Open();
            SqlCommand cmd = new SqlCommand("select Fname, Lname, Uname, Role,Email,Pnum from AdminReg where Uname='" + txtUname.Text + "'", DbConnection.con);
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {


                txtFname.Text = srd.GetValue(0).ToString();
                Lname.Text = srd.GetValue(1).ToString();
                txtUser.Text = srd.GetValue(2).ToString();

                txtRole.Text = srd.GetValue(3).ToString();

                txtEmail.Text = srd.GetValue(4).ToString();
                txtPhone.Text = srd.GetValue(5).ToString();

            }
            DbConnection.con.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DbConnection.con.Open();
            SqlCommand cmd = DbConnection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from AdminReg  Where Uname= '" + txtUname.Text + "'";
            cmd.ExecuteNonQuery();
            DbConnection.con.Close();
            MessageBox.Show("Record Successfully deleted");
            Reset();
            gridView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DbConnection.con.Open();
            SqlCommand cmd = DbConnection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update AdminReg set Fname='" + txtFname.Text + "', LName='" + Lname.Text + "',Uname='" + txtUser.Text + "',Email='" + txtEmail.Text + "',Pnum='" + txtPhone.Text + "',Role='" + txtRole.Text + "' Where Fname= '" + txtUname.Text + "'";
            cmd.ExecuteNonQuery();
            DbConnection.con.Close();
            MessageBox.Show("Record Successfully Updated");
            Reset();
            gridView();
        }

        #region fill to Datagrid view
        public void gridView()
        {
            SqlCommand cmd = new SqlCommand("select Fname as'First Name', Lname as 'Last Name', Uname as'User Name',Email as'email', Zone as'Zone', Pnum as 'Phone Number'From AdminReg order by ID asc", DbConnection.con);
            try
            {
                DbConnection.con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dataGridAdmin.DataSource = dt;
                da.Update(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DbConnection.con.Close();
        }
        #endregion

        #region Reser fields
        public void Reset()
        {
            txtFname.Text = "";
            Lname.Text = "";
            txtUser.Text = "";
            txtRole.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";


        }
        #endregion

        private void EditSuperAdmin_Load(object sender, EventArgs e)
        {
            gridView();
        }
    }

}
