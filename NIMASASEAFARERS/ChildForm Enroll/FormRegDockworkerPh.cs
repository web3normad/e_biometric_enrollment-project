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
using System.Text.RegularExpressions;
using BarcodeLib;

namespace NIMASASEAFARERS
{
    public partial class FormRegDockworkerPh : UserControl
    {
        public FormRegDockworkerPh()
        {
            InitializeComponent();
        }

        #region Reser fields
        public void Reset()
        {

            textBoxName.Text = "";
            textFirstName.Text = "";
            textMiddleName.Text = "";
            datePickerDOB.Text = "";
            comboBoxGender.SelectedIndex = 0;
            textHeight.Text = "";
            textNIN.Text = "";
            textBoxmail.Text = "";
            textAge.Text = "";
            comboOrigin.SelectedIndex = 0;
            textZone.Text = "";
            textAddress.Text = "";
            textNOK.Text = "";
            comboRel.SelectedIndex = 0;
            textPhone.Text = "";
            textCompany.Text = "";
            textLocation.Text = "";
            textRank.Text = "";
            textGeno.SelectedIndex = 0;
            textBloodGrp.SelectedIndex = 0;
            comboQual.SelectedIndex = 0;
            dateEnrollment.Text = "";
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

        private void DockworkerId()
        {
            textBoxCnic.Text = "NMLS/DW" + GetUniqueKey(4);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "" || textFirstName.Text == "" || textBoxCnic.Text == "" || datePickerDOB.Text == "" || textAge.Text == "" || comboOrigin.Text == "" || textZone.Text == "" || textNOK.Text == "" || textAddress.Text == "" || comboBoxGender.Text == "" || textHeight.Text == "" || textNIN.Text == "" || textPhone.Text == "" || textCompany.Text == "" || textLocation.Text == "" || textRank.Text == "" || dateTimeIssue.Text == "" || dateTimeExpiry.Text == "" || dateEnrollment.Text == "")
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



                        SqlCommand comd = new SqlCommand("insert into [DockworkersPh] (DockworkerID, Surname, FirstName, MiddleName, DOB, Gender, Age, Height, NIN, NOK,Relationship, Email, Mobile, Address, State, Company, Location, Zone, Rank, Genotype, BloodGroup, Qualification, DateIssue, DateExpiry, BarcodeImg, EnrollmentDate) values(@dockid,@surname,@firstname,@middlename,@dob,@gender,@age,@height,@nin,@nok,@rel, @email,@mobile,@address,@state,@company,@location,@zone,@rank,@genotype,@bloodgrp,@qualification,@issue,@expiry,@barcode,@enrollmentdate)", DbConnection.con);

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
                        comd.Parameters.Add(new SqlParameter("@company", textCompany.Text));
                        comd.Parameters.Add(new SqlParameter("@location", textLocation.Text));
                        comd.Parameters.Add(new SqlParameter("@zone", textZone.Text));
                        comd.Parameters.Add(new SqlParameter("@rank", textRank.Text));
                        comd.Parameters.Add(new SqlParameter("@genotype", textGeno.Text));
                        comd.Parameters.Add(new SqlParameter("@bloodGrp", textBloodGrp.Text));
                        comd.Parameters.Add(new SqlParameter("@qualification", comboQual.Text));

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
                        DockworkerId();
                        Reset();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void textAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
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


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void guna2HtmlLabel35_Click(object sender, EventArgs e)
        {

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

        private void textHeight_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void textBoxName_Click(object sender, EventArgs e)
        {
            Barcode barcode = new Barcode();
            Color foreColor = Color.Black;
            Color backColor = Color.Transparent;
            Image img = barcode.Encode(TYPE.CODE128, textBoxCnic.Text, foreColor, backColor, (int)(picBarcode.Width * 3.0), (int)(picBarcode.Height * 3.0));
            picBarcode.Image = img;
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

        private void textNIN_KeyPress_1(object sender, KeyPressEventArgs e)
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
            DockworkerId();
        }

        private void comboQual_TextChanged(object sender, EventArgs e)
        {
            {
                if (comboQual.SelectedIndex < 0)
                {
                    comboQual.Text = "Select";
                }
                else
                {
                    comboQual.Text = comboQual.SelectedText;
                }
            }
        }

        private void FormRegDockworkerPh_Load(object sender, EventArgs e)
        {

            comboBoxGender.Text = "Select";
            textGeno.Text = "Select";
            textBloodGrp.Text = "Select";
            comboRel.Text = "Select";
            comboOrigin.Text = "Select";
            comboQual.Text = "Select";
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
