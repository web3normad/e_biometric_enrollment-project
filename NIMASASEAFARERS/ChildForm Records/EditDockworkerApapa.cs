using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIMASASEAFARERS.ChildForm_Records
{
    public partial class EditDockworkerApapa : UserControl
    {
        public EditDockworkerApapa()
        {
            InitializeComponent();
        }

        #region Reser fields
        public void Reset()
        {
            txtRegNo.Text = "";
            txtSurname.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtDOB.Text = "";
            txtGender.Text = "";
            txtHeight.Text = "";
            txtNIN.Text = "";
            txtEmail.Text = "";
            txtAge.Text = "";
            txtState.Text = "";
            txtZone.Text = "";
            txtAddress.Text = "";
            txtNOK.Text = "";
            txtPhone.Text = "";
            textGeno.Text = "";
            textBloodGrp.Text = "";
            txtCompany.Text = "";
            txtLocation.Text = "";
            txtRank.Text = "";
            Passport.Image = null;
            picBarcode.Image = null;

        }
        #endregion

        private void btnFind_Click(object sender, EventArgs e)
        {

            if (txtRegNo.Text == "")
            {
                Verify.Input();
            }

            else
                DbConnection.con.Open();
            SqlCommand cmd = new SqlCommand("select Surname, FirstName, MiddleName, DOB,Gender, Age, Height, NIN, NOK, Email, Mobile, State, Address, Company, Location, Zone, rank, BarcodeImg, DockworkersPassport.Passport from Dockworkers inner join DockworkersPassport on Dockworkers.DockworkerID=DockworkerSPassport.CNIC where DockworkerID='" + txtRegNo.Text + "'", DbConnection.con);
            SqlDataReader srd = cmd.ExecuteReader();
            while (srd.Read())
            {


                txtSurname.Text = srd.GetValue(0).ToString();
                txtFirstName.Text = srd.GetValue(1).ToString();
                txtMiddleName.Text = srd.GetValue(2).ToString();

                txtDOB.Text = srd.GetValue(3).ToString();

                txtGender.Text = srd.GetValue(4).ToString();
                txtAge.Text = srd.GetValue(5).ToString();
                txtHeight.Text = srd.GetValue(6).ToString();
                txtNIN.Text = srd.GetValue(7).ToString();
                txtNOK.Text = srd.GetValue(8).ToString();
                txtEmail.Text = srd.GetValue(9).ToString();
                txtPhone.Text = srd.GetValue(10).ToString();
                textGeno.Text = srd.GetValue(10).ToString();
                textBloodGrp.Text = srd.GetValue(10).ToString();
                txtState.Text = srd.GetValue(11).ToString();
                txtAddress.Text = srd.GetValue(12).ToString();
                txtCompany.Text = srd.GetValue(13).ToString();
                txtLocation.Text = srd.GetValue(14).ToString();

                txtZone.Text = srd.GetValue(15).ToString();
                txtRank.Text = srd.GetValue(16).ToString();



                MemoryStream ms = new MemoryStream((byte[])srd["Passport"]);
                Passport.Image = new Bitmap(ms);

                MemoryStream ms1 = new MemoryStream((byte[])srd["BarcodeImg"]);
                picBarcode.Image = new Bitmap(ms1);

                //Fmd.DeserializeXml(srd["FingertempR"].ToString());
                //Fmd.DeserializeXml(srd["FingertempL"].ToString());

            }
            DbConnection.con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DbConnection.con.Open();
            SqlCommand cmd = DbConnection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update Dockworkers set Surname='" + txtSurname.Text + "', FirstName='" + txtFirstName.Text + "',MiddleName='" + txtMiddleName.Text + "',DOB='" + txtDOB.Text + "', Gender='" + txtGender.Text + "',Age='" + txtAge.Text + "',Height='" + txtHeight.Text + "',NIN='" + txtNIN.Text + "',NOK='" + txtNOK.Text + "',Email='" + txtEmail.Text + "',Mobile='" + txtPhone.Text + "',State='" + txtState.Text + "',Address='" + txtAddress.Text + "',Company='" + txtCompany.Text + "',Location='" + txtLocation.Text + "',Zone='" + txtZone.Text + "',Rank='" + txtRank.Text + "' Where DockworkerID= '" + txtRegNo.Text + "'";
            cmd.ExecuteNonQuery();
            DbConnection.con.Close();
            MessageBox.Show("Record Successfully Updated");
            Reset();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DbConnection.con.Open();
            SqlCommand cmd = DbConnection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from Dockworkers  Where DockworkerID= '" + txtRegNo.Text + "'";
            cmd.ExecuteNonQuery();
            DbConnection.con.Close();
            MessageBox.Show("Record Successfully deleted");
            Reset();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            RecApapa rca = new RecApapa();
            MainControlClass.showControl(rca, content);
        }
    }
}
