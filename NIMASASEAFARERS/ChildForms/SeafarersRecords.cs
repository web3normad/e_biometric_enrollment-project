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
using DataTable = System.Data.DataTable;

namespace NIMASASEAFARERS
{
    public partial class SeafarersRecords : UserControl
    {
        public static string cnic;
        public SeafarersRecords()
        {
            InitializeComponent();
        }


        #region fill to Datagrid view
        public void gridView()
        {
            SqlCommand cmd = new SqlCommand("select SeafarerID as'ID Number',Surname as'Surname',FirstName as'First Name',MiddleName as'Middle Name',DOB as'D.O.B',Gender as'Gender', Age as 'Age', Height as'Height',NIN as'NIN',Email as 'Email',Mobile as 'Mobile', NOK as 'Next of Kin', State as 'State of Origin', Nimasa_RegNo as 'Nimasa Reg Number', Zone as 'Zone', Location as 'Location', Address as 'Address'From Seafarers order by ID asc", DbConnection.con);
            try
            {
                DbConnection.con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dataGridSeafarer.DataSource = dt;
                da.Update(dt);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DbConnection.con.Close();
        }
        #endregion
       

      

        
        private void SeafarersRecords_Load(object sender, EventArgs e)
        {
            gridView();
        }

        private void textSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {

            try
            {
             //   this.seafarersTableAdapter.SEARCH(this.bIOMETRICNIMASADataSet1.Seafarers, textSearch.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }



        }


      
        DataTable dt = new DataTable("Seafarers");
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            dataGridSeafarer.SelectAll();
            DataObject copydata = dataGridSeafarer.GetClipboardContent();
            if (copydata != null) Clipboard.SetDataObject(copydata);
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook xlWbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsheet;
            object miseddata = System.Reflection.Missing.Value;
            xlWbook = xlapp.Workbooks.Add(miseddata);

            xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWbook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range xlr = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1, 1];
            xlr.Select();

            xlsheet.PasteSpecial(xlr, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            EditSeafarer esf = new EditSeafarer();
            MainControlClass.showControl(esf, content);
        }
    }
}
