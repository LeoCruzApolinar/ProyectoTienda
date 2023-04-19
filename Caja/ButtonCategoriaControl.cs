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
    public partial class ButtonCategoriaControl : UserControl
    {
        private int _idCategoria;
        public ButtonCategoriaControl(int id, string nombre)
        {
            InitializeComponent();
            _idCategoria = id;
            label1.Text = nombre;
            this.DoubleClick += panel1_DoubleClick;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            OnCategoriaSeleccionada(_idCategoria);
        }
        public event EventHandler<int> CategoriaSeleccionada;

        private void OnCategoriaSeleccionada(int id)
        {
            CategoriaSeleccionada?.Invoke(this, id);
        }
    }
}
