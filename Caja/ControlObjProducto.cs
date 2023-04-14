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
    public partial class ControlObjProducto : UserControl
    {
        public ControlObjProducto()
        {
            InitializeComponent();
        }

        private void ControlObjProducto_Load(object sender, EventArgs e)
        {
          
            this.BackgroundImageLayout = ImageLayout.Zoom;
        }
    }
}
