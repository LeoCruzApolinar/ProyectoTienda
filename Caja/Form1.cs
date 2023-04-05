using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caja
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ServicioWeb.ServicioASoapClient servicioA = new ServicioWeb.ServicioASoapClient();
        ServicioWeb.Marca marca = new ServicioWeb.Marca();
        List<ServicioWeb.Marca> ListaMarca = new List<ServicioWeb.Marca>(); 
        ServicioWeb.Producto producto = new ServicioWeb.Producto();
        ServicioWeb.Categoria categoria = new ServicioWeb.Categoria();
        ServicioWebTienda.Imagenes imagenes = new ServicioWebTienda.Imagenes();
        public string URLMarcaImagen = null;
        private void BtnAgregarMarca_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (TBDescricionMarca.Text != "" && TBNombreMarca.Text != "" && PBMarca.Image != null && URLMarcaImagen != null)
                {
                    ServicioWeb.Marca marcaA = new ServicioWeb.Marca()
                    {
                        Nombre = TBNombreMarca.Text,
                        Descripcion = TBDescricionMarca.Text,
                        Logo = URLMarcaImagen,
                    };
                    if (servicioA.AgregarMarca(marcaA))
                    {
                        MessageBox.Show("Bien");
                    }
                    else
                    {
                        MessageBox.Show("Mal");
                    }
                    ActualizarListas(1);
                    TBDescricionMarca.Text = null;
                    TBNombreMarca.Text = null;
                    PBMarca.Image = null;
                    URLMarcaImagen = null;
                }
                else
                {
                    MessageBox.Show("Todos lo campos deben de estar llenos");
                }
            }
        }
        private  async Task<string>  ObtenerUrlImagenMarca() 
        {
            string url = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Filtro de archivos para que sólo se muestren imágenes
            openFileDialog1.Filter = "Archivos de imagen (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png";

            // Se muestra el diálogo y se espera a que el usuario seleccione un archivo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Se obtiene la ruta del archivo seleccionado
                string rutaImagen = openFileDialog1.FileName;

                // Aquí puedes hacer lo que quieras con la imagen seleccionada, por ejemplo:
                PBMarca.Image = Image.FromFile(rutaImagen);

                MemoryStream ms = new MemoryStream();
                Image.FromFile(rutaImagen).Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] archivoB = ms.ToArray();
                var x = await servicioA.SubirImagenAsync(5, archivoB, TBNombreMarca.Text + "Imagen");
                url =  x.Body.SubirImagenResult;
                URLMarcaImagen = url;
                return url;
            }
            return url;
        }

        private void BtnAgregarImagenMarca_Click(object sender, EventArgs e)
        {
            if (TBNombreMarca.Text != "")
            {
                ObtenerUrlImagenMarca();
            }
            else 
            {
                MessageBox.Show("Primero debe asignar un nombre a la marca");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ActualizarListas(1);
        }
        private void ActualizarListas(int orden) 
        {

            
            switch (orden)
            {
                case 1:
                    ListaMarca = null;
                    LBMarca.Items.Clear();
                    string a = servicioA.ObtenerMarca();
                    ListaMarca = JsonConvert.DeserializeObject<List<ServicioWeb.Marca>>(servicioA.ObtenerMarca());
                    for (int i = 0; i < ListaMarca.Count; i++)
                    {
                        LBMarca.Items.Add(ListaMarca[i].Nombre);
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
        private void LBMarca_DoubleClick(object sender, EventArgs e)
        {
            if (LBMarca.SelectedItem != null)
            {
                TBDescricionMarca.Text = null;
                TBNombreMarca.Text = null;
                PBMarca.Image = null;
                URLMarcaImagen = null;
                Dictionary<string, ServicioWeb.Marca> diccionario = ListaMarca.ToDictionary(m => m.Nombre);
                try
                {
                    WebClient wc = new WebClient();
                    byte[] bytes = wc.DownloadData(diccionario[LBMarca.SelectedItem.ToString()].Logo);
                    MemoryStream ms = new MemoryStream(bytes);
                    URLMarcaImagen = diccionario[LBMarca.SelectedItem.ToString()].Logo;
                    Image imagen = Image.FromStream(ms);
                    PBMarca.Image = imagen;

                }
                catch (Exception)
                {
                }
                TBNombreMarca.Text = diccionario[LBMarca.SelectedItem.ToString()].Nombre;
                TBDescricionMarca.Text = diccionario[LBMarca.SelectedItem.ToString()].Descripcion;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TBDescricionMarca.Text = null;
            TBNombreMarca.Text = null;
            PBMarca.Image = null;
            URLMarcaImagen = null;
        }
        private void BtnEliminarMarca_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (LBMarca.SelectedItem != null)
                {
                    Dictionary<string, ServicioWeb.Marca> diccionario = ListaMarca.ToDictionary(m => m.Nombre);
                    if (servicioA.EliminarMarca(diccionario[LBMarca.SelectedItem.ToString()].Id))
                    {

                        TBDescricionMarca.Text = null;
                        TBNombreMarca.Text = null;
                        PBMarca.Image = null;
                        URLMarcaImagen = null;
                        ActualizarListas(1);
                    }
                    else
                    {
                        MessageBox.Show("La marca no puede ser eliminada, verifique si esta asociada algun producto");
                    }

                }
                else
                {
                    MessageBox.Show("Debe seleccionar una marca");
                }
            }
        }//bien

        private void BtnActualizarMarca_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (TBDescricionMarca.Text != "" && TBNombreMarca.Text != "" && PBMarca.Image != null && URLMarcaImagen != null)
                {
                    if (LBMarca.SelectedItem != null)
                    {
                        Dictionary<string, ServicioWeb.Marca> diccionario = ListaMarca.ToDictionary(m => m.Nombre);
                        if (URLMarcaImagen == null) { URLMarcaImagen = "imagen"; }
                        ServicioWeb.Marca marcaA = new ServicioWeb.Marca()
                        {
                            Id = diccionario[LBMarca.SelectedItem.ToString()].Id,
                            Nombre = TBNombreMarca.Text,
                            Descripcion = TBDescricionMarca.Text,
                            Logo = URLMarcaImagen,
                        };
                        if (servicioA.ActualizarMarca(marcaA))
                        {
                            MessageBox.Show("bien");
                            ActualizarListas(1);
                        }
                        else
                        {
                            MessageBox.Show("Mal");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar una marca");
                    }
                }
                else
                {
                    MessageBox.Show("Todos lo campos deben de estar llenos");
                }
            }
            
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void BtnEliminarProducto_Click(object sender, EventArgs e)
        {

        }

        private void BtnActualizarProducto_Click(object sender, EventArgs e)
        {

        }
    }
}
