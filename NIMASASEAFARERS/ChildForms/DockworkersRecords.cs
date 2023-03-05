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

namespace NIMASASEAFARERS
{
    public partial class DockworkersRecords : UserControl
    {
        private int ID;

        public DockworkersRecords()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
        
      


    

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
              //  this.dockworkersTableAdapter.SearchDockworker(this.bIOMETRICNIMASADataSet.Dockworkers, textSearch.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

      
        private void DockworkersRecords_Load(object sender, EventArgs e)
        {
            gridView();
        }

        public void gridView()
        {
         
            SqlCommand cmd = new SqlCommand("select Dockworkers.DockworkerID as 'ID Number', Surname as 'Surname', FirstName as 'First Name', MiddleName as 'Middle Name', Dockworkers.DOB as 'DOB', Age as 'Age',Dockworkers.Gender as 'Gender', Dockworkers.Height as 'Height', Dockworkers.Location as'Location', Dockworkers.Rank as 'Rank', Dockworkers.Company as 'Company'from Dockworkers", DbConnection.con);
            try
            {
                DbConnection.con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
            //    dataGridDockworkers.DataSource = dt;
                da.Update(dt);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         //   dataGridDockworkers.AllowUserToAddRows = false;

            DbConnection.con.Close();
        }

        private void btnLag_Click(object sender, EventArgs e)
        {
            RecApapa rap = new RecApapa();
            MainControlClass.showControl(rap, content);
;
       }

        private void btnPh_Click(object sender, EventArgs e)
        {
            RecPh rph = new RecPh();
            MainControlClass.showControl(rph, content);
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            RecCal rcl = new RecCal();
            MainControlClass.showControl(rcl, content);
        }

        private void guna2Shapes3_Click(object sender, EventArgs e)
        {
            RecCal rcl = new RecCal();
            MainControlClass.showControl(rcl, content);
        }

        private void imgPh_Click(object sender, EventArgs e)
        {
            RecPh rph = new RecPh();
            MainControlClass.showControl(rph, content);
        }

        private void btnAp_Click(object sender, EventArgs e)
        {
            RecApapa rap = new RecApapa();
            MainControlClass.showControl(rap, content);
        }

        private void btnWa_Click(object sender, EventArgs e)
        {
            RecWarri rwa = new RecWarri();
            MainControlClass.showControl(rwa, content);
        }

        private void imgWa_Click(object sender, EventArgs e)
        {
            RecWarri rwa = new RecWarri();
            MainControlClass.showControl(rwa, content);
        }
    }
}
