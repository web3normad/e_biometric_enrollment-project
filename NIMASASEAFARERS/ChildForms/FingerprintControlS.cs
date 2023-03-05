using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;

namespace NIMASASEAFARERS
{

    public partial class FingerprintControlS : Form
    {
        private DPCtlUruNet.EnrollmentControl _enrollmentControl;
        private ReaderCollection _readers;

        private const int PROBABILITY_ONE = 0x7fffffff;
        private Fmd firstFinger;
        private Fmd secondFinger;
        int count = 0;
        DataResult<Fmd> resultCapture;
        List<Fmd> preenrollmentFmds;
        /// <summary>
        /// Handler for when a fingerprint is captured.
        /// </summary>
        /// <param name="captureResult">contains info and data on the fingerprint capture</param>
        /// 


        Image<Bgr, Byte> currentFrame;
        Capture grabber;

        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        public FingerprintControlS()
        {
            InitializeComponent();
        }

        #region Reser fields
        public void Reset()
        {
            PassportImage.Image = null;
            pbFingerprint.Image = null;
            textboxNIN.Text = "";
            textboxID.Text = "";

        }
        #endregion
        public Dictionary<int, Fmd> Fmds
        {
            get { return fmds; }
            set { fmds = value; }
        }
        private Dictionary<int, Fmd> fmds = new Dictionary<int, Fmd>();
        private enum Action
        {
            UpdateReaderState,
            SendBitmap,
            SendMessage
        }

        private Reader _reader;

        public Reader CurrentReader
        {
            get { return _reader; }
            set { _reader = value; }
        }

