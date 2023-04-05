using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Storage;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Cloud.Storage.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace ServicioWebTienda
{
    /// <summary>
    /// Descripción breve de ServicioA
    /// </summary>
    [WebService(Namespace = "http://Intec/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioA : System.Web.Services.WebService
    {
        //Firebase
        static IFirebaseConfig Conexion = new FirebaseConfig
        {
            AuthSecret = "oYbtwGC8zihW0MqyztWw7o8i8Ryd9Tw6e0bSsIPJ",
            BasePath = "https://tiendabd-204c4-default-rtdb.firebaseio.com/"
        };
        static IFirebaseClient client = null;
        //Base de datos
        private SqlConnection connection;

        public ServicioA()
        {
            string connectionString = "Data Source=softwaretienda.database.windows.net;Initial Catalog=Tienda;Persist Security Info=True;User ID=AdminSql;Password=Software1234";
            connection = new SqlConnection(connectionString);

        }
        [WebMethod]
        private void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }
        [WebMethod]
        private void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        [WebMethod]
        private string Encriptar(string X) 
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(X);
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
        [WebMethod]
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

        //Metodos de Usuario
        [WebMethod]
        public bool EliminarUsuarioFirebase(string email, string contrasena) 
        {
            try
            {
                var config = new FirebaseAuthConfig
                {
                    ApiKey = "AIzaSyCSehKv8spKMtwND8-M9-fVt1lx919gGN0",
                    AuthDomain = "tiendabd-204c4.firebaseapp.com",
                    Providers = new FirebaseAuthProvider[]
    {
            new GoogleProvider().AddScopes("email"),
            new EmailProvider()
    }
                };

                var client = new FirebaseAuthClient(config);
                var userCredential = client.SignInWithEmailAndPasswordAsync(email, contrasena);
                userCredential.Result.User.DeleteAsync().Wait();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }
        [WebMethod]
        public  string IniciarSesionFirebase(string email, string contrasena)
        {
            try
            {
                LoginU loginU = new LoginU();
                var config = new FirebaseAuthConfig
                {
                    ApiKey = "AIzaSyCSehKv8spKMtwND8-M9-fVt1lx919gGN0",
                    AuthDomain = "tiendabd-204c4.firebaseapp.com",
                    Providers = new FirebaseAuthProvider[]
                    {
            new GoogleProvider().AddScopes("email"),
            new EmailProvider()
                    }
                };

                var client = new FirebaseAuthClient(config);
                var userCredential = client.SignInWithEmailAndPasswordAsync(email, contrasena);
                try
                {
                    if (userCredential.Result == null)
                    {
                        loginU.Estado = false;
                        loginU.Token = null;
                    }
                    else
                    {
                        OpenConnection();
                        SqlCommand command = new SqlCommand("select Usuario from [dbo].[Usuario] where CorreoElectronico = '" + Encriptar(email) + "';", connection);
                    
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            loginU.User =Desencriptar(reader.GetString(0));
                        }
                        reader.Close();
                        CloseConnection();
                        loginU.Estado = true;
                        loginU.Token = userCredential.Result.User.Credential.IdToken;
                        loginU.UID = userCredential.Result.User.Uid;
                    }
                    string OBJ = JsonConvert.SerializeObject(loginU);
                    return OBJ;
                }
                catch (Exception)
                {
                    loginU.Estado = false;
                    loginU.Token = null;
                    string OBJ = JsonConvert.SerializeObject(loginU);
                    return OBJ;

                }
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }
        [WebMethod]
        private  void CrearCuentaFirebase(string Email, string contrasena)
        {
            try
            {
                var config = new FirebaseAuthConfig
                {
                    ApiKey = "AIzaSyCSehKv8spKMtwND8-M9-fVt1lx919gGN0",
                    AuthDomain = "tiendabd-204c4.firebaseapp.com",
                    Providers = new FirebaseAuthProvider[]
                {
                 // Add and configure individual providers
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
    // ...
                },
                    // WPF:
                    UserRepository = new FileUserRepository("FirebaseSample"), // persist data into %AppData%\FirebaseSample
                                                                               // UWP:
                };
                var client = new FirebaseAuthClient(config);
                var userCredential = client.CreateUserWithEmailAndPasswordAsync(Email, contrasena);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [WebMethod]
        private bool VerificarExistenciaUsuario(string usuario,string CorreoElectronico)
        {
            // Abre la conexión antes de ejecutar una consulta
            OpenConnection();
            try
            {
                bool UsuarioExistente = false;
                string consulta = "SELECT COUNT(*) FROM Usuario WHERE Usuario = @Usuario or CorreoElectronico = @CorreoElectronico";
                SqlCommand Command = new SqlCommand(consulta, connection);
                Command.Parameters.AddWithValue("@Usuario", Encriptar(usuario));
                Command.Parameters.AddWithValue("@CorreoElectronico", Encriptar(CorreoElectronico));


                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    UsuarioExistente = true;
                }
                CloseConnection();
                return UsuarioExistente;
            }
            catch (Exception)
            {
                CloseConnection();
                return false;
            }
           
        }
        [WebMethod]
        public bool IngresarUsuario(Usuario user)
        {
            if (!VerificarExistenciaUsuario(user.usuario,user.CorreoElectronico))
            {
                OpenConnection();
                try
                {
                    

                    SqlCommand command = new SqlCommand("InsertarUsuario", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@Usuario", Encriptar(user.usuario));
                    command.Parameters.AddWithValue("@CorreoElectronico", Encriptar(user.CorreoElectronico));
                    command.Parameters.AddWithValue("@Clave", Encriptar(user.Clave));
                    command.Parameters.AddWithValue("@Nombre", user.Nombre);
                    command.Parameters.AddWithValue("@Apellido", user.Apellido);
                    command.Parameters.AddWithValue("@NumeroDeTelefono", user.Telefono);
                    command.Parameters.AddWithValue("@Estado", user.Estado);
                    command.Parameters.AddWithValue("@FechaNacimiento", user.FechaNacimiento);
                    command.Parameters.AddWithValue("@ImagenUsuario", user.ImagenUsuario);
                    command.Parameters.AddWithValue("@Direccion", Encriptar(user.Direccion));
                    CrearCuentaFirebase(user.CorreoElectronico, user.Clave);
                    command.ExecuteNonQuery();
                    CloseConnection();
                    return true;
                }
                catch (Exception ex)
                {
                    CloseConnection();
                    return false;

                }
            }
            else
            {
                return false;
            }

        }
        [WebMethod]
        public string ObtenerUsuario(string Usuario)
        {
            OpenConnection();
            try
            {
                Usuario usuario = new Usuario();

               
                SqlCommand command = new SqlCommand("select * from [dbo].[Usuario] where Usuario = '" + Encriptar(Usuario) + "';", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usuario.usuario = Desencriptar(reader.GetString(1));
                    usuario.CorreoElectronico = Desencriptar(reader.GetString(2));
                    usuario.Clave = Desencriptar(reader.GetString(3));
                    usuario.Nombre = reader.GetString(4);
                    usuario.Apellido = reader.GetString(5);
                    usuario.Telefono = reader.GetString(6);
                    usuario.Estado = Convert.ToInt32(reader.GetBoolean(7));
                    usuario.FechaNacimiento = reader.GetDateTime(8);
                    usuario.ImagenUsuario = reader.GetString(9);
                    usuario.Direccion = Desencriptar(reader.GetString(10));
                }
                reader.Close();
                string Json = JsonConvert.SerializeObject(usuario);
                CloseConnection();
                return Json;
            }
            catch (Exception ex )
            {
                CloseConnection();
                return ex.Message;
            }
        }
        [WebMethod]
        public bool ActualizarUsuario(Usuario user, string CorreoAnterior, string UsuaioAnterior) 
        {
                Usuario Xuser = JsonConvert.DeserializeObject<Usuario>(ObtenerUsuario(UsuaioAnterior));
                if (Xuser.usuario == UsuaioAnterior && Xuser.CorreoElectronico == CorreoAnterior)
                {
                    
                        if (VerificarExistenciaUsuario(user.usuario, "a") && UsuaioAnterior != user.usuario)
                        {

                            return false;
                        }
                        else if (VerificarExistenciaUsuario("a", user.CorreoElectronico) && CorreoAnterior != user.CorreoElectronico) 
                        {
                            return false;
                        }
                    
                    int Id = 0;
                    OpenConnection();
                    SqlCommand command = new SqlCommand("select Id from [dbo].[Usuario] where Usuario = '" + Encriptar(UsuaioAnterior) + "';", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                    }
                    reader.Close();
                    command = new SqlCommand("ActualizarUsuario", connection);
                    // Especificar que se va a ejecutar un procedimiento almacenado
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros al comando
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@Usuario", Encriptar(user.usuario));
                    command.Parameters.AddWithValue("@CorreoElectronico", Encriptar(user.CorreoElectronico));
                    command.Parameters.AddWithValue("@Clave", Encriptar(user.Clave));
                    command.Parameters.AddWithValue("@Nombre", user.Nombre);
                    command.Parameters.AddWithValue("@Apellido", user.Apellido);
                    command.Parameters.AddWithValue("@NumeroDeTelefono", user.Telefono);
                    command.Parameters.AddWithValue("@Estado", user.Estado);
                    command.Parameters.AddWithValue("@FechaNacimiento", user.FechaNacimiento);
                    command.Parameters.AddWithValue("@ImagenUsuario", user.ImagenUsuario);
                    command.Parameters.AddWithValue("@Direccion", Encriptar(user.Direccion));

                    // Ejecutar el comando
                    int rowsAffected = command.ExecuteNonQuery();

                    // Comprobar cuántas filas fueron afectadas por la actualización
                    if (rowsAffected > 0)
                    {
                        CloseConnection();
                        return true;
                    }
                    else
                    {
                        CloseConnection();
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                
        
            
           
        }

        //Producto
        [WebMethod]
        public bool CrearProducto(Producto producto)
        {
            try 
            {
                if (!ProductoExiste(producto.Nombre))
                {
                    return false;
                }
                OpenConnection();
                SqlCommand command = new SqlCommand("CrearProducto", connection);
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
                CloseConnection();
                return true;
            }
            catch (Exception)
            {
                CloseConnection();
                return false;
            }
            
        }
        [WebMethod]
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
        [WebMethod]
        public bool ProductoExiste(string nombre)
        {
            OpenConnection();
            try
            {
                bool ProductoExiste = false;
                string consulta = "SELECT COUNT(*) FROM Producto WHERE Nombre = @Nombre";
                SqlCommand Command = new SqlCommand(consulta, connection);
                Command.Parameters.AddWithValue("@Nombre", nombre);

                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    ProductoExiste = true;
                }
                CloseConnection();
                return ProductoExiste;
            }
            catch (Exception)
            {
                CloseConnection();
                return false;
            }
        }
        [WebMethod]
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
        [WebMethod]
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
        [WebMethod]
        public bool ActualizarProducto(Producto producto)
        {
            try
            {
                OpenConnection();

                SqlCommand command =  command = new SqlCommand("ActualizarProducto", connection);
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
                    CloseConnection();
                    return true;
                }
                else
                {
                    CloseConnection();
                    return false;
                }

            }
            catch(Exception)
            { 
                return false;
                throw;
            }
        }
        [WebMethod]
        public bool EliminarProducto(int Id)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new SqlCommand("EliminarProducto", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", Id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    CloseConnection();
                    return false;
                }

            }
            catch (Exception)
            {
                CloseConnection();
                return false;
            }
        }

        //Marca
        [WebMethod]
        private bool MarcaExiste(string nombre) 
        {
            OpenConnection();
            try
            {
                bool MarcaExiste = false;
                string consulta = "SELECT COUNT(*) FROM Marca WHERE Nombre = @Nombre";
                SqlCommand Command = new SqlCommand(consulta, connection);
                Command.Parameters.AddWithValue("@Nombre", nombre);

                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    MarcaExiste = true;
                }
                CloseConnection();
                return MarcaExiste;
            }
            catch (Exception)
            {
                CloseConnection();
                return false;
            }
        }
        [WebMethod]
        public bool AgregarMarca(Marca marca)
        {
            try
            {
                if (MarcaExiste(marca.Nombre))
                {
                    return false;
                }
                OpenConnection();
                SqlCommand command = new SqlCommand("AgregarMarca", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                command.Parameters.AddWithValue("@Descripcion", marca.Descripcion);
                command.Parameters.AddWithValue("@Logo", marca.Logo);
                command.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            catch(Exception)
            {
                CloseConnection();
                return false; 
            }
        }
        [WebMethod]
        public string ObtenerMarca() 
        {
            OpenConnection();
            try
            {
                List<Marca> list = new List<Marca>();
                int i = 0;
                SqlCommand command = new SqlCommand("SELECT  * FROM [dbo].[Marca] order by Id asc;", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Marca());
                    list[i].Id = reader.GetInt32(0);
                    list[i].Nombre = reader.GetString(1);
                    list[i].Descripcion = reader.GetString(2);
                    list[i].Logo = Desencriptar(reader.GetString(3));
                    i++;
                }
                reader.Close();
                string Json = JsonConvert.SerializeObject(list);
                CloseConnection();
                return Json;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return ex.Message;
            }
        }
        [WebMethod]
        public bool ActualizarMarca(Marca marca) 
        { 
            try
            {
                OpenConnection();

                SqlCommand command = command = new SqlCommand("ActualizarMarca", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", marca.Id);
                command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                command.Parameters.AddWithValue("@Descripcion", marca.Descripcion);
                command.Parameters.AddWithValue("@Logo", marca.Logo);
                
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    CloseConnection();
                    return false;
                }

            }
            catch(Exception)
            { 
                CloseConnection(); 
                return false; 
            }    

        }
        [WebMethod]
        public bool EliminarMarca(int Id)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new SqlCommand("EliminarMarca", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", Id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    CloseConnection();
                    return false;
                }

            }
            catch (Exception)
            {
                CloseConnection();
                return false;
            }
        }

        //Categoria
        [WebMethod]
        private bool CategoriaExiste(string nombre)
        {
            try
            {
                OpenConnection();
                bool Categoria = false;
                string consulta = "SELECT COUNT(*) FROM Categoria WHERE Nombre = @Nombre";
                SqlCommand Command = new SqlCommand(consulta, connection);
                Command.Parameters.AddWithValue("@Nombre", nombre);

                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    Categoria = true;
                }
                CloseConnection();
                return Categoria;
            }
            catch (Exception)
            {
                CloseConnection();
                return false;
            }
        }
        [WebMethod]
        public bool AgregarCategoria(Categoria categoria)
        {
            try
            {
                if (CategoriaExiste(categoria.Nombre))
                {
                    return false;
                }
                OpenConnection();
                SqlCommand command = new SqlCommand("AgregarCategoria", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                command.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                command.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            catch(Exception)
            { 
                CloseConnection();
                return false; 
            }
        }
        [WebMethod]
        public string ObtenerCategoria()
        {
            OpenConnection();
            try
            {
                List<Categoria> list = new List<Categoria>();
                int i = 0;
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Categoria] order by Id asc;;", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Categoria());
                    list[i].Id = reader.GetInt32(0);
                    list[i].Nombre = reader.GetString(1);
                    list[i].Descripcion = reader.GetString(2);
                    i++;
                }
                reader.Close();
                string Json = JsonConvert.SerializeObject(list);
                CloseConnection();
                return Json;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return ex.Message;
            }
        }
        [WebMethod]
        public bool ActualizarCategoria(Categoria categoria)
        {
            try
            {
                OpenConnection();

                SqlCommand command = command = new SqlCommand("ActualizarCategoria", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", categoria.Id);
                command.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                command.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    CloseConnection();
                    return false;
                }
            }
            catch(Exception)
            {
                CloseConnection();
                return false; 
            }
        }
        [WebMethod]
        public bool EliminarCategoria(int Id)
        {
            try
            {
                OpenConnection();

                SqlCommand command = new SqlCommand("EliminarCategoria", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", Id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    CloseConnection();
                    return true;
                }
                else
                {
                    CloseConnection();
                    return false;
                }

            }
            catch (Exception)
            {
                CloseConnection();
                return false;
            }
        }

        //Subir imagenes
        [WebMethod]
        public string SubirImagen(int orden, byte [] archivoB, string nombre) 
        {   
            string URL = null;
            Stream archivo = new MemoryStream(archivoB);
            switch (orden)
            {
                case 1:
                    URL =  SubirStorage( archivo,  nombre, "FotoPrincipalProducto");
                    break;
                case 2:
                    URL =  SubirStorage(archivo, nombre, "FotosProducto");
                    break;
                case 3:
                    URL =  SubirStorage(archivo, nombre, "FotoUsuario");
                    break;
                case 4:
                    URL =  SubirStorage(archivo, nombre, "FotoEmpleado");
                    break;
                case 5:
                    URL =  SubirStorage(archivo, nombre, "FotoMarca");
                    break;
            }
            return Encriptar(URL);
        }
        [WebMethod]
        private string SubirStorage(Stream archivo, string nombre, string carpeta)
        {
            client = new FireSharp.FirebaseClient(Conexion);
            int i = 1;
            string x = nombre;
            while (ImagenExiste(nombre))
            {
                //actual
                nombre = x + i;
                i++;
            }
            try
            {
                string ruta = "tiendabd-204c4.appspot.com";

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    ruta,
                    new FirebaseStorageOptions
                    {
                        ThrowOnCancel = true
                    })
                    .Child(carpeta)
                    .Child(nombre)
                    .PutAsync(archivo, cancellation.Token);

                var downloadURL = task.GetAwaiter().GetResult(); // Espera a que se complete la subida y obtiene la URL pública
                
                FirebaseResponse Fotos = client.Set("/Imagenes/"+nombre,Encriptar(downloadURL));

                return downloadURL;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [WebMethod]
        public bool ImagenExiste(string nombre)
        {
            client = new FireSharp.FirebaseClient(Conexion);
            FirebaseResponse Fotos = client.Get("/Imagenes/" + nombre);
            if (Fotos.Body != "null")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
