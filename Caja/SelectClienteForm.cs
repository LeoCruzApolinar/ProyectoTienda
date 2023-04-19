using Caja.DataSetTableAdapters;
using ServicioWebTienda;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caja
{
    public partial class SelectClienteForm : Form
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
        public SelectClienteForm()
        {
            InitializeComponent();
        }
        List<Usuario> usuarios = new List<Usuario>();
        private void SelectClienteForm_Load(object sender, EventArgs e)
        {
            UsuarioTableAdapter usuarioTableAdapter = new UsuarioTableAdapter();
            DataTable dt = usuarioTableAdapter.GetData();

            foreach (DataRow fila in dt.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.Id = (int)fila["Id"];
                usuario.Nombre = fila["Nombre"].ToString();
                usuario.Apellido = fila["Apellido"].ToString();
                usuario.NombreUsuario = fila["Usuario"].ToString();
                usuario.CorreoElectronico = Desencriptar(fila["CorreoElectronico"].ToString());
                usuario.Contraseña = fila["Contraseña"].ToString();
                usuario.NumeroDeTelefono = fila["NumeroDeTelefono"].ToString();
                usuario.FechaNacimiento = (DateTime)fila["FechaNacimiento"];
                usuario.Direccion = fila["Direccion"].ToString();
                usuario.Estado = (bool)fila["Estado"];
                usuario.ImagenUsuario = fila["ImagenUsuario"].ToString();
                comboBox1.Items.Add(Desencriptar(usuario.NombreUsuario));
                usuarios.Add(usuario);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el usuario seleccionado
            Usuario usuarioSeleccionado = usuarios[comboBox1.SelectedIndex];
            label8.Text = usuarioSeleccionado.FechaNacimiento.ToShortDateString();
            label1.Text = usuarioSeleccionado.Nombre;
            // Cargar los datos del usuario en los controles del formulario
            label3.Text = usuarioSeleccionado.Apellido;
            label6.Text = usuarioSeleccionado.CorreoElectronico;
            panel3.BackColor = usuarioSeleccionado.FechaNacimiento.Date == DateTime.Now.Date ? Color.Green : Color.Black;
        }
    }
}
