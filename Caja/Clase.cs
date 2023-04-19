using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja
{
    public class Datos
    {
        public static Dictionary<int,string> DiccionarioCategoria = new Dictionary<int,string>();
        public static List<string> Listadoproductos = new List<string>();
        public static List<Producto> ListaCarrito = new List<Producto>();
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroDeTelefono { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string ImagenUsuario { get; set; }
        public string Direccion { get; set; }
    }
    public class Factura
    {
        public int NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }
        public List<Producto> Productos { get; set; }
        public decimal Total { get { return Productos.Sum(p => p.Precio); } }
    }

    public class Cliente
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }

    public class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }

    public class ProductoOBJ
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public bool Estado { get; set; }
        public string ImagenPrincipal { get; set; }
        public int IdMarca { get; set; }
    }


}
