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
    public partial class IdCardControl : UserControl
    {
        public IdCardControl()
        {
            InitializeComponent();
        }

        private void btnApapa_Click(object sender, EventArgs e)
        {
            CardsApapa cda = new CardsApapa();
            MainControlClass.showControl(cda, content);
        }

        private void btnPh_Click(object sender, EventArgs e)
        {
            CardsPh cdp = new CardsPh();
            MainControlClass.showControl(cdp, content);
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            CardsCal cdc = new CardsCal();
            MainControlClass.showControl(cdc, content);
        }

        private void btnWarri_Click(object sender, EventArgs e)
        {
            CardsWa cdw = new CardsWa();
            MainControlClass.showControl(cdw, content);
        }
    }
}
