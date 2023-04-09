using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADCT
{
    internal class ProductoMetodo
    {
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
        public ProductoMetodo()
        {
            string connectionString = "Data Source=softwaretienda.database.windows.net;Initial Catalog=Tienda;Persist Security Info=True;User ID=AdminSql;Password=Software1234";
            connection = new SqlConnection(connectionString);

        }
        private SqlConnection connection;
        private void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }
        private void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        private string Encriptar(string X)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(X);
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
        private string Desencriptar(string X)
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

        public string ComandoProductos(int Orden)
        {
            int i = 0;
            List<Producto> ListaProductos = new List<Producto>();

            switch (Orden)
            {
                case 1:
                    //Obtiene todos los productos sin ningun orden
                    ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY p.Id;");
                    break;
                case 2:
                    //Obtiene todos los productos con los precios de manera ascendente
                    ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY p.Precio ASC;");
                    break;
                case 3:
                    //Obtiene todos los productos con los precios de manera descendente
                    ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY p.Precio DESC;");
                    break;
                case 4:
                    //Obtiene todos los productos con las calificaciones de manera ascendente
                    ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY Calificacion ASC;");
                    break;
                case 5:
                    //Obtiene todos los productos con las calificaciones de manera descendente
                    ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY Calificacion DESC;");
                    break;
                case 6:
                    //Obtiene todos los productos con una relacion calidad precio ascendente
                    ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY  p.Precio/AVG(ca.Puntuacion) ASC;");
                    break;
                case 7:
                    //Obtiene todos los productos con una relacion calidad precio ascendente
                    ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY  p.Precio/AVG(ca.Puntuacion) DESC;");
                    break;
            }
            return JsonConvert.SerializeObject(ListaProductos);
        }
        private List<Producto> ObtenerProductos(string Comando)
        {
            OpenConnection();
            SqlCommand comando;
            SqlDataReader reader;
            comando = new SqlCommand(Comando, connection);
            comando.ExecuteNonQuery();
            reader = comando.ExecuteReader();
            List<Producto> ListaProductos = new List<Producto>();
            int i = 0;
            while (reader.Read())
            {
                ListaProductos.Add(new Producto());
                ListaProductos[i].Id = reader.GetInt32(0);
                ListaProductos[i].Nombre = reader.GetString(1);
                ListaProductos[i].Descripcion = reader.GetString(2);
                ListaProductos[i].Precio = reader.GetDecimal(3);
                ListaProductos[i].Stock = reader.GetInt32(4);
                ListaProductos[i].FechaDeCreacion = reader.GetDateTime(5);
                ListaProductos[i].FechaUltimaModificacion = reader.GetDateTime(6);
                ListaProductos[i].Estado = reader.GetBoolean(7);
                ListaProductos[i].ImagenPrincipal = reader.GetString(8);
                ListaProductos[i].Categoria = reader.GetString(9);
                ListaProductos[i].Marca = reader.GetString(10);
                try
                {
                    ListaProductos[i].calificacion = reader.GetDouble(11);
                }
                catch (Exception)
                {

                    ListaProductos[i].calificacion = 0;
                }

                i++;
            }
            reader.Close();
            for (int A = 0; A < ListaProductos.Count; A++)
            {
                ListaProductos[A].Imágenes = ImagenesProducto(ListaProductos[A].Id);
            }
            CloseConnection();
            return ListaProductos;
        }
        private List<Imagenes> ImagenesProducto(int id)
        {
            OpenConnection();
            List<Imagenes> ListaImagenes = new List<Imagenes>();
            SqlCommand comando;
            SqlDataReader reader;
            comando = new SqlCommand("select Nombre,URL from ImagenesProductos where IdProducto = " + id, connection);
            reader = comando.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                ListaImagenes.Add(new Imagenes());
                ListaImagenes[i].Nombre = reader.GetString(0);
                ListaImagenes[i].URL = reader.GetString(1);
                i++;
            }
            CloseConnection();
            return ListaImagenes;
        }
    }
}
