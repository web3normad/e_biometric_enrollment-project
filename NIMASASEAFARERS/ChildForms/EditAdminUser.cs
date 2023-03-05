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
    public partial class EditAdminUser : UserControl
    {
        public EditAdminUser()
        {
            InitializeComponent();
        }

        private void iDelete()
        {
            foreach (DataGridViewRow item in this.dataGridAdmin.SelectedRows)
            {
                dataGridAdmin.Rows.RemoveAt(item.Index);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            iDelete();
        }

        private void dataGridAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #region fill to Datagrid view
        public void gridView()
        {
            SqlCommand cmd = new SqlCommand("select Fname as'First Name', Lname as 'Last Name', Uname as'User Name',Email as'email', Zone as'Zone', Phone as 'Phone Number'From UserReg order by ID asc", DbConnection.con);
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



        private void EditAdminUser_Load(object sender, EventArgs e)
        {
            gridView();
            
        }

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

        private void btnDel_Click(object sender, EventArgs e)
        {
            DbConnection.con.Open();
            SqlCommand cmd = DbConnection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from UserReg  Where Uname= '" + txtUname.Text + "'";
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
            cmd.CommandText = "Update UserReg set Fname='" + txtFname.Text + "', LName='" + Lname.Text + "',Uname='" + txtUser.Text + "',Email='" + txtEmail.Text + "',Phone='" + txtPhone.Text + "',Role='" + txtRole.Text + "' Where Fname= '" + txtUname.Text + "'";
            cmd.ExecuteNonQuery();
            DbConnection.con.Close();
            MessageBox.Show("Record Successfully Updated");
            Reset();
            gridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (txtUname.Text == "")
            {
                Verify.Input();
            }

            else
                DbConnection.con.Open();
            SqlCommand cmd = new SqlCommand("select Fname, Lname, Uname, Role,Email,Phone from UserReg where Uname='" + txtUname.Text + "'", DbConnection.con);
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
    }
}
