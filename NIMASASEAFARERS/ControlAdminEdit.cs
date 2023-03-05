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
    public partial class ControlAdminEdit : UserControl
    {
        public ControlAdminEdit()
        {
            InitializeComponent();
        }

        private void btnSupAd_Click(object sender, EventArgs e)
        {
            EditSuperAdmin esa = new EditSuperAdmin();
            MainControlClass.showControl(esa, Content);
        }

        private void btnUserAd_Click(object sender, EventArgs e)
        {
            EditAdminUser eau = new EditAdminUser();
            MainControlClass.showControl(eau, Content);
        }
    }
}