        private void LoadScanners()
        {
            cboReaders.Text = string.Empty;
            cboReaders.Items.Clear();
            cboReaders.SelectedIndex = -1;

            try
            {
                _readers = ReaderCollection.GetReaders();

                foreach (Reader Reader in _readers)
                {
                    cboReaders.Items.Add(Reader.Description.Name);
                }

                if (cboReaders.Items.Count > 0)
                {
                    cboReaders.SelectedIndex = 0;
                    //btnCaps.Enabled = true;
                    //btnSelect.Enabled = true;
                }
                else
                {
                    //btnSelect.Enabled = false;
                    //btnCaps.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //message box:
                String text = ex.Message;
                text += "\r\n\r\nPlease check if DigitalPersona service has been started";
                String caption = "Cannot access readers";
                MessageBox.Show(text, caption);
            }
        }

        private void FingerprintControlS_Load(object sender, EventArgs e)
        {
            LoadScanners();
            firstFinger = null;
            secondFinger = null;
            resultCapture = null;
            preenrollmentFmds = new List<Fmd>();
            if (CurrentReader != null)
            {
                CurrentReader.Dispose();
                CurrentReader = null;
            }
            CurrentReader = _readers[cboReaders.SelectedIndex];


            if (_enrollmentControl != null)
            {
                _enrollmentControl.Reader = CurrentReader;
            }
            else
            {
                _enrollmentControl = new DPCtlUruNet.EnrollmentControl(CurrentReader, Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
                _enrollmentControl.BackColor = System.Drawing.SystemColors.ControlLightLight;
                _enrollmentControl.Location = new System.Drawing.Point(20, 20);
                _enrollmentControl.Name = "ctlEnrollmentControl";
                _enrollmentControl.Size = new System.Drawing.Size(482, 346);
                _enrollmentControl.TabIndex = 0;
                _enrollmentControl.OnCancel += new DPCtlUruNet.EnrollmentControl.CancelEnrollment(this.enrollment_OnCancel);
                _enrollmentControl.OnCaptured += new DPCtlUruNet.EnrollmentControl.FingerprintCaptured(this.enrollment_OnCaptured);
                _enrollmentControl.OnDelete += new DPCtlUruNet.EnrollmentControl.DeleteEnrollment(this.enrollment_OnDelete);
                _enrollmentControl.OnEnroll += new DPCtlUruNet.EnrollmentControl.FinishEnrollment(this.enrollment_OnEnroll);
                _enrollmentControl.OnStartEnroll += new DPCtlUruNet.EnrollmentControl.StartEnrollment(this.enrollment_OnStartEnroll);
            }


            this.Controls.Add(_enrollmentControl);

        }
        #region Enrollment Control Events
        private void enrollment_OnCancel(DPCtlUruNet.EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            if (enrollmentControl.Reader != null)
            {
                SendMessage("OnCancel:  " + enrollmentControl.Reader.Description.Name + ", finger " + fingerPosition);
            }
            else
            {
                SendMessage("OnCancel:  No Reader Connected, finger " + fingerPosition);
            }

            btnCancel.Enabled = false;
        }

        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        private void enrollment_OnCaptured(DPCtlUruNet.EnrollmentControl enrollmentControl, CaptureResult captureResult, int fingerPosition)
        {
            if (enrollmentControl.Reader != null)
            {
                SendMessage("OnCaptured:  " + enrollmentControl.Reader.Description.Name + ", finger " + fingerPosition + ", quality " + captureResult.Quality.ToString());
            }
            else
            {
                SendMessage("OnCaptured:  No Reader Connected, finger " + fingerPosition);
            }

            if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                if (CurrentReader != null)
                {
                    CurrentReader.Dispose();
                    CurrentReader = null;
                }

                // Disconnect reader from enrollment control
                _enrollmentControl.Reader = null;

                MessageBox.Show("Error:  " + captureResult.ResultCode);
                btnCancel.Enabled = false;
            }
            else
            {
                if (captureResult.Data != null)
                {
                    foreach (Fid.Fiv fiv in captureResult.Data.Views)
                    {
                        pbFingerprint.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    }
                }
            }
        }
        private void enrollment_OnDelete(DPCtlUruNet.EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            if (enrollmentControl.Reader != null)
            {
                SendMessage("OnDelete:  " + enrollmentControl.Reader.Description.Name + ", finger " + fingerPosition);
                SendMessage("Enrolled Finger Mask: " + _enrollmentControl.EnrolledFingerMask);
                SendMessage("Disabled Finger Mask: " + _enrollmentControl.DisabledFingerMask);
            }
            else
            {
                SendMessage("OnDelete:  No Reader Connected, finger " + fingerPosition);
            }

            Fmds.Remove(fingerPosition);

            if (Fmds.Count == 0)
            {

            }
        }
        private void enrollment_OnEnroll(DPCtlUruNet.EnrollmentControl enrollmentControl, DataResult<Fmd> result, int fingerPosition)
        {
            if (enrollmentControl.Reader != null)
            {
                SendMessage("OnEnroll:  " + enrollmentControl.Reader.Description.Name + ", finger " + fingerPosition);
                SendMessage("Enrolled Finger Mask: " + _enrollmentControl.EnrolledFingerMask);
                SendMessage("Disabled Finger Mask: " + _enrollmentControl.DisabledFingerMask);
            }
            else
            {
                SendMessage("OnEnroll:  No Reader Connected, finger " + fingerPosition);
            }
            if (textboxID.Text == "")
            {
                Verify.ID();
            }

            else

          if (result != null && result.Data != null)
            {
                string value = Fmd.SerializeXml(result.Data);

                SqlParameter param = new SqlParameter();
                String sql = String.Format("insert into FingerprintSeafarers (CNIC, FingertempR, FingertempL) values('{0}','{1}','{1}')", textboxID.Text, value);
                SqlCommand cmd = new SqlCommand(sql, DbConnection.con);
                try
                {
                    DbConnection.con.Open();
                    cmd.ExecuteNonQuery();
                    DbConnection.con.Close();
                    MessageBox.Show("Fingerprint saved successfully", "Stored", buttons: MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
                DbConnection.con.Close();


                Fmds.Add(fingerPosition, result.Data);
            }

            btnCancel.Enabled = false;


        }
        private void enrollment_OnStartEnroll(DPCtlUruNet.EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            if (enrollmentControl.Reader != null)
            {
                SendMessage("OnStartEnroll:  " + enrollmentControl.Reader.Description.Name + ", finger " + fingerPosition);
            }
            else
            {
                SendMessage("OnStartEnroll:  No Reader Connected, finger " + fingerPosition);
            }

            btnCancel.Enabled = true;
        }
        #endregion

        /// <summary>
        /// Cancel enrollment when window is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>


        /// <summary>
        /// Close window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>


        /// <summary>
        /// Cancel enrollment when window is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        /// 



        private void SendMessage(string message)
        {
            txtMessage.Text += message + "\r\n\r\n";
            txtMessage.SelectionStart = txtMessage.TextLength;
            txtMessage.ScrollToCaret();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show("Are you sure you want to cancel this enrollment?", "Are You Sure?", buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                _enrollmentControl.Cancel();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FingerprintControlS_FormClosing(object sender, FormClosingEventArgs e)
        {
            _enrollmentControl.Cancel();
        }

      
        void FrameGrabber(object sender, EventArgs e)
        {

            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(276, 214, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            PassportImage.Image = currentFrame.ToBitmap();
        }

       

       

  
        private void btnStart_Click_1(object sender, EventArgs e)
        {
            grabber = new Emgu.CV.Capture();
            grabber.QueryFrame();

            //Initialize the FrameGraber event
            Application.Idle += new EventHandler(FrameGrabber);
            //button1.Enabled = false;
        }

        private void btnCapture_Click_1(object sender, EventArgs e)
        {
            //You'll want to unsubcribe from the event handler so this doesn't occur
            Application.Idle -= FrameGrabber;
            grabber.Dispose(); ;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                DbConnection.con.Close();
                if (DbConnection.con.State != ConnectionState.Open)
                {
                    DbConnection.con.Open();
                }

                SqlCommand cmd = new SqlCommand();
                SqlCommand comd = new SqlCommand("insert into SeafarersPassport (CNIC,Passport)values(@cnic,@pic)", DbConnection.con);

                MemoryStream stream = new MemoryStream();
                PassportImage.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] picc = stream.ToArray();

                comd.Parameters.Add(new SqlParameter("@cnic", textboxID.Text));

                comd.Parameters.AddWithValue("@pic", picc);

                if (DbConnection.con.State == System.Data.ConnectionState.Closed)
                {
                    DbConnection.con.Open();
                }
                comd.ExecuteNonQuery();

                Verify.Save();
                DbConnection.con.Close();
                Reset();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if ( textboxNIN.Text == "" )
            {
                Verify.Input();
            }

            else
            {
                DbConnection.con.Open();
                SqlCommand cmd = new SqlCommand("select SeafarerID from Seafarers where NIN='" + textboxNIN.Text + "'", DbConnection.con);
                SqlDataReader srd = cmd.ExecuteReader();
                while (srd.Read())
                {
                    textboxID.Text = srd.GetValue(0).ToString();
                }
                DbConnection.con.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            PassportImage.Image = null;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show("Are you sure you want to cancel this enrollment?", "Are You Sure?", buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                _enrollmentControl.Cancel();
            }
        }
    }
}
