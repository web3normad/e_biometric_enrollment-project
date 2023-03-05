using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using NIMASASEAFARERS.Properties;
using System.Data.SqlClient;


namespace NIMASASEAFARERS
{
    public partial class AdminDashboard : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        bool RegCollapsed;
        bool EnrollCollapsed;
        bool VerifyCollapsed;
        private bool isCollapsed;
        public AdminDashboard()
        {

            InitializeComponent();
            hideSubMenu();


            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 45);
            panelSidebar.Controls.Add(leftBorderBtn);
            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            // this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void hideSubMenu()
        {
            panelReg.Visible = false;
            panelEnroll.Visible = false;
         
            panelID.Visible = false;
            panelAdmin.Visible = false;
            panelRecords.Visible = false;
            panelDock.Visible = false;
            panelDockEnroll.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(60, 179, 113);
            public static Color color2 = Color.FromArgb(249, 249, 249);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);

        }
        //methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(249, 249, 249);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //Icon Current Child Form
               

            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(249, 249, 249);
                currentBtn.ForeColor = Color.Black;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Black;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        private void AdminDashboard_Load(object sender, EventArgs e)
        {

            Dashboard db = new Dashboard();
            MainControlClass.showControl(db, Content);
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


       

        private void panelNav_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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
            Content.Controls.Add(Child);
            Content.Tag = Child;
            Child.BringToFront();
            Child.Show();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {

        }

    


        private void btnRegistration_Click(object sender, EventArgs e)
        {
            showSubMenu(panelReg);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnEnrollment_Click(object sender, EventArgs e)
        {
            showSubMenu(panelEnroll);
        }


        private void btnRSeafarer_Click_1(object sender, EventArgs e)
        {
            FormEnroll fe = new FormEnroll();
            MainControlClass.showControl(fe, Content);
            hideSubMenu();
        }

        private void btnRDockworker_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panelDock);
        }

        private void btnSEnrollment_Click(object sender, EventArgs e)
        {
            Opening(new FingerprintControlS());
            hideSubMenu();
        }

        private void btnDEnrollment_Click(object sender, EventArgs e)
        {
            Opening(new FingerprintControl());
            hideSubMenu();
        }

        private void btnSVerification_Click(object sender, EventArgs e)
        {
            FormVerifySeafarer fvs = new FormVerifySeafarer();
            MainControlClass.showControl(fvs, Content);
            hideSubMenu();
        }

        private void btnDVerification_Click(object sender, EventArgs e)
        {
            FormVerify fv = new FormVerify();
            MainControlClass.showControl(fv, Content);
            hideSubMenu();
        }

      

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            Dashboard db = new Dashboard();
            MainControlClass.showControl(db, Content);
        }


        private void Content_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

    

        private void btnLogoout_Click_1(object sender, EventArgs e)
        {
            AdminLogin formAdmin = new AdminLogin();
            formAdmin.Show();
            this.Hide();
        }


      

        private void Content_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelVerify_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnIDCard_Click_2(object sender, EventArgs e)
        {
            showSubMenu(panelID);
        }

        private void btnSID_Click_2(object sender, EventArgs e)
        {
            
        }

        private void btnDID_Click_2(object sender, EventArgs e)
        {
         
        }

        private void btnRecords_Click_2(object sender, EventArgs e)
        {
            showSubMenu(panelRecords);
        }

        private void btnSeaRecord_Click_1(object sender, EventArgs e)
        {
            SeafarersRecords sfr = new SeafarersRecords();
            MainControlClass.showControl(sfr, Content);
            hideSubMenu();
        }

        private void btnDockRecord_Click_1(object sender, EventArgs e)
        {
            DockworkersRecords dwr = new DockworkersRecords();
            MainControlClass.showControl(dwr, Content);
            hideSubMenu();
        }

        private void btnSettings_Click_2(object sender, EventArgs e)
        {
            showSubMenu(panelAdmin);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateAdminUser cau = new CreateAdminUser();
            MainControlClass.showControl(cau, Content);
            hideSubMenu();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditAdminUser eau = new EditAdminUser();
            MainControlClass.showControl(eau, Content);
            hideSubMenu();
        }



        private void btnLag_Click_1(object sender, EventArgs e)
        {
            formRegDockWorker fed = new formRegDockWorker();
            MainControlClass.showControl(fed, Content);
            hideSubMenu();
        }

        private void btnPh_Click_1(object sender, EventArgs e)
        {
            FormRegDockworkerPh fdh = new FormRegDockworkerPh();
            MainControlClass.showControl(fdh, Content);
            hideSubMenu();
        }

        private void btnCal_Click_1(object sender, EventArgs e)
        {
            FormRegDockworkerCal frc = new FormRegDockworkerCal();
            MainControlClass.showControl(frc, Content);
            hideSubMenu();
        }

        private void btnWa_Click_1(object sender, EventArgs e)
        {
            FormRegDockworkerWa frw = new FormRegDockworkerWa();
            MainControlClass.showControl(frw, Content);
            hideSubMenu();
        }

        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            formAdminD fus = new formAdminD ();
            MainControlClass.showControl(fus, Content);
            hideSubMenu();
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            ControlAdminEdit cae = new ControlAdminEdit();
            MainControlClass.showControl(cae, Content);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            showSubMenu(panelAdmin);
        }

        private void btnRecords_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panelRecords);
        }

        private void btnDockRecord_Click(object sender, EventArgs e)
        {
            DockworkersRecords dwr = new DockworkersRecords();
            MainControlClass.showControl(dwr, Content);
        }

        private void btnEnrollment_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panelEnroll);
        }

        private void btnDEnrollment_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panelDockEnroll);
        }

        private void btnBioLag_Click(object sender, EventArgs e)
        {
            Opening(new FingerprintControl());
            hideSubMenu();
        }

        private void btnBioPh_Click(object sender, EventArgs e)
        {
            Opening(new EnrollBioPh());
            hideSubMenu();
        }

        private void btnBioCal_Click(object sender, EventArgs e)
        {
            Opening(new EnrollBioCal());
            hideSubMenu();
        }

        private void btnBioWa_Click(object sender, EventArgs e)
        {
            Opening(new EnrollBioWa());
            hideSubMenu();
        }

        private void btnDID_Click_1(object sender, EventArgs e)
        {
            CardsApapa cd = new CardsApapa();
            MainControlClass.showControl(cd, Content);
        }

        private void btnIDCard_Click_3(object sender, EventArgs e)
        {
            showSubMenu(panelID);
        }

        private void btnSeaRecord_Click(object sender, EventArgs e)
        {
            SeafarersRecords sfr = new SeafarersRecords();
            MainControlClass.showControl(sfr, Content);
        }

        private void btnSEnrollment_Click_1(object sender, EventArgs e)
        {
            Opening(new FingerprintControlS());
            hideSubMenu();
        }

        private void btnSID_Click(object sender, EventArgs e)
        {

        }
    }
}



