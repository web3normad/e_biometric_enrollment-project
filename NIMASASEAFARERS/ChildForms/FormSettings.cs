using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIMASASEAFARERS
{
    public partial class FormSettings : UserControl
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
          
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }

        private void btnCreateUser_Click_1(object sender, EventArgs e)
        {
            CreateAdminUser cau = new CreateAdminUser();
            MainControlClass.showControl(cau, Content);
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            EditAdminUser edu = new EditAdminUser();
            MainControlClass.showControl(edu, Content);
        }
    }
}
