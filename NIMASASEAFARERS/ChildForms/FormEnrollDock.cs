using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using DAL;
using Business_Entities;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;
using System.Text.RegularExpressions;
using BarcodeLib;
using System.Drawing.Imaging;

namespace NIMASASEAFARERS
{
    public partial class FormEnrollDock : UserControl
    {
        Image<Bgr, Byte> currentFrame;
        Emgu.CV.Capture grabber;
        //HaarCascade face;
        //string finalname;
        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();


        string imageLocation = "";


        SqlConnection con;

       


   

        #region Add Staff Members
        public static List<Byte[]> prints = new List<byte[]>();
        Bus_Person busper = new Bus_Person();
        Dal_Person dalper = new Dal_Person();
        #endregion
        public FormEnrollDock()
        {
            InitializeComponent();

            
        }
        #region Reser fields
        public void Reset()
        {

            textBoxName.Text = "";
            textFirstName.Text = "";
            textMiddleName.Text = "";
            dateTimeDOB.Text = "";
            comboBoxGender.SelectedIndex = -1;
            textHeight.Text = "";
            textNIN.Text = "";
            textBoxmail.Text = "";
            textAge.Text = "";
            comboOrigin.SelectedIndex = -1;
            textZone.Text = "";
            textAddress.Text = "";
            textNOK.Text = "";
            textPhone.Text = "";
            textCompany.Text = "";
            textLocation.Text = "";
            textRank.Text = "";
            comboQual.SelectedIndex = -1;
            dateEnrollment.Text = "";
            dateTimeIssue.Text = "";
            dateTimeExpiry.Text = "";
            picBarcode.Image = null;
            

        }
        #endregion
        //public static string GetUniqueKey(int maxSize)
        //{
        //    char[] chars = new char[62];
        //    chars = "123456789".ToCharArray();
        //    byte[] data = new byte[1];
        //    RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        //    crypto.GetNonZeroBytes(data);
        //    data = new byte[maxSize];
        //    crypto.GetNonZeroBytes(data);
        //    StringBuilder result = new StringBuilder(maxSize);
        //    foreach (byte b in data)
        //    {
        //        result.Append(chars[b % (chars.Length)]);
        //    }
        //    return result.ToString();
        //}

      

