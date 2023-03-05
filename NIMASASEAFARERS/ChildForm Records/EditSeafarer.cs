using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace NIMASASEAFARERS
{
    public partial class EditSeafarer : UserControl
    {
        public EditSeafarer()
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
            txtCompany.Text = "";
            txtLocation.Text = "";
            txtRank.Text = "";
            Passport.Image = null;
            BarcodeImg.Image = null;
          



        }
        #endregion
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtRegNo.Text == "")
            {
                Verify.Input();
            }

            else
                DbConnection.con.Open();
            SqlCommand cmd = new SqlCommand("select Surname, FirstName, MiddleName, DOB,Gender, Age, Height, NIN, NOK, Email, Mobile, State, Address, NIMASA_RegNo, Location, Zone, rank, Barcode, SeafarersPassport.Passport from Seafarers inner join SeafarersPassport on Seafarers.SeafarerID=SeafarersPassport.CNIC where SeafarerID='" + txtRegNo.Text + "'", DbConnection.con);
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
                txtState.Text = srd.GetValue(11).ToString();
                txtAddress.Text = srd.GetValue(12).ToString();
                txtCompany.Text = srd.GetValue(13).ToString();
                txtLocation.Text = srd.GetValue(14).ToString();

                txtZone.Text = srd.GetValue(15).ToString();
                txtRank.Text = srd.GetValue(16).ToString();


               
                MemoryStream ms = new MemoryStream((byte[])srd["Passport"]);
                Passport.Image = new Bitmap(ms);

                MemoryStream ms1 = new MemoryStream((byte[])srd["Barcode"]);
                BarcodeImg.Image = new Bitmap(ms1);

                //Fmd.DeserializeXml(srd["FingertempR"].ToString());
                //Fmd.DeserializeXml(srd["FingertempL"].ToString());

            }
            DbConnection.con.Close();
            

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DbConnection.con.Open();
            SqlCommand cmd = DbConnection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update Seafarers set Surname='" + txtSurname.Text + "', FirstName='" + txtFirstName.Text + "',MiddleName='" + txtMiddleName.Text + "',DOB='" + txtDOB.Text + "', Gender='" + txtGender.Text + "',Age='" + txtAge.Text + "',Height='" + txtHeight.Text + "',NIN='" + txtNIN.Text + "',NOK='" + txtNOK.Text + "',Email='" + txtEmail.Text + "',Mobile='" + txtPhone.Text + "',State='" + txtState.Text + "',Address='" + txtAddress.Text + "',NIMASA_RegNo='" + txtCompany.Text + "',Location='" + txtLocation.Text + "',Zone='" + txtZone.Text + "',Rank='" + txtRank.Text + "' Where SeafarerID= '" + txtRegNo.Text + "'";
            cmd.ExecuteNonQuery();
            DbConnection.con.Close();
            MessageBox.Show("Record Successfully Updated");
            Reset();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DbConnection.con.Open();
            SqlCommand cmd = DbConnection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from Seafarers  Where SeafarerID= '" + txtRegNo.Text + "'";
            cmd.ExecuteNonQuery();
            DbConnection.con.Close();
            MessageBox.Show("Record Successfully deleted");
            Reset();
        }
    }
}
