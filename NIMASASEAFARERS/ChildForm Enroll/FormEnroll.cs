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

namespace NIMASASEAFARERS
{
    public partial class FormEnroll : UserControl
    {
        //Declaration of all variables, vectors and haarcascades
        Image<Bgr, Byte> currentFrame;


        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);

        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();

        SqlConnection con;




        string imageLocation = "";

        #region Add Staff Members
        public static List<Byte[]> prints = new List<byte[]>();
        Bus_Person busper = new Bus_Person();
        Dal_Person dalper = new Dal_Person();
        #endregion
        public FormEnroll()
        {
            InitializeComponent();
        }
        #region Reser fields
        public void Reset()
        {
            textBoxCnic.Text = "";
            textBoxName.Text = "";
            textFirstName.Text = "";
            textMiddleName.Text = "";
            datePickerDOB.Text = "";
            comboBoxGender.SelectedIndex = 0;
            textAge.Text = "";
            textHeight.Text = "";
            textNIN.Text = "";
            textBoxmail.Text = "";
            textPhone.Text = "";
            textNOK.Text = "";
            comboOrigin.SelectedIndex = 0;
            textZone.Text = "";
            textAddress.Text = "";
            textLocation.Text = "";
            dateEnrollment.Text = "";
            textRank.Text = "";
            textRegNo.Text = "";
            comboRel.SelectedIndex = 0;
            textGeno.SelectedIndex = 0;
            textBloodGrp.SelectedIndex = 0;
            dateTimeIssue.Text = "";
            dateTimeExpiry.Text = "";
            picBarcode.Image = null;
            textPhone.FillColor = Color.White;
            textNIN.FillColor = Color.White;


        }
        #endregion

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];

            chars = "123456789".ToCharArray();

            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        private void SeafarerId()
        {
            textBoxCnic.Text = "NMLS/SF" + GetUniqueKey(4);
        }



        //public void AutoID()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    {
        //        try
        //        {
        //            DbConnection.con.Open();
        //            string sql = "SELECT TOP 1 id, SeafarerID FROM Seafarers ORDER BY id DESC";
        //            cmd = new SqlCommand(sql, DbConnection.con);
        //            adapter = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();

        //            adapter.Fill(ds);

        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                string tmp_id = ds.Tables[0].Rows[0]["SeafarerID"].ToString().Substring(7, 4);
        //                int new_id = System.Convert.ToInt32(tmp_id) + 1;
        //                textBoxCnic.Text = "NMLS/SF" + new_id.ToString("0000");
        //            }
        //            else
        //                textBoxCnic.Text = "NMLS/SF0001";
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


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (textBoxName.Text == "" || textFirstName.Text == "" || textBoxCnic.Text == "" || datePickerDOB.Text == "" || textAge.Text == "" || comboOrigin.Text == "" || textZone.Text == "" || textNOK.Text == "" || textAddress.Text == "" || comboBoxGender.Text == "" || textHeight.Text == "" || textNIN.Text == "" || textNOK.Text == "" || textPhone.Text == "" || textRegNo.Text == "" || textLocation.Text == "" || comboOrigin.Text == "" || textRank.Text == "" || textZone.Text == "" || dateTimeIssue.Text == "" || dateTimeExpiry.Text == "" || dateEnrollment.Text == "")
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



                        SqlCommand comd = new SqlCommand("insert into [Seafarers] (SeafarerID, Surname, FirstName, MiddleName, DOB, Gender, Age, Height, NIN,NOK,Relationship, Email, Mobile, Address, State,NIMASA_RegNo, Location, Zone, Rank,Genotype,BloodGroup, DateIssue, DateExpiry, Barcode, EnrollmentDate) values(@dockid,@surname,@firstname,@middlename,@dob,@gender,@age,@height,@nin,@nok,@rel,@email,@mobile,@address,@state,@nimreg,@location,@zone,@rank,@genotype,@bloodgrp,@issue,@expiry,@barcode,@enrollmentdate)", DbConnection.con);

                        MemoryStream stream = new MemoryStream();
                        picBarcode.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] barcode = stream.ToArray();



                        comd.Parameters.Add(new SqlParameter("@dockid", textBoxCnic.Text));
                        comd.Parameters.Add(new SqlParameter("@surname", textBoxName.Text));
                        comd.Parameters.Add(new SqlParameter("@firstname", textFirstName.Text));
                        comd.Parameters.Add(new SqlParameter("@middlename", textMiddleName.Text));

                        comd.Parameters.Add(new SqlParameter("@dob", datePickerDOB.Text));

                        comd.Parameters.Add(new SqlParameter("@gender", comboBoxGender.Text));
                        comd.Parameters.Add(new SqlParameter("@age", textAge.Text));
                        comd.Parameters.Add(new SqlParameter("@height", textHeight.Text));
                        comd.Parameters.Add(new SqlParameter("@nin", textNIN.Text));
                        comd.Parameters.Add(new SqlParameter("@nok", textNOK.Text));
                        comd.Parameters.Add(new SqlParameter("@rel", comboRel.Text));
                        comd.Parameters.Add(new SqlParameter("@email", textBoxmail.Text));
                        comd.Parameters.Add(new SqlParameter("@mobile", textPhone.Text));

                        comd.Parameters.Add(new SqlParameter("@address", textAddress.Text));
                        comd.Parameters.Add(new SqlParameter("@state", comboOrigin.Text));
                        comd.Parameters.Add(new SqlParameter("@nimreg", textRegNo.Text));
                        comd.Parameters.Add(new SqlParameter("@location", textLocation.Text));
                        comd.Parameters.Add(new SqlParameter("@zone", textZone.Text));
                        comd.Parameters.Add(new SqlParameter("@rank", textRank.Text));
                        comd.Parameters.Add(new SqlParameter("@rank", textGeno.Text));
                        comd.Parameters.Add(new SqlParameter("@rank", textBloodGrp.Text));


                        comd.Parameters.Add(new SqlParameter("@issue", dateTimeIssue.Text = DateTime.Now.ToShortDateString()));
                        comd.Parameters.Add(new SqlParameter("@expiry", dateTimeExpiry.Text = DateTime.Now.AddDays(1825).ToShortDateString()));
                        comd.Parameters.Add(new SqlParameter("@enrollmentdate", dateEnrollment.Text = DateTime.Now.ToShortDateString()));
                        comd.Parameters.AddWithValue("@barcode", barcode);

                        if (DbConnection.con.State == System.Data.ConnectionState.Closed)
                        {
                            DbConnection.con.Open();
                        }
                        comd.ExecuteNonQuery();

                        Verify.Save();
                        DbConnection.con.Close();
                        SeafarerId();
                        Reset();





                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }






        private void FormEnroll_Load(object sender, EventArgs e)
        {
            SeafarerId();
            comboBoxGender.Text = "Select";
            textGeno.Text = "Select";
            textBloodGrp.Text = "Select";
            comboRel.Text = "Select";
            comboOrigin.Text = "Select";

        }

        private Form active = null;
        private SqlDataAdapter adapter;
        private SqlCommand cmd;

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


        private void textAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void datePickerDOB_ValueChanged(object sender, EventArgs e)
        {
            DateTime time_start = Convert.ToDateTime(datePickerDOB.Value);
            DateTime time_end = DateTime.Today;
            TimeSpan span = time_end.Subtract(time_start);
            var daysTotal = span.TotalDays;
            var yearsTotal = Math.Truncate(daysTotal / 365);

            textAge.Text = Convert.ToString(yearsTotal);
        }

        private void textPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex r = new Regex(@"^[0-9]{10}$");
            if (r.IsMatch(textPhone.Text))
            {
                textPhone.FillColor = System.Drawing.Color.LightGreen;

            }
            else
            {
                textPhone.FillColor = System.Drawing.Color.LightPink;
            }
        }

        private void textBoxName_Click_1(object sender, EventArgs e)
        {
            Barcode barcode = new Barcode();
            Color foreColor = Color.Black;
            Color backColor = Color.Transparent;
            Image img = barcode.Encode(TYPE.CODE128, textBoxCnic.Text, foreColor, backColor, (int)(picBarcode.Width * 3.0), (int)(picBarcode.Height * 3.0));
            picBarcode.Image = img;
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

        private void textNIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex r = new Regex(@"^[0-9]{10}$");
            if (r.IsMatch(textNIN.Text))
            {
                textNIN.FillColor = System.Drawing.Color.LightGreen;

            }
            else
            {
                textNIN.FillColor = System.Drawing.Color.LightPink;
            }
        }

        private void textBoxCnic_Load(object sender, EventArgs e)
        {
            SeafarerId();
        }

        private void comboBoxGender_TextChanged(object sender, EventArgs e)
        {
            {
                if (comboBoxGender.SelectedIndex < 0)
                {
                    comboBoxGender.Text = "Select";
                }
                else
                {
                    comboBoxGender.Text = comboBoxGender.SelectedText;
                }
            }
        }

        private void textGeno_TextChanged(object sender, EventArgs e)
        {
            {
                if (textGeno.SelectedIndex < 0)
                {
                    textGeno.Text = "Select";
                }
                else
                {
                    textGeno.Text = textGeno.SelectedText;
                }
            }
        }

        private void textBloodGrp_TextChanged(object sender, EventArgs e)
        {
            {
                if (textBloodGrp.SelectedIndex < 0)
                {
                    textBloodGrp.Text = "Select";
                }
                else
                {
                    textBloodGrp.Text = textBloodGrp.SelectedText;
                }
            }

        }

        private void comboRel_TextChanged(object sender, EventArgs e)
        {
            {
                if (comboRel.SelectedIndex < 0)
                {
                    comboRel.Text = "Select";
                }
                else
                {
                    comboRel.Text = comboRel.SelectedText;
                }
            }
        }

        private void comboOrigin_TextChanged(object sender, EventArgs e)
        {
            {
                if (comboOrigin.SelectedIndex < 0)
                {
                    comboOrigin.Text = "Select";
                }
                else
                {
                    comboOrigin.Text = comboOrigin.SelectedText;
                }
            }
        }
    }
}