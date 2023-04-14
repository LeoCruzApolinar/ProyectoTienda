using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caja
{
    public partial class FacturacionForm : Form
    {
        public FacturacionForm()
        {
            InitializeComponent();
        }

        private void FacturacionForm_Load(object sender, EventArgs e)
        {

            label1.Location = new Point(
            (panel2.Width - label1.Width) / 2,
            (panel2.Height - label1.Height) / 2);
        }
    }
}
