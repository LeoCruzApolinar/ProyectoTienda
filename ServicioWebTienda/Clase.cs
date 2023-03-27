using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWebTienda
{
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
}

