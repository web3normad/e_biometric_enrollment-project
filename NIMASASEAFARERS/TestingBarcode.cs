﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace NIMASASEAFARERS
{
    public partial class TestingBarcode : Form
    {
        public TestingBarcode()
        {
            InitializeComponent();
        }


        private void barcode_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            string sql = "select Dockworkers.DockworkerID, Dockworkers.Surname, Dockworkers.FirstName, Dockworkers.MiddleName, Dockworkers.DOB,Dockworkers.Gender, Dockworkers.Age, Dockworkers.Location, Dockworkers.Rank, Dockworkers.Company, Dockworkers.Zone, DockworkersPassport.Passport from Dockworkers inner join DockworkersPassport on Dockworkers.DockworkerID= DockworkersPassport.CNIC  where DockworkerID='" + barcode.Text + "'";
            SqlDataReader reader;


            DbConnection.con.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = DbConnection.con;
            reader = cmd.ExecuteReader();


            if (reader.Read() == true)
            {

                txtIdNum.Text = reader["DockworkerID"].ToString();
                txtSurname.Text = reader["Surname"].ToString();
                txtFirstName.Text = reader["FirstName"].ToString();
                txtMiddleName.Text = reader["MiddleName"].ToString();
                txtDOB.Text = reader["DOB"].ToString();
                txtGender.Text = reader["Gender"].ToString();
                txtAge.Text = reader["Age"].ToString();
                txtCompany.Text = reader["Company"].ToString();
                txtLocation.Text = reader["Location"].ToString();
                txtZone.Text = reader["Zone"].ToString();
                txtRank.Text = reader["Rank"].ToString();





                //txtZone.Text = reader.GetString(11);


                //MemoryStream ms = new MemoryStream((byte[])reader.GetString("Passport"));
                MemoryStream ms = new MemoryStream((byte[])reader["Passport"]);
                pictureBox1.Image = new Bitmap(ms);
                barcode.SelectAll();
            }
            else if (reader.Read() == false)
            {
                txtIdNum.Text = "Dock Worker not Found";
                txtSurname.Text = "";
                txtFirstName.Text = "";
                txtMiddleName.Text = "";
                txtDOB.Text = "";
                txtGender.Text = "";
                txtLocation.Text = "";
                txtCompany.Text = "";
                txtZone.Text = "";
                txtRank.Text = "";
                pictureBox1.Image = null;

            }
            DbConnection.con.Close();
        }
        int timeLeft = 3;
        int timeLeft1 = 20;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                lbl_time.Text = timeLeft + "Seconds";
                barcode.Select();
            }
            else
            {
                barcode.SelectAll();
            }
            if (timeLeft1 > 0)
            {
                timeLeft1 = timeLeft1 - 1;
                lbl_timer1.Text = timeLeft + "";
            }
            if (lbl_timer1.Text == "15")
            {
                txtIdNum.Text = "Scan Barcode";
                txtSurname.Text = "";
                txtFirstName.Text = "";
                txtMiddleName.Text = "";
                txtDOB.Text = "";
                txtGender.Text = "";
                txtLocation.Text = "";
                txtCompany.Text = "";
                txtZone.Text = "";
                txtRank.Text = "";
                barcode.Select();
                barcode.SelectAll();
            }
        }
    }


        
    
}
        
    
   


        //    if (timeLeft1 == 0)
        //    {
        //        timeLeft1 = 20;
        //    }
        //}
    


       