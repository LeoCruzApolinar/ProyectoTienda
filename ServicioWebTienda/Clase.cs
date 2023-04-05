using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWebTienda
{
    public class LoginU 
    {
        public bool Estado { get; set; }
        public string Token { get; set; }
        public string User { get; set; }
        public string UID { get; set; }
    }
   public class Usuario
    { 
        public string usuario { get; set; }
        public string CorreoElectronico { get;set; }
        public string Clave { get; set;}
        public string Nombre { get; set;}
        public string Apellido { get; set;}
        public string Telefono { get; set;}
        public int Estado { get; set;}
        public DateTime FechaNacimiento { get; set;}
        public string ImagenUsuario { get; set;}   
        public string Direccion { get; set;}    
    }
    //Producto
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public List<Imagenes> Imágenes { get; set; }
        public int Stock { get; set; }
        public double calificacion { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public bool Estado { get; set; }
        public string Marca { get; set; }
        public string ImagenPrincipal { get; set; }
    }
    public class Imagenes
    {
        public string Nombre { get; set; }
        public string URL { get; set; }
        public DateTime FechaDeAsignacion { get; set; }
    }
    public class ProdcutoPedidos
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public List<Imagenes> Imágenes { get; set; }
    }
    public class Pedidos
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEntrega { get; set; }
        public List<ProdcutoPedidos> listaProdcutos { get; set; }
        public double Cantidad { get; set; }
        public double PrecioTotal { get; set; }
    }
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Logo { get; set; }
    }
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }  
        public string Descripcion { get; set;}
    }
    public class Empleado
    {
        public int Id { get; set; } 
        public string Usuario { get; set;}
        public string CorreoElectronico { get; set;}
        public string Clave { get; set;}
        public string Nombre { get; set;}
        public string Apellido { get; set;}
        public string Telefono { get; set;}
        public string Estado { get; set;}
        public DateTime FechaNacimiento { get; set;}
        public string ImagenEmpleado { get; set;}
        public string Cedula { get; set; }
        public int IdRol { get; set;}
    }
}

