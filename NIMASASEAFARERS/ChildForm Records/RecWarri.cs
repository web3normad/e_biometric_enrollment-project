using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIMASASEAFARERS
{
    public partial class RecWarri : UserControl
    {
        public RecWarri()
        {
            InitializeComponent();
        }
      

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataGridWarri.SelectAll();
            DataObject copydata = dataGridWarri.GetClipboardContent();
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
            EditDockworkerWa edw = new EditDockworkerWa();
            MainControlClass.showControl(edw, content);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DockworkersRecords dwr = new DockworkersRecords();
            MainControlClass.showControl(dwr, content);
        }



        #region fill to Datagrid view
        public void gridView()
        {
            SqlCommand cmd = new SqlCommand("select DockworkerID as'Dockworker ID',Surname as'Surname',FirstName as'First Name',MiddleName as'Middle Name',DOB as'D.O.B',Gender as'Gender', Age as 'Age', Height as'Height', NIN as'NIN', Genotype as 'Genotype', BloodGroup as 'Blood Group', Email as 'Email',Mobile as 'Mobile', NOK as 'Next of Kin',Relationship as 'Relationship', State as 'State of Origin', Rank as 'Rank', Zone as 'Zone', Location as 'Location', Company as 'Company', Address as 'Address', EnrollmentDate as 'Date of Enrollment' From DockworkersWa order by ID asc", DbConnection.con);
            try
            {
                DbConnection.con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dataGridWarri.DataSource = dt;
                da.Update(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DbConnection.con.Close();
        }
        #endregion

        private void RecWarri_Load(object sender, EventArgs e)
        {
            gridView();
        }
    }
}