        //private void StaffId()
        //{
        //    textBoxCnic.Text = GetUniqueKey(5);
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {

            //Gender();
            //Account();
            if (textBoxName.Text == "" || textFirstName.Text == ""  || textBoxCnic.Text == "" || dateTimeDOB.Text == "" || textAge.Text == "" || comboOrigin.Text == "" || textZone.Text == "" || textNOK.Text == "" || textAddress.Text == "" || comboBoxGender.Text == "" || textHeight.Text == "" || textNIN.Text == "" || textBoxmail.Text == "" || textPhone.Text == "" || textCompany.Text == "" || textLocation.Text == ""  || textRank.Text == "" || dateTimeIssue.Text == "" || dateTimeExpiry.Text == "" || dateEnrollment.Text == "")
            {
                Verify.Input();
            }

            else
            {
                SqlCommand cmdChk = new SqlCommand("select NIN from Dockworkers where NIN='" + textNIN.Text + "';", DbConnection.con);
                SqlParameter param = new SqlParameter();
                if (DbConnection.con.State != ConnectionState.Open)
                {
                    DbConnection.con.Open();
                }
                param.Value = this.textBoxCnic.Text;
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
                       


                        SqlCommand comd = new SqlCommand("insert into [Dockworkers] (DockworkerID, Surname, FirstName, MiddleName, DOB, Gender, Age, Height, NIN, NOK, Email, Mobile, Address, State, Company, Location, Zone, Rank, Qualification, DateIssue, DateExpiry, Barcode, EnrollmentDate) values(@dockid,@surname,@firstname,@middlename,@dob,@gender,@age,@height,@nin,@nok,@email,@mobile,@address,@state,@company,@location,@zone,@rank,@qualification,@issue,@expiry,@barcode,@enrollmentdate)", DbConnection.con);

                        MemoryStream stream = new MemoryStream();
                        picBarcode.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] barcode = stream.ToArray();

                       

                        comd.Parameters.Add(new SqlParameter("@dockid", textBoxCnic.Text));
                        comd.Parameters.Add(new SqlParameter("@surname", textBoxName.Text));
                        comd.Parameters.Add(new SqlParameter("@firstname", textFirstName.Text));
                        comd.Parameters.Add(new SqlParameter("@middlename", textMiddleName.Text));

                        comd.Parameters.Add(new SqlParameter("@dob", dateTimeDOB.Text));

                        comd.Parameters.Add(new SqlParameter("@gender", comboBoxGender.Text));
                        comd.Parameters.Add(new SqlParameter("@age", textAge.Text));
                        comd.Parameters.Add(new SqlParameter("@height", textHeight.Text));
                        comd.Parameters.Add(new SqlParameter("@nin", textNIN.Text));
                        comd.Parameters.Add(new SqlParameter("@nok", textNOK.Text));
                        comd.Parameters.Add(new SqlParameter("@email", textBoxmail.Text));
                        comd.Parameters.Add(new SqlParameter("@mobile", textPhone.Text));
                        
                        comd.Parameters.Add(new SqlParameter("@address", textAddress.Text));
                        comd.Parameters.Add(new SqlParameter("@state", comboOrigin.Text));
                        comd.Parameters.Add(new SqlParameter("@company", textCompany.Text));
                        comd.Parameters.Add(new SqlParameter("@location", textLocation.Text));
                        comd.Parameters.Add(new SqlParameter("@zone", textZone.Text));
                        comd.Parameters.Add(new SqlParameter("@rank", textRank.Text));
                        comd.Parameters.Add(new SqlParameter("@qualification", comboQual.Text));

                        comd.Parameters.Add(new SqlParameter("@issue", dateTimeIssue.Text));
                        comd.Parameters.Add(new SqlParameter("@expiry", dateTimeExpiry.Text));
                        comd.Parameters.Add(new SqlParameter("@enrollmentdate", dateEnrollment.Text = DateTime.Now.ToShortDateString()));
                        comd.Parameters.AddWithValue("@barcode", barcode);







                       


                        if (DbConnection.con.State == System.Data.ConnectionState.Closed)
                        {
                            DbConnection.con.Open();
                        }
                        comd.ExecuteNonQuery();

                        Verify.Save();
                        DbConnection.con.Close();
                        Reset();
                        Increment();
                        
                      


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void Barcode()
        {

        }

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        // // CaptureForm cf = new CaptureForm();
        // //  MainControlClass.showControl(cf, Content);

        // // FingerprintControl Country = new FingerprintControl();
        ////   Country.ShowDialog();
        //    Opening(new FingerprintControl());
        //}


        //public void AutoID()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    {
        //        try
        //        {
        //            DbConnection.con.Open();
        //            string sql = "SELECT TOP 1 id, DockworkerID FROM Dockworkers ORDER BY id DESC";
        //            cmd = new SqlCommand(sql, DbConnection.con);
        //            adapter = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();

        //            adapter.Fill(ds);

        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                string tmp_id = ds.Tables[0].Rows[0]["DockworkerID"].ToString()/*.Substring(14,3)*/;
        //                int new_id = System.Convert.ToInt32(tmp_id) + 1;
        //                textBoxCnic.Text = "NMLS/ABTLAP/DW" + new_id.ToString("000");
        //                textBoxCnic.Text = "NMLS/ENL/DW" + new_id.ToString("000");
        //                textBoxCnic.Text = "NMLS/GDNL/DW" + new_id.ToString("000");
        //            }
        //            else
        //                textBoxCnic.Text = "NMLS/ABTLAP/DW001";
        //        }


        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //        finally
        //        {
        //            DbConnection.con.Close();
        //        }
        //    }
        //}
        private void Increment()
        {
            string[] tempArray = textAutoId.Text.Split('/');
            textBox1.Text = tempArray[0];
            textBox2.Text = tempArray[1];
            textBox3.Text = tempArray[2];

            if (textBox4.Text == "")
            {
                textBox4.Text = tempArray[3];
            }
            int i = Convert.ToInt32(textBox4.Text);
            i++;
            textBox4.Text = i.ToString();

            string autoId = textBox1.Text + "/" + textBox2.Text + "/" + textBox3.Text + "/" + string.Format("{0:000}", i);
            textBoxCnic.Text = autoId;


        }
        private void FormEnrollDock_Load(object sender, EventArgs e)
        {
            //  StaffId();
            Increment();
        }

        private Form active = null;
        private SqlDataAdapter adapter;

        private void Opening(Form Child)
        {
            if (active != null)
            {
                //
                active.Close();
            }
            active = Child;
            Child.TopLevel = false;
            Child.FormBorderStyle = FormBorderStyle.None;
            Child.Dock = DockStyle.Fill;
            Content.Controls.Add(Child);
            Content.Tag = Child;
            Child.BringToFront();
            Child.Show();
          
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {

           

            Barcode barcode = new Barcode();
            Color foreColor = Color.Black;
            Color backColor = Color.Transparent;
            Image img = barcode.Encode(TYPE.CODE128, textBoxCnic.Text, foreColor, backColor, (int)(picBarcode.Width * 3.0), (int)(picBarcode.Height * 3.0));
            picBarcode.Image = img;
        }

      

        private void textAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void textHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (ch == 46 && textHeight.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;

            }
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void textPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex r = new Regex(@"^[0-9]{11}$");
            if (r.IsMatch(textPhone.Text))
            {
                textPhone.FillColor = System.Drawing.Color.LightGreen;

            }
            else
            {
                textPhone.FillColor = System.Drawing.Color.LightPink;
            }
        }

        private void textNIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex r = new Regex(@"^[0-9]{11}$");
            if (r.IsMatch(textNIN.Text))
            {
                textNIN.FillColor = System.Drawing.Color.LightGreen;

            }
            else
            {
                textNIN.FillColor = System.Drawing.Color.LightPink;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void Content_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
