using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Security.Policy;
using Firebase.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioWebTienda
{
    /// <summary>
    /// Descripción breve de ServicioProducto
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioProducto : System.Web.Services.WebService
    {
        ConexionBD conexionBD = new ConexionBD();
        //Producto
        [WebMethod]
        public bool CrearProducto(Producto producto, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                if (ProductoExiste(producto.Nombre))
                {
                    return false;
                }
                conexionBD.OpenConnection();
                SqlCommand command = new SqlCommand("CrearProducto", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@IdCategoria", int.Parse(producto.Categoria));
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@Stock", producto.Stock);
                command.Parameters.AddWithValue("@FechaDeCreacion", producto.FechaDeCreacion);
                command.Parameters.AddWithValue("@FechaUltimaModificacion", producto.FechaUltimaModificacion);
                command.Parameters.AddWithValue("@Estado", producto.Estado);
                command.Parameters.AddWithValue("@ImagenPrincipal", producto.ImagenPrincipal);
                command.Parameters.AddWithValue("@IdMarca", int.Parse(producto.Marca));
                command.ExecuteNonQuery();
                conexionBD.CloseConnection();

                LOG lOG = new LOG();
                lOG.Resultado = true.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return true;
            }
            catch (Exception ex)
            {

                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return false;
            }

        }
        [WebMethod]
        public List<Imagenes> ImagenesProducto(int id, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();
                List<Imagenes> ListaImagenes = new List<Imagenes>();
                SqlCommand comando;
                SqlDataReader reader;
                comando = new SqlCommand("select Nombre,URL from ImagenesProductos where IdProducto = " + id, conexionBD.connection);
                reader = comando.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    ListaImagenes.Add(new Imagenes());
                    ListaImagenes[i].Nombre = reader.GetString(0);
                    ListaImagenes[i].URL = conexionBD.Desencriptar(reader.GetString(1));
                    i++;
                }
         
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = JsonConvert.SerializeObject(ListaImagenes);
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return ListaImagenes;
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return null;
            }
           
        }
        [WebMethod]
        public bool ProductoExiste(string nombre, string Remitente = "Anonimo", int Origen = 1)
        {
            conexionBD.OpenConnection();
            try
            {
                bool ProductoExiste = false;
                string consulta = "SELECT COUNT(*) FROM Producto WHERE Nombre = @Nombre";
                SqlCommand Command = new SqlCommand(consulta, conexionBD.connection);
                Command.Parameters.AddWithValue("@Nombre", nombre);

                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    ProductoExiste = true;
                }
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = ProductoExiste.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return ProductoExiste;
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return false;
            }
        }
        [WebMethod]
        public string ComandoProductos(int Orden, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                int i = 0;
                List<Producto> ListaProductos = new List<Producto>();

                switch (Orden)
                {
                    case 1:
                        //Obtiene todos los productos sin ningun orden
                        ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY p.Id;", Remitente, Origen);
                        break;
                    case 2:
                        //Obtiene todos los productos con los precios de manera ascendente
                        ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY p.Precio ASC;", Remitente, Origen);
                        break;
                    case 3:
                        //Obtiene todos los productos con los precios de manera descendente
                        ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY p.Precio DESC;", Remitente, Origen);
                        break;
                    case 4:
                        //Obtiene todos los productos con las calificaciones de manera ascendente
                        ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY Calificacion ASC;", Remitente, Origen);
                        break;
                    case 5:
                        //Obtiene todos los productos con las calificaciones de manera descendente
                        ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY Calificacion DESC;", Remitente, Origen);
                        break;
                    case 6:
                        //Obtiene todos los productos con una relacion calidad precio ascendente
                        ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY  p.Precio/AVG(ca.Puntuacion) ASC;", Remitente, Origen);
                        break;
                    case 7:
                        //Obtiene todos los productos con una relacion calidad precio ascendente
                        ListaProductos = ObtenerProductos("SELECT p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre AS Categoria, m.Nombre AS Marca, AVG(ca.Puntuacion) AS Calificacion\r\nFROM Producto p\r\nINNER JOIN Categoria c ON p.IdCategoria = c.Id\r\nINNER JOIN Marca m ON p.IdMarca = m.Id\r\nLEFT JOIN Calificacion ca ON p.Id = ca.IdProducto \r\nGROUP BY p.Id, p.Nombre, p.Descripcion, p.Precio, p.Stock, p.FechaDeCreacion, p.FechaUltimaModificacion, p.Estado, p.ImagenPrincipal, c.Nombre, m.Nombre\r\nORDER BY  p.Precio/AVG(ca.Puntuacion) DESC;", Remitente, Origen);
                        break;
                }
                LOG lOG = new LOG();
                lOG.Resultado = JsonConvert.SerializeObject(ListaProductos);
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return JsonConvert.SerializeObject(ListaProductos);
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                return null;
            }
        }

        [WebMethod]
        private List<Producto> ObtenerProductos(string Comando, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();
                SqlCommand comando;
                SqlDataReader reader;
                comando = new SqlCommand(Comando, conexionBD.connection);
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
                    ListaProductos[i].ImagenPrincipal = conexionBD.Desencriptar(reader.GetString(8));
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
                    ListaProductos[A].Imágenes = ImagenesProducto(ListaProductos[A].Id,Remitente,Origen);
                }
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = JsonConvert.SerializeObject(ListaProductos);
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return ListaProductos;
            }
            catch(Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return null;
            }
            
        }
        [WebMethod]
        public bool ActualizarProducto(Producto producto, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = command = new SqlCommand("ActualizarProducto", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", producto.Id);
                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@IdCategoria", int.Parse(producto.Categoria));
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@Stock", producto.Stock);
                command.Parameters.AddWithValue("@FechaUltimaModificacion", producto.FechaUltimaModificacion);
                command.Parameters.AddWithValue("@Estado", producto.Estado);
                command.Parameters.AddWithValue("@ImagenPrincipal", producto.ImagenPrincipal);
                command.Parameters.AddWithValue("@IdMarca", int.Parse(producto.Marca));

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    conexionBD.CloseConnection();
                    LOG lOG = new LOG();
                    lOG.Resultado = true.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return true;
                }
                else
                {
                    conexionBD.CloseConnection();
                    LOG lOG = new LOG();
                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return false;
                }

            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return false;
                throw;
            }
        }
        [WebMethod]
        public bool EliminarProducto(int Id, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = new SqlCommand("EliminarProducto", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", Id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    conexionBD.CloseConnection();
                    LOG lOG = new LOG();
                    lOG.Resultado = true.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return true;
                }
                else
                {
                    conexionBD.CloseConnection();
                    LOG lOG = new LOG();
                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return false;
                }

            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return false;
            }
        }
        [WebMethod]
        private bool ExisteNombreImagen(string nombre, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();
                string sql = "SELECT COUNT(*) FROM ImagenesProductos WHERE Nombre = @NombreImagen";
                using (SqlCommand command = new SqlCommand(sql, conexionBD.connection))
                {
                    command.Parameters.AddWithValue("@NombreImagen", nombre);
                    int count = (int)command.ExecuteScalar();
                    LOG lOG = new LOG();
                    lOG.Resultado = (count > 0).ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                return false;
            }
            
        }

        //Revisarlo arduamente OJO
        [WebMethod]
        public bool AgregarImagenProducto(Imagenes imagenes, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                string NImagenes = imagenes.Nombre + "Imagen";

                int i = 1;
                string x = NImagenes;
                while (ExisteNombreImagen(NImagenes))
                {
                    //actual
                    NImagenes = x + i;
                    i++;
                }
                imagenes.Nombre = NImagenes;
                try
                {
                    conexionBD.OpenConnection();
                    SqlCommand command = new SqlCommand("InsertarImagenProducto", conexionBD.connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros de entrada
                    command.Parameters.AddWithValue("@IdProducto", imagenes.IDP);
                    command.Parameters.AddWithValue("@Nombre", imagenes.Nombre);
                    command.Parameters.AddWithValue("@URL", imagenes.URL);
                    command.Parameters.AddWithValue("@FechaDeCreacion", imagenes.FechaDeAsignacion);

                    // Agregar parámetro de salida
                    SqlParameter idGeneradoParam = new SqlParameter("@IdGenerado", SqlDbType.Int);
                    idGeneradoParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(idGeneradoParam);

                    // Ejecutar el stored procedure
                    command.ExecuteNonQuery();

                    // Recuperar el valor generado automáticamente para la columna "Id"
                    int idGenerado = (int)idGeneradoParam.Value;

                    LOG lOG = new LOG();
                    lOG.Resultado = true.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return true;
                }
                catch (Exception ex)
                {
                    LOG lOG = new LOG();
                    lOG.Resultado = ex.Message;
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 2;
                    return false;
                  
                }
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                return false;
            }

        }
        [WebMethod]
        public string ObtenerImagenesProducto(int ID, string Remitente = "Anonimo", int Origen = 1) 
        {
            try
            {
                List<Imagenes> Limagenes = new List<Imagenes>();
                conexionBD.OpenConnection();
                SqlCommand command = new SqlCommand("Select * from ImagenesProductos where IdProducto = @ID", conexionBD.connection);

                command.Parameters.AddWithValue("@ID", ID);
                SqlDataReader reader = command.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    Imagenes imagenes = new Imagenes();
                    imagenes.ID = reader.GetInt32(0);
                    imagenes.IDP = reader.GetInt32(1);
                    imagenes.Nombre = reader.GetString(2);
                    imagenes.URL = conexionBD.Desencriptar(reader.GetString(3));
                    imagenes.FechaDeAsignacion = reader.GetDateTime(4);
                    Limagenes.Add(imagenes);
                }
                LOG lOG = new LOG();
                lOG.Resultado = JsonConvert.SerializeObject(Limagenes);
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return JsonConvert.SerializeObject(Limagenes); ;
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                return null;
            }
        }
        [WebMethod]
        public async Task<bool> EliminarImagenP(string NombreImagen, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();
                try
                {
                    var firebaseStorage = new FirebaseStorage("tiendabd-204c4.appspot.com");
                    await firebaseStorage.Child("FotosProducto").Child(NombreImagen + "Imagen").DeleteAsync();
                }
                catch (Exception)
                {

                }
                SqlCommand command = new SqlCommand("PPEliminarImagenPorNombre", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@NombreImagen", NombreImagen);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    conexionBD.CloseConnection();
                    LOG lOG = new LOG();
                    lOG.Resultado = true.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return true;
                }
                else
                {
                    conexionBD.CloseConnection();
                    LOG lOG = new LOG();
                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return false;
                }

            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return false;
            }
        }

    }
}
