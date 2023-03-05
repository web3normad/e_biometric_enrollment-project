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

namespace NIMASASEAFARERS
{
    public partial class FormEnroll : UserControl
    {
        //Declaration of all variables, vectors and haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;

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

            textBoxName.Text = "";
            textFirstName.Text = "";
            textMiddleName.Text = "";
            dateTimeDOB.Text = "";
            comboBoxGender.SelectedIndex = -1;
            textHeight.Text = "";
            textNIN.Text = "";
            textBoxmail.Text = "";
            textPhone.Text = "";
            textLocation.Text = "";
            comboTitle.SelectedIndex = -1;
            comboRank.SelectedIndex = -1;
            dateTimeIssue.Text = "";
            dateTimeExpiry.Text = "";
            pictureBox1.Image = null;

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

        private void Clock_ID()
        {
            textBoxCnic.Text = "CS" + GetUniqueKey(4);
        }

        private void StaffId()
        {
            textBoxCnic.Text = GetUniqueKey(5);
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            CaptureForm cf = new CaptureForm();
            MainControlClass.showControl(cf, Content);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
         
            //Gender();
            //Account();
            if (textBoxName.Text == "" || textFirstName.Text == "" || textMiddleName.Text == "" || textBoxCnic.Text == "" || dateTimeDOB.Text == "" || comboBoxGender.Text == "" || textHeight.Text == "" || textNIN.Text == "" || textBoxmail.Text == "" || textPhone.Text == "" || textLocation.Text == "" || comboTitle.Text == "" || comboRank.Text == ""|| dateTimeIssue.Text == "" || dateTimeExpiry.Text == "")
            {
                Verify.Input();
            }

            else
            {
                SqlCommand cmdChk = new SqlCommand("select Email from EmpRegistration where Email='" + textBoxmail.Text + "';", DbConnection.con);
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
                        //byte[] image = null;
                        //FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
                        //BinaryReader br = new BinaryReader(fs);
                        //image = br.ReadBytes((int)fs.Length);
                        //////////////////////////////////////////////////////////////////////////////
                        SqlCommand cmd = new SqlCommand();
                        //cmd.Connection = DbConnection.con;
                        

                        SqlCommand comd = new SqlCommand("insert into [EmpRegistration] (CNIC, Surname, FirstName, MiddleName, DOB, Gender, Height, NIN, Rank, Email, Mobile, Location, Title, DateIssue, DateExpiry, Barcode) values(@cnic,@surname,@firstname,@middlename,@dob,@gender,@height,@nin,@rank,@email,@mobile,@location,@title,@issue,@expiry,@barcode)", DbConnection.con);

                        MemoryStream stream = new MemoryStream();

                        pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] barcode = stream.ToArray();

                        comd.Parameters.Add(new SqlParameter("@cnic", textBoxCnic.Text));
                        comd.Parameters.Add(new SqlParameter("@surname", textBoxName.Text));
                        comd.Parameters.Add(new SqlParameter("@firstname", textFirstName.Text));
                        comd.Parameters.Add(new SqlParameter("@middlename", textMiddleName.Text));

                        comd.Parameters.Add(new SqlParameter("@dob", dateTimeDOB.Text));

                        comd.Parameters.Add(new SqlParameter("@gender", comboBoxGender.Text));
                        comd.Parameters.Add(new SqlParameter("@height", textHeight.Text));
                        comd.Parameters.Add(new SqlParameter("@nin", textNIN.Text));
                        comd.Parameters.Add(new SqlParameter("@rank", comboRank.Text));

                        comd.Parameters.Add(new SqlParameter("@email", textBoxmail.Text));
                        comd.Parameters.Add(new SqlParameter("@mobile", textPhone.Text));
                        comd.Parameters.Add(new SqlParameter("@location", textLocation.Text));
                        comd.Parameters.Add(new SqlParameter("@title", comboTitle.Text));
                        comd.Parameters.Add(new SqlParameter("@issue", dateTimeIssue.Text));
                        comd.Parameters.Add(new SqlParameter("@expiry", dateTimeExpiry.Text));
                        comd.Parameters.AddWithValue("@barcode", barcode);


                        if (DbConnection.con.State == System.Data.ConnectionState.Closed)
                        {
                            DbConnection.con.Open();
                        }
                        comd.ExecuteNonQuery();

                        Verify.Save();
                        DbConnection.con.Close();
                        Reset();
                        StaffId();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {

            var writer = new BarcodeWriter();

            writer.Format = BarcodeFormat.CODE_128;
            var text = textBoxName.Text + "\n" +
             textFirstName.Text + "\n" +
             textMiddleName.Text + "\n" +
             textNIN.Text + "\n" +
             textLocation.Text + "\n" +
             textBoxCnic.Text + "\n" +
             comboTitle.Text;
            var result = writer.Write(text);
            pictureBox1.Image = result;
        }
    }
}
