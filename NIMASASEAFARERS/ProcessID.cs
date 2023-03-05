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





namespace NIMASASEAFARERS
{
    public partial class ProcessID : Form
    {
    
        DataTable dtable = new DataTable();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection("Server = tcp:nimasamlsserver.database.windows.net, 1433; Initial Catalog = NIMASAMLS.MDF; Persist Security Info=False;User ID = nimasamls; Password=Seafarer2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30");

        public ProcessID()
        {
            InitializeComponent();
        }

        private void Clock_In_Load(object sender, EventArgs e)
        {

            try
            {

                if (!(txtStaff_ID.Text.Equals("")))
                {
                    SqlConnection con = new SqlConnection("Server = tcp:nimasamlsserver.database.windows.net,1433; Initial Catalog = NIMASAMLS.MDF; Persist Security Info = False; User ID = nimasamls; Password = Seafarer2; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30");
                    con.Open();

                    string strcom = "select Dockworkers.Surname, Dockworkers.FirstName, Dockworkers.MiddleName, Dockworkers.DOB, Dockworkers.Gender, Dockworkers.Height, Dockworkers.Location, Dockworkers.Rank, DockworkersPassport.Passport from Dockworkers inner join DockworkersPassport on Dockworkers.DockworkerID= DockworkersPassport.CNIC ";
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



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnPrint_Click(object sender, EventArgs e)
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
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            Bitmap bmp = new Bitmap(groupBox1.ClientSize.Width, groupBox1.ClientSize.Height);

            float tgtWidthMM = 100;  // 5cm
            float tgtHeightMM = 50;  // 3.5cm
            float tgtWidthInches = tgtWidthMM / 25.4f;
            float tgtHeightInches = tgtHeightMM / 25.4f;
            float srcWidthPx = bmp.Width; //  633
            float srcHeightPx = bmp.Height; //  381
            float dpiX = srcWidthPx / tgtWidthInches;
            float dpiY = srcHeightPx / tgtHeightInches;

            bmp.SetResolution(150, 150);
           
            groupBox1.DrawToBitmap(bmp, groupBox1.ClientRectangle);
          
            e.Graphics.DrawImage((Image)bmp, x, y);
          
            //Bitmap bmp = new Bitmap(this.groupBox1.Width, this.groupBox1.Height);
            //this.groupBox1.DrawToBitmap(bmp, new Rectangle(0, 0, this.groupBox1.Width, this.groupBox1.Height));
            //e.Graphics.DrawImage(bmp, 150, 50);


            //Bitmap bmp1 = new Bitmap(this.groupBox2.Width, this.groupBox2.Height);
            //this.groupBox2.DrawToBitmap(bmp1, new Rectangle(0, 0, this.groupBox2.Width, this.groupBox2.Height));
            //e.Graphics.DrawImage(bmp1, 150, 350);
        }


        private Bitmap PrintBmp;
       
        public void PD_PrintPage(object sender, PrintPageEventArgs e)
        {
            double WidthRatio = e.MarginBounds.Width / (double)PrintBmp.Width;
            double HeightRatio = e.MarginBounds.Height / (double)PrintBmp.Height;
            if (WidthRatio > HeightRatio)
            {
                e.Graphics.DrawImage(PrintBmp, 0, 0, Convert.ToInt32(HeightRatio * e.MarginBounds.Width), e.MarginBounds.Height);
            }
            else
            {
                e.Graphics.DrawImage(PrintBmp, 0, 0, e.MarginBounds.Width, Convert.ToInt32(WidthRatio * e.MarginBounds.Height));
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

}

