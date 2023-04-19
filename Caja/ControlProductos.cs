using Caja.DataSetTableAdapters;
using Newtonsoft.Json;
using ServicioWebTienda;
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
    public partial class ControlProductos : UserControl
    {
        public ControlProductos()
        {
            InitializeComponent();
      
      
        }
        private void ControlProductos_Load(object sender, EventArgs e)
        {

            CargarProductos();
            CargarCategoria();
        }

        private void CargarCategoria()
        {

            CategoriaTableAdapter categoriaTableAdapter = new CategoriaTableAdapter();
            DataTable dt = categoriaTableAdapter.GetData();
            Datos.DiccionarioCategoria.Clear();
            foreach (DataRow fila in dt.Rows)
            {
                ButtonCategoriaControl button = new ButtonCategoriaControl(
                    int.Parse(fila["Id"].ToString()),
                    fila["Nombre"].ToString()
                );

                Datos.DiccionarioCategoria.Add(int.Parse(fila["Id"].ToString()), fila["Nombre"].ToString());

                button.CategoriaSeleccionada += ButtonCategoriaControl_CategoriaSeleccionada;

                flowLayoutPanel2.Controls.Add(button);
            }
        }

        private void ButtonCategoriaControl_CategoriaSeleccionada(object sender, int e)
        {
            int idCategoria = e;
            CargarProductos(idCategoria);
            // Hacer algo con el ID de la categoría seleccionada
        }

        private void CargarProductos(int idCategoria = 0)
        {
            flowLayoutPanel1.Controls.Clear();
            ProductoTableAdapter productoTableAdapter = new ProductoTableAdapter();
            DataTable dt = productoTableAdapter.GetDataByCategoria(idCategoria);

            foreach (DataRow fila in dt.Rows)
            {
                ProductoOBJ producto = new ProductoOBJ();
                producto.Id = Convert.ToInt32(fila["Id"]);
                producto.Nombre = fila["Nombre"].ToString();
                producto.Descripcion = fila["Descripcion"].ToString();
                producto.IdCategoria = Convert.ToInt32(fila["IdCategoria"]);
                producto.Precio = Convert.ToDecimal(fila["Precio"]);
                producto.Stock = Convert.ToInt32(fila["Stock"]);
                producto.FechaDeCreacion = Convert.ToDateTime(fila["FechaDeCreacion"]);
                producto.FechaUltimaModificacion = Convert.ToDateTime(fila["FechaUltimaModificacion"]);
                producto.Estado = Convert.ToBoolean(fila["Estado"]);
                producto.ImagenPrincipal = fila["ImagenPrincipal"].ToString();
                producto.IdMarca = Convert.ToInt32(fila["IdMarca"]);

                ControlObjProducto Cproducto = new ControlObjProducto(producto);
                flowLayoutPanel1.Controls.Add(Cproducto);
                Cproducto.ProductoSeleccionado += controlObjProducto_ProductoSeleccionado;
            }
        }

        private void controlObjProducto_ProductoSeleccionado(object sender, ProductoOBJ e)
        {
            OnProductoSeleccionadoOBJ(e);
        }
        public event EventHandler<ProductoOBJ> ProductoSeleccionadoOBJ;
        private void OnProductoSeleccionadoOBJ(ProductoOBJ e)
        {
            ProductoSeleccionadoOBJ?.Invoke(this, e);
        }
        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            CargarProductos();
        }
    }
}
