using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UareUSampleCSharp;
using System.IO;
using System.Text.RegularExpressions;
using BarcodeLib;

namespace NIMASASEAFARERS
{
    public partial class EditDockworker : UserControl
    {
        public EditDockworker()
        {
            InitializeComponent();
        }

        private Fmd firstFinger;
        private Fmd secondFinger;
        int count = 0;
        DataResult<Fmd> resultEnrollment;
        List<Fmd> preenrollmentFmds;
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
            picBarcode.Image = null;




        }
        #endregion
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }



        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {


        }

        private void btnUpload_Click(object sender, EventArgs e)
        {



        }

        private void loadBarcode_Click(object sender, EventArgs e)
        {
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //SqlCommand con = new SqlCommand();
            //DbConnection.con.Open();
            //SqlCommand cmd = new SqlCommand("select Barcode from Dockworkers where DockworkerID='" + txtRegNo.Text + "'", DbConnection.con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["Barcode"]);
            //    pictureBox1.Image = new Bitmap(ms);
            //}

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {

            if (txtRegNo.Text == "")
            {
                Verify.Input();
            }

            else
                DbConnection.con.Open();
            SqlCommand cmd = new SqlCommand("select Surname, FirstName, MiddleName, DOB,Gender, Age, Height, NIN, NOK, Email, Mobile, State, Address, Company, Location, Zone, rank, DockworkersPassport.Passport from Dockworkers inner join DockworkersPassport on Dockworkers.DockworkerID=DockworkerSPassport.CNIC where DockworkerID='" + txtRegNo.Text + "'", DbConnection.con);
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

                //MemoryStream ms1 = new MemoryStream((byte[])srd["Barcode"]);
                //picBarcode.Image = new Bitmap(ms1);

                //Fmd.DeserializeXml(srd["FingertempR"].ToString());
                //Fmd.DeserializeXml(srd["FingertempL"].ToString());

            }
            DbConnection.con.Close();



        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            DbConnection.con.Open();
            SqlCommand cmd = DbConnection.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update Dockworkers set Surname='" + txtSurname.Text + "', FirstName='" + txtFirstName.Text + "',MiddleName='" + txtMiddleName.Text + "',DOB='" + txtDOB.Text + "', Gender='" + txtGender.Text + "',Age='" + txtAge.Text + "',Height='" + txtHeight.Text + "',NIN='" + txtNIN.Text + "',NOK='" + txtNOK.Text + "',Email='" + txtEmail.Text + "',Mobile='" + txtPhone.Text + "',State='" + txtState.Text + "',Address='" + txtAddress.Text + "',Company='" + txtCompany.Text + "',Location='" + txtLocation.Text + "',Zone='" + txtZone.Text + "',Rank='" + txtRank.Text + "' Where DockworkerID= '" + txtRegNo.Text + "'";
            cmd.ExecuteNonQuery();
            DbConnection.con.Close();
            MessageBox.Show("Record Successfully Updated");
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
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

        private void btnBrowse_Click_1(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "Select Image (*.jpg; *.png;,*.Gif) |*.Jpg; *.png; *.Gif ";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Passport.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void btnUpload_Click_1(object sender, EventArgs e)
        {
            DbConnection.con.Open();
            SqlCommand con = new SqlCommand();
            SqlCommand cmd = new SqlCommand("insert into DockworkerPhoto (PassportID,Surname,FirstName,MiddleName,Passport)values(@cnic,@surname,@firstname,@middlename,@pic)", DbConnection.con);




            cmd.Parameters.Add(new SqlParameter("@cnic", txtRegNo.Text));
            cmd.Parameters.Add(new SqlParameter("@surname", txtSurname.Text));
            cmd.Parameters.Add(new SqlParameter("@firstName", txtFirstName.Text));
            cmd.Parameters.Add(new SqlParameter("@middlename", txtMiddleName.Text));
            cmd.Parameters.AddWithValue("@pic", openFileDialog1.FileName);


            cmd.ExecuteNonQuery();
            DbConnection.con.Close();
            MessageBox.Show("Passport Uploaded");
        }

        //private void txtRegNo_Click(object sender, EventArgs e)
        //{

        //}

        private void btnBack_Click(object sender, EventArgs e)
        {
            DockworkersRecords dwr = new DockworkersRecords();
            MainControlClass.showControl(content, dwr);
        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            Barcode barcode = new Barcode();
            Color foreColor = Color.Black;
            Color backColor = Color.Transparent;
            Image img = barcode.Encode(TYPE.CODE128, txtRegNo.Text, foreColor, backColor, (int)(this.picBarcode.Width * 3.0), (int)(this.picBarcode.Height * 3.0));
            this.picBarcode.Image = img;
        }

        private void btnSearch_Click_2(object sender, EventArgs e)
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Barcode barcode = new Barcode();
            Color foreColor = Color.Black;
            Color backColor = Color.Transparent;
            Image img = barcode.Encode(TYPE.CODE128, txtRegNo.Text, foreColor, backColor, (int)(picBarcode.Width * 3.0), (int)(picBarcode.Height * 3.0));
            picBarcode.Image = img;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            DbConnection.con.Open();
            
                MemoryStream stream = new MemoryStream();
                picBarcode.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] BarcodeImg = stream.ToArray();


            SqlCommand con = new SqlCommand();
            SqlCommand cmd = new SqlCommand("Update Dockworkers set BarcodeImg=@img Where DockworkerId='" + txtRegNo.Text + "'", DbConnection.con);


                
              
                cmd.Parameters.Add(new SqlParameter("@img", BarcodeImg));
                cmd.ExecuteNonQuery();
                DbConnection.con.Close();
                MessageBox.Show("Record Successfully Updated");
               
        }

        private void content_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void textBloodGrp_TextChanged(object sender, EventArgs e)
        {

        }

        private void textGeno_TextChanged(object sender, EventArgs e)
        {

        }

        private void picBarcode_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel18_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel17_Click(object sender, EventArgs e)
        {

        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel16_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel15_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel14_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel13_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel12_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Passport_Click(object sender, EventArgs e)
        {

        }

        private void txtGender_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDOB_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNIN_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtState_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRank_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNOK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCompany_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtZone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMiddleName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRegNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
