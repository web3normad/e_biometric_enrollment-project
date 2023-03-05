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
using CrystalDecisions.CrystalReports.Engine;

namespace NIMASASEAFARERS
{
    public partial class CardsApapa : UserControl
    {
        public CardsApapa()
        {
            InitializeComponent();
        }
        ReportDocument rd;

       

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CardsApapa_Load(object sender, EventArgs e)
        {

            //SqlConnection con = new SqlConnection("Server=tcp:nmlsdocks.database.windows.net,1433;Initial Catalog=NIMASAMLSBAK;Persist Security Info=False;User ID=nmls;Password=Seafarer2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
            //SqlCommand cmd = new SqlCommand("Select DockworkerID from Dockworkers", DbConnection.con);
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = cmd;
            //DataTable table1 = new DataTable();
            //da.Fill(table1);
            //DataRow itemrow = table1.NewRow();
            //itemrow[1] = "Select ID Number";
            //table1.Rows.InsertAt(itemrow, 0);

            //comboSearch.DataSource = table1;
            //comboSearch.DisplayMember = "DockworkerID";
            comboID.Text = "Select ID card by Dockworker ID";
        }

        private void comboID_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
    }
}
