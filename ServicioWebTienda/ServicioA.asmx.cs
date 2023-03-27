using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicioWebTienda
{
    /// <summary>
    /// Descripción breve de ServicioA
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioA : System.Web.Services.WebService
    {
        private string conexion = "Data Source=softwaretienda.database.windows.net;Initial Catalog=Tienda;Persist Security Info=True;User ID=AdminSql;Password=Software1234";
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
            byte[] bytes = Convert.FromBase64String(X);
            string cadena = System.Text.Encoding.UTF8.GetString(bytes);
            return cadena;
        }
        [WebMethod]
        private  void CrearCuenta(string Email, string contrasena)
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
            var userCredential =  client.CreateUserWithEmailAndPasswordAsync(Email, contrasena);
        }

        [WebMethod]
        private bool VerificarExistencia(Usuario user)
        {
            bool UsuarioExistente = true;
            SqlConnection Connection = new SqlConnection(conexion);

            string consulta = "SELECT COUNT(*) FROM Usuario WHERE Usuario = @Usuario or CorreoElectronico = @CorreoElectronico";
            SqlCommand Command = new SqlCommand(consulta, Connection);
            Command.Parameters.AddWithValue("@Usuario", Encriptar(user.usuario)); 
            Command.Parameters.AddWithValue("@CorreoElectronico", Encriptar(user.CorreoElectronico));
            Connection.Open();

            int Registro = (int)Command.ExecuteScalar();

            if ( Registro > 0 )
            {
                UsuarioExistente = false;
            }

            return UsuarioExistente;
        }

        [WebMethod]
        public string IngresarUsuario(Usuario user)
        {
            if (VerificarExistencia(user))
            {
                SqlConnection Connection = new SqlConnection(conexion);
                try
                {
                    SqlCommand command = new SqlCommand("InsertarUsuario", Connection);
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
                    CrearCuenta(user.CorreoElectronico, user.Clave);
                    Connection.Open();
                    command.ExecuteNonQuery();
                    return "Se guardo";
                }
                catch (Exception)
                {

                    return "no se guardo";

                }
            }
            else
            {
                return "El Usuario ya existe";
            }

        }


    }
}
