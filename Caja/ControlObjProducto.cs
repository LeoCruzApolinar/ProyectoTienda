using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caja
{
    public partial class ControlObjProducto : UserControl
    {
        public string Desencriptar(string X)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(X);
                string cadena = System.Text.Encoding.UTF8.GetString(bytes);
                return cadena;
            }
            catch (Exception)
            {

                return X;
            }

        }
        private ProductoOBJ _ProductoOBJ;
        public ControlObjProducto(ProductoOBJ Cproducto)
        {
            InitializeComponent();
            string urlImagen = Desencriptar(Cproducto.ImagenPrincipal);
            WebClient webClient = new WebClient();
            byte[] bytesImagen = webClient.DownloadData(urlImagen);
            MemoryStream streamImagen = new MemoryStream(bytesImagen);
            Image imagen = Image.FromStream(streamImagen);
            pictureBox1.Image = imagen;
            label1.Text = Cproducto.Nombre;
            _ProductoOBJ = Cproducto;
        }

        private void ControlObjProducto_Load(object sender, EventArgs e)
        {
          
            this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OnProductoSeleccionado(_ProductoOBJ);
        }
        public event EventHandler<ProductoOBJ> ProductoSeleccionado;

        private void OnProductoSeleccionado(ProductoOBJ X)
        {
            ProductoSeleccionado?.Invoke(this, X);
        }
    }
}
