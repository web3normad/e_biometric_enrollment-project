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
using System.Windows.Forms.DataVisualization.Charting;


namespace NIMASASEAFARERS
{
    public partial class Dashboard : UserControl
    {
        private string selected = string.Empty;
        public Dashboard()
        {

            InitializeComponent();
            GetData();
        }
        SqlConnection Con = new SqlConnection("Server=tcp:nmlsdocks.database.windows.net,1433;Initial Catalog=NIMASAMLSBAK;Persist Security Info=False;User ID=nmls;Password=Seafarer2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
        public void GetData()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from Seafarers ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SeafarerLbl.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sda1 = new SqlDataAdapter("Select count(*) from Dockworkers ", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            DockWorkersLbl.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("Select count(*) from DockworkersPh ", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            DockWorkersPhLbl.Text = dt2.Rows[0][0].ToString();

            SqlDataAdapter sda3 = new SqlDataAdapter("Select count(*) from DockworkersCal ", Con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            DockWorkersCalLbl.Text = dt3.Rows[0][0].ToString();

            SqlDataAdapter sda4 = new SqlDataAdapter("Select count(*) from DockworkersWa ", Con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            DockWorkersWaLbl.Text = dt4.Rows[0][0].ToString();


            int Total = Convert.ToInt32(dt.Rows[0][0].ToString()) + Convert.ToInt32(dt1.Rows[0][0].ToString())+ Convert.ToInt32(dt2.Rows[0][0].ToString())
                + Convert.ToInt32(dt3.Rows[0][0].ToString())+ Convert.ToInt32(dt4.Rows[0][0].ToString());
            TotalLbl.Text = "" + Total;
            Con.Close();

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
           //EnrollmentChart.Series["Enrollment Data"].Points.AddXY("Apapa");
            
            
            
            
            
            
            //string[] names = { ""};
            //panel1.BackColor = Color.White;
            //for (int i = names.Length - 1; i > -1; i--)
            //{
            //    var button = new Button()
            //    {
            //        BackColor = Color.Empty,
            //        Text = names[i],
            //        TextAlign = ContentAlignment.MiddleLeft,
            //        Dock = DockStyle.Top,
            //        Height = 25,
            //        FlatStyle = FlatStyle.Flat,
            //        Padding = new Padding(10, 0, 0, 0),
            //        FlatAppearance =
            //        {
            //             BorderSize = 0,
            //             MouseOverBackColor = Color.DodgerBlue
            //        }
            //    };

            //    {
            //        selected = button.Text;
            //        SelectBasicExamples(selected);
            //        panel2.Size = new Size(5, button.Height);
            //        panel2.Location = new Point(panel1.Width - 5, button.Top);
            //        panel2.BringToFront();
            //    };
            //    panel1.Controls.Add(button);
            //}

         

        }
      
        private void ApplyConfig()
        {
                 //chartReg.ApplyConfig(ConfigExamples.Light.Config(), Color.White);
        }


        private void SelectBasicExamples(string name)
        {
                //ApplyConfig();
                // BasicExamples.RoundedBar.Example(chartReg);
          
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
