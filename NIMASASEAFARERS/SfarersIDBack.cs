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
    public partial class SfarersIDBack : Form
    {

        DataTable dtable = new DataTable();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection("Server=tcp:nimasamlsserver.database.windows.net,1433;Initial Catalog=NIMASAMLS.MDF;Persist Security Info=False;User ID=nimasamls;Password=Seafarer2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");

        public SfarersIDBack()
        {
            InitializeComponent();
        }

        private void btnGenerateID_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Server=tcp:nimasamlsserver.database.windows.net,1433;Initial Catalog=NIMASAMLS.MDF;Persist Security Info=False;User ID=nimasamls;Password=Seafarer2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
            con.Open();

            string sql = "select Barcode from Seafarers where SeafarerID='" + textboxCnic.Text + "'";
            SqlCommand command = new SqlCommand(sql, con);
            DbConnection.con.Close();

            using (SqlDataReader dr = command.ExecuteReader())
            {
                if (dr.Read())
                { 

                    MemoryStream ms1 = new MemoryStream((byte[])dr["Barcode"]);
                    barcode.Image = new Bitmap(ms1);
                }
            }
        }

        private void btnFrontView_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
