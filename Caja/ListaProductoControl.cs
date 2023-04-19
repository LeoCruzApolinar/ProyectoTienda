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
    public partial class ListaProductoControl : UserControl
    {
        private Producto producto_OBJ;
        private int Maxiomo;
        private decimal Precio;
        public ListaProductoControl(Producto producto)
        {
            InitializeComponent();
            producto_OBJ = producto;
            label1.Text = producto_OBJ.Nombre;
            Maxiomo = producto_OBJ.Cantidad;
            Precio = producto_OBJ.Precio;

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            int valor;

            if (!int.TryParse(textBox1.Text, out valor)) // Verificar si el valor introducido es un número entero válido
            {
                e.Cancel = true; // Cancelar la operación
                MessageBox.Show("Debe introducir un número entero válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (valor < 1 || valor > Maxiomo) // Verificar si el valor está dentro del rango permitido
            {
                e.Cancel = true; // Cancelar la operación
                MessageBox.Show("Debe introducir un valor entre 1 y "+Maxiomo+" .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label2.Text = (valor*Precio).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Producto NuevoProducto = new Producto();
                NuevoProducto.Nombre = producto_OBJ.Nombre;
                NuevoProducto.Precio = decimal.Parse(label2.Text);
                NuevoProducto.Cantidad = int.Parse(textBox1.Text);

                // Verificar si el producto ya está en la lista
                if (Datos.ListaCarrito.Contains(NuevoProducto))
                {
                    // Buscar el producto en la lista
                    Producto ProductoExistente = Datos.ListaCarrito.Find(p => p.Equals(NuevoProducto));

                    // Verificar si el nuevo producto es diferente al existente
                    if (ProductoExistente.Nombre != NuevoProducto.Nombre ||
                        ProductoExistente.Precio != NuevoProducto.Precio ||
                        ProductoExistente.Cantidad != NuevoProducto.Cantidad)
                    {
                        // Actualizar el producto existente en la lista
                        ProductoExistente.Nombre = NuevoProducto.Nombre;
                        ProductoExistente.Precio = NuevoProducto.Precio;
                        ProductoExistente.Cantidad = NuevoProducto.Cantidad;

                        // Cambiar el color del botón a verde
                        button1.BackColor = Color.Green;
                    }
                }
                else
                {
                    // Agregar el nuevo producto a la lista
                    Datos.ListaCarrito.Add(NuevoProducto);

                    // Cambiar el color del botón a verde
                    button1.BackColor = Color.Green;
                }
            }
            else 
            {
                MessageBox.Show("Agregue una cantidad");
            }
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Producto productoAEliminar = producto_OBJ;
            Datos.ListaCarrito.Remove(productoAEliminar);
            OnEliminarProductoDelFlowlayout(producto_OBJ);
        }

        public event EventHandler<Producto> EliminarProductoDelFlowlayout;

        private void OnEliminarProductoDelFlowlayout(Producto X)
        {
            EliminarProductoDelFlowlayout?.Invoke(this, X);
        }
    }
}
