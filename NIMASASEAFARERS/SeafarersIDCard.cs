using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace NIMASASEAFARERS
{
    public partial class SeafarersIDCard : Form
    {
        DataTable dtable = new DataTable();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection("Server=tcp:nimasamlsserver.database.windows.net,1433;Initial Catalog=NIMASAMLS.MDF;Persist Security Info=False;User ID=nimasamls;Password=Seafarer2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");

       
        public SeafarersIDCard()
        {
            InitializeComponent();

        }

    

        private void btnGenerateID_Click(object sender, EventArgs e)
        {
            if (!txtStaff_ID.Text.Equals(""))
            {
                SqlConnection con = new SqlConnection("Server=tcp:nimasamlsserver.database.windows.net,1433;Initial Catalog=NIMASAMLS.MDF;Persist Security Info=False;User ID=nimasamls;Password=Seafarer2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
                con.Open();

                string sql = "select Seafarers.SeafarerID, Seafarers.Surname, Seafarers.FirstName, Seafarers.MiddleName, Seafarers.DOB, Seafarers.Gender, Seafarers.Height, Seafarers.Location, Seafarers.Rank, Seafarers.DateIssue, Seafarers.DateExpiry, Seafarers.Barcode, SeafarersPassport.Passport from Seafarers inner join SeafarersPassport on Seafarers.SeafarerID= SeafarersPassport.CNIC  where SeafarerID='" + textboxCnic.Text + "'";
                SqlCommand command = new SqlCommand(sql, con);
                DbConnection.con.Close();

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    if (dr.Read())
                    {



                        txtStaff_ID.Text = dr["SeafarerID"].ToString();
                        txtStaff_Name.Text = dr["Surname"].ToString();
                        textFirstName.Text = dr["FirstName"].ToString();
                        textMiddleName.Text = dr["MiddleName"].ToString();
                        dateTimeDOB.Text = dr["DOB"].ToString();
                        textGender.Text = dr["Gender"].ToString();
                        textHeight.Text = dr["Height"].ToString();
                        textRank.Text = dr["Rank"].ToString();
                        textLocation.Text = dr["Location"].ToString();

                        dateTimeIssue.Text = dr["DateIssue"].ToString();
                        dateTimeExpiry.Text = dr["DateExpiry"].ToString();


                        MemoryStream ms = new MemoryStream((byte[])dr["Passport"]);
                        pix.Image = new Bitmap(ms);

                        
                    }
                }
            }
        }

        private Form active = null;
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
            panelIDCard.Controls.Add(Child);
            panelIDCard.Tag = Child;
            Child.BringToFront();
            Child.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Opening(new SfarersIDBack());
        }

        private void textboxCnic_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
