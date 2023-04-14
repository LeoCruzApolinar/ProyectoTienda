using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicioWebTienda
{
    /// <summary>
    /// Descripción breve de ServicioUsuario
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioUsuario : System.Web.Services.WebService
    {
        ConexionBD conexionBD = new ConexionBD();
        //Metodos de Usuario
        [WebMethod]
        public bool EliminarUsuarioFirebase(string email, string contrasena, string Remitente = "Anonimo", int Origen = 1)
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
                LOG lOG = new LOG();
                lOG.Resultado = true.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return true;
            }
            catch (Exception)
            {
                LOG lOG = new LOG();
                lOG.Resultado = false.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return false;
            }

        }
        [WebMethod]
        public string IniciarSesionFirebase(string email, string contrasena, string Remitente = "Anonimo", int Origen = 1)
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
                        conexionBD.OpenConnection();
                        SqlCommand command = new SqlCommand("select Usuario from [dbo].[Usuario] where CorreoElectronico = @Email;", conexionBD.connection);
                        command.Parameters.AddWithValue("@Email", conexionBD.Encriptar(email));

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            loginU.User = conexionBD.Desencriptar(reader.GetString(0));
                        }
                        reader.Close();
                        conexionBD.CloseConnection();
                        loginU.Estado = true;
                        loginU.Token = userCredential.Result.User.Credential.IdToken;
                        loginU.UID = userCredential.Result.User.Uid;
                    }
                    string OBJ = JsonConvert.SerializeObject(loginU);
                    LOG lOG = new LOG();
                    lOG.Resultado = conexionBD.Encriptar(OBJ.ToString());
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return OBJ;
                }
                catch (Exception ex)
                {
                    LOG lOG = new LOG();
                    lOG.Resultado = ex.Message;
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 2;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    loginU.Estado = false;
                    loginU.Token = null;
                    string OBJ = JsonConvert.SerializeObject(loginU);
                    return OBJ;

                }
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                throw;
            }


        }
        [WebMethod]
        private void CrearCuentaFirebase(string Email, string contrasena, string Remitente = "Anonimo", int Origen = 1)
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
                LOG lOG = new LOG();
                lOG.Resultado = "Bien";
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                
            }

        }
        [WebMethod]
        private bool VerificarExistenciaUsuario(string usuario, string CorreoElectronico, string Remitente = "Anonimo", int Origen = 1)
        {
            // Abre la conexión antes de ejecutar una consulta
       
            conexionBD.OpenConnection();
            try
            {
                bool UsuarioExistente = false;
                string consulta = "SELECT COUNT(*) FROM Usuario WHERE Usuario = @Usuario or CorreoElectronico = @CorreoElectronico";
                SqlCommand Command = new SqlCommand(consulta, conexionBD.connection);
                Command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(usuario));
                Command.Parameters.AddWithValue("@CorreoElectronico", conexionBD.Encriptar(CorreoElectronico));


                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    UsuarioExistente = true;
                }
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = UsuarioExistente.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return UsuarioExistente;
            }
            catch (Exception)
            {
                LOG lOG = new LOG();
                lOG.Resultado = false.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return false;
            }

        }
        [WebMethod]
        public bool IngresarUsuario(Usuario user, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                if (!VerificarExistenciaUsuario(user.usuario, user.CorreoElectronico, Remitente, Origen))
                {
                    conexionBD.OpenConnection();
                    try
                    {


                        SqlCommand command = new SqlCommand("InsertarUsuario", conexionBD.connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(user.usuario));
                        command.Parameters.AddWithValue("@CorreoElectronico", conexionBD.Encriptar(user.CorreoElectronico));
                        command.Parameters.AddWithValue("@Clave", conexionBD.Encriptar(user.Clave));
                        command.Parameters.AddWithValue("@Nombre", user.Nombre);
                        command.Parameters.AddWithValue("@Apellido", user.Apellido);
                        command.Parameters.AddWithValue("@NumeroDeTelefono", user.Telefono);
                        command.Parameters.AddWithValue("@Estado", user.Estado);
                        command.Parameters.AddWithValue("@FechaNacimiento", user.FechaNacimiento);
                        command.Parameters.AddWithValue("@ImagenUsuario", user.ImagenUsuario);
                        command.Parameters.AddWithValue("@Direccion", conexionBD.Encriptar(user.Direccion));
                        CrearCuentaFirebase(user.CorreoElectronico, user.Clave);
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
                else
                {
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
                return false;
            }
            

        }
        [WebMethod]
        public string ObtenerUsuario(string Usuario, string Remitente = "Anonimo", int Origen = 1)
        {
            conexionBD.OpenConnection();
            try
            {
                Usuario usuario = new Usuario();


                SqlCommand command = new SqlCommand("select * from [dbo].[Usuario] where Usuario =  @Usuario;", conexionBD.connection);
                command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(Usuario));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usuario.usuario = conexionBD.Desencriptar(reader.GetString(1));
                    usuario.CorreoElectronico = conexionBD.Desencriptar(reader.GetString(2));
                    usuario.Clave = conexionBD.Desencriptar(reader.GetString(3));
                    usuario.Nombre = reader.GetString(4);
                    usuario.Apellido = reader.GetString(5);
                    usuario.Telefono = reader.GetString(6);
                    usuario.Estado = Convert.ToInt32(reader.GetBoolean(7));
                    usuario.FechaNacimiento = reader.GetDateTime(8);
                    usuario.ImagenUsuario = reader.GetString(9);
                    usuario.Direccion = conexionBD.Desencriptar(reader.GetString(10));
                }
                reader.Close();
                string Json = JsonConvert.SerializeObject(usuario);
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = conexionBD.Encriptar(Json);
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return Json;
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return ex.Message;
            }
        }
        [WebMethod]
        public bool ActualizarUsuario(Usuario user, string CorreoAnterior, string UsuaioAnterior, string Remitente = "Anonimo", int Origen = 1)
        {
            Usuario Xuser = JsonConvert.DeserializeObject<Usuario>(ObtenerUsuario(UsuaioAnterior));
            if (Xuser.usuario == UsuaioAnterior && Xuser.CorreoElectronico == CorreoAnterior)
            {

                if (VerificarExistenciaUsuario(user.usuario, "a") && UsuaioAnterior != user.usuario)
                {
                    LOG lOG = new LOG();
                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 2;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return false;
                }
                else if (VerificarExistenciaUsuario("a", user.CorreoElectronico) && CorreoAnterior != user.CorreoElectronico)
                {
                    LOG lOG = new LOG();
                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 2;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return false;
                }

                int Id = 0;
                conexionBD.OpenConnection();
                SqlCommand command = new SqlCommand("select Id from [dbo].[Usuario] where Usuario = @Usuario;", conexionBD.connection);
                command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(UsuaioAnterior));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Id = reader.GetInt32(0);
                }
                reader.Close();
                command = new SqlCommand("ActualizarUsuario", conexionBD.connection);
                // Especificar que se va a ejecutar un procedimiento almacenado
                command.CommandType = CommandType.StoredProcedure;

                // Agregar parámetros al comando
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(user.usuario));
                command.Parameters.AddWithValue("@CorreoElectronico", conexionBD.Encriptar(user.CorreoElectronico));
                command.Parameters.AddWithValue("@Clave", conexionBD.Encriptar(user.Clave));
                command.Parameters.AddWithValue("@Nombre", user.Nombre);
                command.Parameters.AddWithValue("@Apellido", user.Apellido);
                command.Parameters.AddWithValue("@NumeroDeTelefono", user.Telefono);
                command.Parameters.AddWithValue("@Estado", user.Estado);
                command.Parameters.AddWithValue("@FechaNacimiento", user.FechaNacimiento);
                command.Parameters.AddWithValue("@ImagenUsuario", user.ImagenUsuario);
                command.Parameters.AddWithValue("@Direccion", conexionBD.Encriptar(user.Direccion));

                // Ejecutar el comando
                int rowsAffected = command.ExecuteNonQuery();

                // Comprobar cuántas filas fueron afectadas por la actualización
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
                    LOG lOG = new LOG();
                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    conexionBD.CloseConnection();
                    return false;
                }
            }
            else
            {
                LOG lOG = new LOG();
                lOG.Resultado = false.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return false;
            }




        }
        [WebMethod]
        public bool EliminarUsuario(int Id, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = new SqlCommand("EliminarUsuario", conexionBD.connection);
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
                    LOG lOG = new LOG();
                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    conexionBD.CloseConnection();
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
