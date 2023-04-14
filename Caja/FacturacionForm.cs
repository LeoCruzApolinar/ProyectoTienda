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
            //tableLayoutPanel3.Paint += new PaintEventHandler(tableLayoutPanel3_Paint);
            tableLayoutPanel4.Paint += new PaintEventHandler(tableLayoutPanel4_Paint);
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

            //ControlPaint.DrawBorder(e.Graphics, tableLayoutPanel3.ClientRectangle, Color.Black,
            //    1, ButtonBorderStyle.Solid, Color.Black, 1, ButtonBorderStyle.Solid,
            //    Color.Black, 1, ButtonBorderStyle.Solid, Color.Transparent, 0,
            //    ButtonBorderStyle.None);

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, tableLayoutPanel4.ClientRectangle, Color.Black,
                0, ButtonBorderStyle.Solid, Color.Gray, 1, ButtonBorderStyle.Solid,
                Color.Black, 0, ButtonBorderStyle.Solid, Color.Transparent, 0,
                ButtonBorderStyle.None);
        }
    }
}
