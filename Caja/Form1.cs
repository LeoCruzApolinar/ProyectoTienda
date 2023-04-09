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
        List<ServicioWeb.Categoria> ListaCategoria = new List<ServicioWeb.Categoria>(); 
        ServicioWeb.Producto producto = new ServicioWeb.Producto();
        List<ServicioWeb.Producto> ListaProducto = new List<ServicioWeb.Producto>();
        ServicioWeb.Categoria categoria = new ServicioWeb.Categoria();
        ServicioWebTienda.Imagenes imagenes = new ServicioWebTienda.Imagenes();
        public string URLMarcaImagen = null;
        public string URLProductoImagen = null;
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
            ActualizarListas(2);
            ActualizarListas(3);
            TBEstadoProducto.Items.Add("true");
            TBEstadoProducto.Items.Add("False");
        }
        private void ActualizarListas(int orden) 
        {

            
            switch (orden)
            {
                case 1:
                    ListaMarca = null;
                    TBMarcaProducto.Items.Clear();
                    LBMarca.Items.Clear();
                    ListaMarca = JsonConvert.DeserializeObject<List<ServicioWeb.Marca>>(servicioA.ObtenerMarca());
                    for (int i = 0; i < ListaMarca.Count; i++)
                    {
                        LBMarca.Items.Add(ListaMarca[i].Nombre);
                        TBMarcaProducto.Items.Add(ListaMarca[i].Nombre);
                    }
                    break;
                case 2:
                    ListaCategoria = null;
                    LBCategoria.Items.Clear();
                    TBCategoriaProducto.Items.Clear();
                    ListaCategoria = JsonConvert.DeserializeObject<List<ServicioWeb.Categoria>>(servicioA.ObtenerCategoria());
                    for (int i = 0; i < ListaCategoria.Count; i++)
                    {
                        LBCategoria.Items.Add(ListaCategoria[i].Nombre);
                        TBCategoriaProducto.Items.Add(ListaCategoria[i].Nombre);
                    }
                    break;
                case 3:
                    ListaProducto = null;
                    LBProducto.Items.Clear();
                    ListaProducto = JsonConvert.DeserializeObject<List<ServicioWeb.Producto>>(servicioA.ComandoProductos(1));
                    for (int i = 0; i < ListaProducto.Count; i++)
                    {
                        LBProducto.Items.Add(ListaProducto[i].Nombre);
                    }
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
                            TBDescricionMarca.Text = null;
                            TBNombreMarca.Text = null;
                            PBMarca.Image = null;
                            URLMarcaImagen = null;
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

            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (LBProducto.SelectedItem != null)
                {
                    Dictionary<string, ServicioWeb.Producto> diccionario = ListaProducto.ToDictionary(m => m.Nombre);
                    if (servicioA.EliminarProducto(diccionario[LBProducto.SelectedItem.ToString()].Id))
                    {

                        MessageBox.Show("Bien");
                        TBNombreProducto.Text = null;
                        TBDescripcionProducto.Text = null;
                        TBStockProducto.Text = null;
                        TBEstadoProducto.Text = null;
                        TBMarcaProducto.Text = null;
                        TBCategoriaProducto.Text = null;
                        TBPrecioProducto.Text = null;
                        PBProducto.Image = null;
                        ActualizarListas(3);
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
        }

        private void BtnActualizarProducto_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (TBDescripcionProducto.Text != "" && TBNombreProducto.Text != "" && TBDescripcionProducto != null && TBStockProducto.Text != "" && TBEstadoProducto.Text != "" && TBMarcaProducto.Text != "" && TBCategoriaProducto.Text != "" && TBPrecioProducto.Text != "" && URLProductoImagen != null && PBProducto.Image != null)
                {
                    if (LBProducto.SelectedItem != null)
                    {
                        Dictionary<string, ServicioWeb.Marca> diccionario = ListaMarca.ToDictionary(m => m.Nombre);
                        Dictionary<string, ServicioWeb.Categoria> diccionarioC = ListaCategoria.ToDictionary(m => m.Nombre);
                        Dictionary<string, ServicioWeb.Producto> diccionarioP = ListaProducto.ToDictionary(m => m.Nombre);
                        if (URLProductoImagen == null) { URLProductoImagen = "imagen"; }
                        ServicioWeb.Producto productoA = new ServicioWeb.Producto()
                        {
                            Id = diccionarioP[LBProducto.SelectedItem.ToString()].Id,
                            Nombre = TBNombreProducto.Text,
                            Descripcion = TBDescripcionProducto.Text,
                            Stock = int.Parse(TBStockProducto.Text),
                            Estado = bool.Parse(TBEstadoProducto.Text),
                            Marca = diccionario[TBMarcaProducto.Text].Id.ToString(),
                            Categoria = diccionarioC[TBCategoriaProducto.Text].Id.ToString(),
                            Precio = decimal.Parse(TBPrecioProducto.Text),
                            ImagenPrincipal = URLProductoImagen,
                            FechaUltimaModificacion = DateTime.Now,
                            FechaDeCreacion = diccionarioP[LBProducto.SelectedItem.ToString()].FechaDeCreacion,
                        };
                        if (servicioA.ActualizarProducto(productoA))
                        {
                            MessageBox.Show("bien");
                            ActualizarListas(3);
                            TBNombreProducto.Text = null;
                            TBDescripcionProducto.Text = null;
                            TBStockProducto.Text = null;
                            TBEstadoProducto.Text = null;
                            TBMarcaProducto.Text = null;
                            TBCategoriaProducto.Text = null;
                            TBPrecioProducto.Text = null;
                            PBProducto.Image = null;
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

        private void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (TBDescripcionProducto.Text != "" && TBNombreProducto.Text != "" && TBDescripcionProducto != null && TBStockProducto.Text != "" && TBEstadoProducto.Text != "" && TBMarcaProducto.Text != "" && TBCategoriaProducto.Text != "" && TBPrecioProducto.Text != "" && URLProductoImagen != null && PBProducto.Image != null)
                {
                    Dictionary<string, ServicioWeb.Marca> diccionario = ListaMarca.ToDictionary(m => m.Nombre);
                    Dictionary<string, ServicioWeb.Categoria> diccionarioC = ListaCategoria.ToDictionary(m => m.Nombre);
                    ServicioWebTienda.ServicioA servicio = new ServicioWebTienda.ServicioA();
                    ServicioWebTienda.Producto productoA = new ServicioWebTienda.Producto()
                    {
                        Nombre = TBNombreProducto.Text,
                        Descripcion = TBDescripcionProducto.Text,
                        Stock = int.Parse(TBStockProducto.Text),
                        Estado = bool.Parse(TBEstadoProducto.Text),
                        Marca = diccionario[TBMarcaProducto.Text].Id.ToString(),
                        Categoria = diccionarioC[TBCategoriaProducto.Text].Id.ToString(),
                        Precio = decimal.Parse(TBPrecioProducto.Text),
                        ImagenPrincipal = URLProductoImagen,
                        FechaUltimaModificacion = DateTime.Now,
                        FechaDeCreacion = DateTime.Now,
                    };
                    if (servicio.CrearProducto(productoA))
                    {
                        MessageBox.Show("Bien");
                        TBNombreProducto.Text = null;
                        TBDescripcionProducto.Text = null;
                        TBStockProducto.Text = null;
                        TBEstadoProducto.Text = null;
                        TBMarcaProducto.Text = null;
                        TBCategoriaProducto.Text = null;
                        TBPrecioProducto.Text = null;
                        PBProducto.Image = null;
                        ActualizarListas(3); 
                    }
                    else
                    {
                        MessageBox.Show("Mal");
                    }

                }
                else
                {
                    MessageBox.Show("Todos lo campos deben de estar llenos");
                }
            }
        }

        private void BtnAgregarImagenProducto_Click(object sender, EventArgs e)
        {
            if (TBNombreProducto.Text != "")
            {
                ObtenerUrlImagenProducto();
            }
            else
            {
                MessageBox.Show("Primero debe asignar un nombre al producto");
            }
        }

        private async Task<string> ObtenerUrlImagenProducto()
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
                PBProducto.Image = Image.FromFile(rutaImagen);

                // Si la imagen no es de tipo PNG, se convierte a ese formato
                if (Path.GetExtension(rutaImagen).ToLower() != ".png")
                {
                    Bitmap imagen = new Bitmap(rutaImagen);
                    MemoryStream ms = new MemoryStream();
                    imagen.Save(ms, ImageFormat.Png);
                    ms.Seek(0, SeekOrigin.Begin);
                    rutaImagen = Path.ChangeExtension(rutaImagen, ".png");
                    File.WriteAllBytes(rutaImagen, ms.ToArray());
                }

                byte[] archivoB = File.ReadAllBytes(rutaImagen);
                var x = await servicioA.SubirImagenAsync(5, archivoB, TBNombreProducto.Text + "Imagen");
                url = x.Body.SubirImagenResult;
                URLProductoImagen = url;
                return url;
            }
            return url;

        }

        private void LBCategoria_DoubleClick(object sender, EventArgs e)
        {
            if (LBCategoria.SelectedItem != null)
            {
                TBDescricionCategoria.Text = null;
                TBNombreCategoria.Text = null;
                Dictionary<string, ServicioWeb.Categoria> diccionario = ListaCategoria.ToDictionary(m => m.Nombre);

                TBNombreCategoria.Text = diccionario[LBCategoria.SelectedItem.ToString()].Nombre;
                TBDescricionCategoria.Text = diccionario[LBCategoria.SelectedItem.ToString()].Descripcion;

            }
        }

        private void BtnAgregarCategoria_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (TBDescricionCategoria.Text != "" && TBNombreCategoria.Text != "")
                {
                    ServicioWeb.Categoria categoriaA = new ServicioWeb.Categoria()
                    {
                        Nombre = TBNombreCategoria.Text,
                        Descripcion = TBDescricionCategoria.Text,
                    };
                    if (servicioA.AgregarCategoria(categoriaA))
                    {
                        MessageBox.Show("Bien");
                    }
                    else
                    {
                        MessageBox.Show("Mal");
                    }
                    ActualizarListas(2);
                    TBDescricionCategoria.Text = null;
                    TBNombreCategoria.Text = null;
                }
                else
                {
                    MessageBox.Show("Todos lo campos deben de estar llenos");
                }
            }
        }

        private void BtnActualizarCategoria_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (TBDescricionCategoria.Text != "" && TBNombreCategoria.Text != "")
                {
                    if (LBCategoria.SelectedItem != null)
                    {
                        Dictionary<string, ServicioWeb.Categoria> diccionario = ListaCategoria.ToDictionary(m => m.Nombre);
                        ServicioWeb.Categoria categoriaA = new ServicioWeb.Categoria()
                        {
                            Id = diccionario[LBCategoria.SelectedItem.ToString()].Id,
                            Nombre = TBNombreCategoria.Text,
                            Descripcion = TBDescricionCategoria.Text,
                        };
                        if (servicioA.ActualizarCategoria(categoriaA))
                        {
                            MessageBox.Show("bien");
                            ActualizarListas(2);
                            TBDescricionCategoria.Text = null;
                            TBNombreCategoria.Text = null;
                        }
                        else
                        {
                            MessageBox.Show("Mal");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar una categoria");
                    }
                }
                else
                {
                    MessageBox.Show("Todos lo campos deben de estar llenos");
                }
            }
        }

        private void BtnEliminarCategoria_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que quieres realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (LBCategoria.SelectedItem != null)
                {
                    Dictionary<string, ServicioWeb.Categoria> diccionario = ListaCategoria.ToDictionary(m => m.Nombre);
                    if (servicioA.EliminarCategoria(diccionario[LBCategoria.SelectedItem.ToString()].Id))
                    {

                        TBDescricionCategoria.Text = null;
                        TBNombreCategoria.Text = null;
                        ActualizarListas(2);
                    }
                    else
                    {
                        MessageBox.Show("La categoria no puede ser eliminada, verifique si esta asociada algun producto o marca");
                    }

                }
                else
                {
                    MessageBox.Show("Debe seleccionar una categoria");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TBDescricionCategoria.Text = null;
            TBNombreCategoria.Text = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TBNombreProducto.Text = null;
            TBDescripcionProducto.Text = null;
            TBStockProducto.Text = null;
            TBEstadoProducto.Text = null;
            TBMarcaProducto.Text = null;
            TBCategoriaProducto.Text = null;
            TBPrecioProducto.Text = null;
            PBProducto.Image = null;
        }

        private void LBProducto_DoubleClick(object sender, EventArgs e)
        {
            if (LBProducto.SelectedItem != null)
            {
                TBNombreProducto.Text = null;
                TBDescripcionProducto.Text = null;
                TBStockProducto.Text = null;
                TBEstadoProducto.Text = null;
                TBMarcaProducto.Text = null;
                TBCategoriaProducto.Text = null;
                TBPrecioProducto.Text = null;
                PBProducto.Image = null;
                Dictionary<string, ServicioWeb.Producto> diccionario = ListaProducto.ToDictionary(m => m.Nombre);
                try
                {
                    WebClient wc = new WebClient();
                    byte[] bytes = wc.DownloadData(diccionario[LBProducto.SelectedItem.ToString()].ImagenPrincipal);
                    MemoryStream ms = new MemoryStream(bytes);
                    URLProductoImagen = diccionario[LBProducto.SelectedItem.ToString()].ImagenPrincipal;
                    Image imagen = Image.FromStream(ms);
                    PBProducto.Image = imagen;

                }
                catch (Exception)
                {
                }
                TBNombreProducto.Text = diccionario[LBProducto.SelectedItem.ToString()].Nombre;
                TBDescripcionProducto.Text = diccionario[LBProducto.SelectedItem.ToString()].Descripcion;
                TBStockProducto.Text = diccionario[LBProducto.SelectedItem.ToString()].Stock.ToString();
                TBEstadoProducto.Text = diccionario[LBProducto.SelectedItem.ToString()].Estado.ToString(); ;
                TBMarcaProducto.Text = diccionario[LBProducto.SelectedItem.ToString()].Marca;
                TBCategoriaProducto.Text = diccionario[LBProducto.SelectedItem.ToString()].Categoria;
                TBPrecioProducto.Text = diccionario[LBProducto.SelectedItem.ToString()].Precio.ToString();

            }
        }
    }
}
