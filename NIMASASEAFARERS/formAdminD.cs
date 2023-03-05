using NIMASASEAFARERS.ChildForms;
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
    public partial class formAdminD : UserControl
    {
        public formAdminD()
        {
            InitializeComponent();
        }

        private void btnUserAd_Click(object sender, EventArgs e)
        {
            CreateAdminUser cau = new CreateAdminUser();
            MainControlClass.showControl(cau, Content);
           
        }

        private void btnSupAd_Click(object sender, EventArgs e)
        {

            SuperAdmin cau = new SuperAdmin();
            MainControlClass.showControl(cau, Content);
        }
    }
}
