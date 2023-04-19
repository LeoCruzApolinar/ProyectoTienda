using System;
using System.Collections;
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
            GestorBaseDeDatos gestorBaseDeDatos = new GestorBaseDeDatos();
            gestorBaseDeDatos.LlamarCloner();
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

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ControlProductos controlProductos = new ControlProductos();
            panel2.Controls.Add(controlProductos);
            controlProductos.ProductoSeleccionadoOBJ += controlObjProducto_ProductoSeleccionadoOBJ;
        }

        private void controlObjProducto_ProductoSeleccionadoOBJ(object sender, ProductoOBJ e)
        {
            if (!Datos.Listadoproductos.Contains(e.Nombre))
            {
                Producto producto = new Producto();
                producto.Cantidad = e.Stock;
                producto.Nombre = e.Nombre;
                producto.Precio = e.Precio;
                ListaProductoControl listaProductoControl = new ListaProductoControl(producto);
                flowLayoutPanel1.Controls.Add(listaProductoControl);
                Datos.Listadoproductos.Add(producto.Nombre);
                listaProductoControl.EliminarProductoDelFlowlayout += EliminarProductoDelFlowlayout;

            }
            else
            {
                MessageBox.Show("Ya este producto esta en la lista");
            }
   
        }

        private void EliminarProductoDelFlowlayout(object sender, Producto e)
        {
            ListaProductoControl listaProductoControl = sender as ListaProductoControl;
            if (listaProductoControl != null)
            {
                flowLayoutPanel1.Controls.Remove(listaProductoControl);
                Datos.Listadoproductos.Remove(e.Nombre);
                Datos.ListaCarrito.Remove(e);
            }
        }

        private void BtnBorrarFactura_Click(object sender, EventArgs e)
        {
            Datos.ListaCarrito.Clear();
            flowLayoutPanel1.Controls.Clear();
            Datos.Listadoproductos.Clear();
        }
        private SelectClienteForm selectClienteForm;
        private void button2_Click(object sender, EventArgs e)
        {
            if (selectClienteForm == null)
            {
                selectClienteForm = new SelectClienteForm();
                selectClienteForm.FormClosed += selectClienteForm_FormClosed;
                selectClienteForm.Show();
            }
        }
        private void selectClienteForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            selectClienteForm = null;
        }
    }
}
