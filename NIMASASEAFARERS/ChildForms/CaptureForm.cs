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
using DPUruNet;
using UareUSampleCSharp;


namespace NIMASASEAFARERS
{
    public partial class CaptureForm : UserControl
    {
        Image<Bgr, Byte> currentFrame;
        Emgu.CV.Capture grabber;
        //HaarCascade face;
        //string finalname;
        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();

        public static Dictionary<int, Fmd> fmds = new Dictionary<int, Fmd>();
        DataResult<Fmd> resultEnrollment;
        string imageLocation = "";
       
        List<Fmd> preenrollmentFmds;


        public CaptureForm()
        {
            InitializeComponent();
        }



        #region Reser fields
        public void Reset()
        {

            Passport.Image = null;
            // Picture.Image = null;
        }
        #endregion



        private void btnStart_Click(object sender, EventArgs e)
        {

        }
        void FrameGrabber(object sender, EventArgs e)
        {

            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(200, 185, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            Passport.Image = currentFrame.ToBitmap();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {

        }
        private void saveprints(Byte[] array)
        {
            DbConnection.checkConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("Insert Into " + "Finger" + " (CNIC, LeftFinger) VALUES('" + textBoxCnic.Text.ToString() + "' '" + Fmd.SerializeXml(resultEnrollment.Data) + "')", DbConnection.con);

                cmd.Parameters.Add(new SqlParameter("@cnic", textBoxCnic.Text));
                cmd.Parameters.Add(new SqlParameter("@img", array));
                if (DbConnection.con.State == System.Data.ConnectionState.Closed)
                {
                    DbConnection.con.Open();
                }
                cmd.ExecuteNonQuery();
                lblmsg.Text = "";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Finger Saved!";
                //MessageBox.Show("Finger Saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveEnrollment(Byte[] array)
        {
            DbConnection.checkConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("Insert Into " + "Finger" + " (RightFinger, LeftFinger) VALUES('" + textBoxCnic.Text.ToString() + "' '" + Fmd.SerializeXml(resultEnrollment.Data) + "' '" + Fmd.SerializeXml(resultEnrollment.Data) + "')", DbConnection.con);

                cmd.Parameters.Add(new SqlParameter("@cnic", textBoxCnic.Text));
                cmd.Parameters.Add(new SqlParameter("@img", array));
                if (DbConnection.con.State == System.Data.ConnectionState.Closed)
                {
                    DbConnection.con.Open();
                }
                cmd.ExecuteNonQuery();
                lblmsg.Text = "";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Finger Saved!";
                //MessageBox.Show("Finger Saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCapture_Click_1(object sender, EventArgs e)
        {

        }

        private void btnStart_Click_2(object sender, EventArgs e)
        {
          




        }

        private void btnFinger_Click(object sender, EventArgs e)
        {
            FingerprintControl Country = new FingerprintControl();
            Country.ShowDialog();
        }

        private void btnCapture_Click_2(object sender, EventArgs e)
        {
            //You'll want to unsubcribe from the event handler so this doesn't occur
            Application.Idle -= FrameGrabber;
            grabber.Dispose();
        }

        private void btnStart_Click_3(object sender, EventArgs e)
        {
              grabber = new Emgu.CV.Capture();
            grabber.QueryFrame();

            //Initialize the FrameGraber event
            Application.Idle += new EventHandler(FrameGrabber);
            //button1.Enabled = false;

        }
         public SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Pictures\NIMASASEAFARERS\NIMASASEAFARERS\BIOMETRICNIMASA.mdf;Integrated Security=True;Connect Timeout=30");
    
        private void BtnSave_Click_1(object sender, EventArgs e)
        {
            if (resultEnrollment != null)
            {
                try
                {
                    DbConnection.con.Close();
                    DbConnection.con.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into Finger (RightFinger, LeftFinger, Passport) VALUES(@cnic @Fmd.SerializeXml(resultEnrollment.Data))", DbConnection.con);
                    MemoryStream stream = new MemoryStream();
                    Passport.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] picc = stream.ToArray();

                    cmd.Parameters.AddWithValue("@pic", picc);

                    cmd.ExecuteNonQuery();

                    Verify.Save();
                  
                    DbConnection.con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Passport_Click(object sender, EventArgs e)
        {

        }
    }
}
