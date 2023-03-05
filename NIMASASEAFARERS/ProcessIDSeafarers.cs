using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace NIMASASEAFARERS
{
    public partial class ProcessIDSeafarers : Form
    {

        DataTable dtable = new DataTable();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection("Server = tcp:nimasamlsserver.database.windows.net, 1433; Initial Catalog = NIMASAMLS.MDF; Persist Security Info=False;User ID = nimasamls; Password=Seafarer2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30");
        PrintDocument printdoc1 = new PrintDocument();
        PrintPreviewDialog previewdlg = new PrintPreviewDialog();
        Panel pannel = null;
        public ProcessIDSeafarers()
        {
            InitializeComponent();
        }

     
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    


        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void ProcessIDSeafarers_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void ProcessIDSeafarers_Load(object sender, EventArgs e)
        {
            try
            {

                if (!(txtStaff_ID.Text.Equals("")))
                {
                    SqlConnection con = new SqlConnection("Server = tcp:nimasamlsserver.database.windows.net,1433; Initial Catalog = NIMASAMLS.MDF; Persist Security Info = False; User ID = nimasamls; Password = Seafarer2; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30");
                    con.Open();

                    string strcom = "select Seafarers.Surname, Seafarers.FirstName, Seafarers.MiddleName, Seafarers.DOB, Seafarers.Gender, Seafarers.Height, Seafarers.Location, Seafarers.Rank, SeafarersPassport.Passport from Seafarers inner join SeafarersPassport on Seafarers.SeafarerID= SeafarersPassport.CNIC ";
                    SqlDataAdapter daDetails = new SqlDataAdapter(strcom, con);
                    DataSet dsDetails = new DataSet();
                    daDetails.Fill(dsDetails);

                    if (dsDetails.Tables[0].Rows.Count > 0)
                    {
                        txtStaff_Name.Text = dsDetails.Tables[0].Rows[0]["Surname"].ToString();
                        textFirstName.Text = dsDetails.Tables[0].Rows[0]["FirstName"].ToString();
                        textMiddleName.Text = dsDetails.Tables[0].Rows[0]["MiddleName"].ToString();
                        dateTimeDOB.Text = dsDetails.Tables[0].Rows[0]["DOB"].ToString();
                        textGender.Text = dsDetails.Tables[0].Rows[0]["Gender"].ToString();
                        textHeight.Text = dsDetails.Tables[0].Rows[0]["Height"].ToString();
                        textRank.Text = dsDetails.Tables[0].Rows[0]["Rank"].ToString();
                        textLocation.Text = dsDetails.Tables[0].Rows[0]["Location"].ToString();


                        MemoryStream ms = new MemoryStream((byte[])dsDetails.Tables[0].Rows[0]["Passport"]);
                        pix.Image = new Bitmap(ms);

                        
                    }


                    con.Close();

                }
                else
                {

                    MessageBox.Show("ID can't be Empty!", "Fingerprint Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();

            doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("PaperA4", 840, 1180);
            doc.PrintPage += this.Doc_PrintPage;
            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;
            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }

        }
   


        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
           
            Bitmap bmp = new Bitmap(this.groupBox1.Width, this.groupBox1.Height);
            bmp.SetResolution(300, 300);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            this.groupBox1.DrawToBitmap(bmp, new Rectangle(0, 0, this.groupBox1.Width, this.groupBox1.Height));
            e.Graphics.DrawImage(bmp, 150, 50);

            
            //Graphics _graphics = Graphics.FromImage(bmp);
            //_graphics.DrawImage(bmp, new Rectangle(0, 0, groupBox1.Width, groupBox1.Height),
            // new Rectangle(0, 0, groupBox1.Width, groupBox1.Height), GraphicsUnit.Pixel) ;

            //Bitmap bmp1 = new Bitmap(this.groupBox2.Width, this.groupBox2.Height);
            //this.groupBox2.DrawToBitmap(bmp1, new Rectangle(0, 0, this.groupBox2.Width, this.groupBox2.Height));
            //e.Graphics.DrawImage(bmp1, 150, 350);
        }
      

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
