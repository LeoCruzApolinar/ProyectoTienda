using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicioWebTienda
{
    /// <summary>
    /// Descripción breve de ServicioEmpleado
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioEmpleado : System.Web.Services.WebService
    {
        ConexionBD conexionBD = new ConexionBD();

        [WebMethod]
        public bool VerificarExistenciaEmpleado(string usuario, string CorreoElectronico, string Remitente = "Anonimo", int Origen = 1)
        {
            conexionBD.OpenConnection();
            try
            {
                bool EmpleadoExistente = false;
                string consulta = "SELECT COUNT(*) FROM Empleado WHERE Empleado = @Usuario or CorreoElectronico = @CorreoElectronico";
                SqlCommand Command = new SqlCommand(consulta, conexionBD.connection);
                Command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(usuario));
                Command.Parameters.AddWithValue("@CorreoElectronico", conexionBD.Encriptar(CorreoElectronico));


                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    EmpleadoExistente = true;
                }
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = EmpleadoExistente.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return EmpleadoExistente;
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
        public bool IngresarEmpleado(Empleado empleado, string Remitente = "Anonimo", int Origen = 1) 
        {
            if (!VerificarExistenciaEmpleado(empleado.Usuario, empleado.CorreoElectronico,Remitente,Origen))
            {
                conexionBD.OpenConnection();
                try
                {
                    SqlCommand command = new SqlCommand("InsertarEmpleado", conexionBD.connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(empleado.Usuario));
                    command.Parameters.AddWithValue("@CorreoElectronico", conexionBD.Encriptar(empleado.CorreoElectronico));
                    command.Parameters.AddWithValue("@Clave", conexionBD.Encriptar(empleado.Clave));
                    command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    command.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                    command.Parameters.AddWithValue("@NumeroDeTelefono", empleado.Telefono);
                    command.Parameters.AddWithValue("@Estado", empleado.Estado);
                    command.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento);
                    command.Parameters.AddWithValue("@ImagenEmpleado", conexionBD.Encriptar(empleado.ImagenEmpleado));
                    command.Parameters.AddWithValue("@Cedula", conexionBD.Encriptar(empleado.Cedula));
                    command.Parameters.AddWithValue("@IdRol", empleado.IdRol);
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
                return false;
            }
        }
        [WebMethod]
        public string ObtenerEmpleado(string Usuario, string Remitente = "Anonimo", int Origen = 1)
        {
            conexionBD.OpenConnection();
            try
            {
                Empleado empleado = new Empleado();

                SqlCommand command = new SqlCommand("select * from [dbo].[Empleado] where Empleado =  @Usuario;", conexionBD.connection);
                command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(Usuario));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    empleado.Usuario = conexionBD.Desencriptar(reader.GetString(1));
                    empleado.CorreoElectronico = conexionBD.Desencriptar(reader.GetString(2));
                    empleado.Clave = conexionBD.Desencriptar(reader.GetString(3));
                    empleado.Nombre = reader.GetString(4);
                    empleado.Apellido = reader.GetString(5);
                    empleado.Telefono = reader.GetString(6);
                    empleado.Estado = Convert.ToInt32(reader.GetBoolean(7));
                    empleado.FechaNacimiento = reader.GetDateTime(8);
                    empleado.ImagenEmpleado = conexionBD.Desencriptar(reader.GetString(9));
                    empleado.Cedula = conexionBD.Desencriptar(reader.GetString(10));
                    empleado.IdRol = Convert.ToInt32(reader.GetString(11));
                }
                reader.Close();
                string Json = JsonConvert.SerializeObject(empleado);
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = Json;
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return Json;
            }
            catch (Exception ex)
            {
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                return ex.Message;
            }
        }
        [WebMethod]
        public bool ActualizarEmpleado(Empleado empleado, string CorreoAnterior, string UsuarioAnterior, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                Empleado XEmpleado = JsonConvert.DeserializeObject<Empleado>(ObtenerEmpleado(UsuarioAnterior));
                if (XEmpleado.Usuario == UsuarioAnterior && XEmpleado.CorreoElectronico == CorreoAnterior)
                {

                    if (VerificarExistenciaEmpleado(empleado.Usuario, "a", Remitente, Origen) && UsuarioAnterior != empleado.Usuario)
                    {
                        LOG lOG = new LOG();
                        lOG.Resultado = false.ToString();
                        lOG.Remitente = Remitente;
                        lOG.Tipo = 1;
                        conexionBD.RegistroHistorico(lOG, Origen);
                        return false;
                    }
                    else if (VerificarExistenciaEmpleado("a", empleado.CorreoElectronico, Remitente, Origen) && CorreoAnterior != empleado.CorreoElectronico)
                    {
                        LOG lOG = new LOG();
                        lOG.Resultado = false.ToString();
                        lOG.Remitente = Remitente;
                        lOG.Tipo = 1;
                        conexionBD.RegistroHistorico(lOG, Origen);
                        return false;
                    }

                    int Id = 0;
                    conexionBD.OpenConnection();
                    SqlCommand command = new SqlCommand("select Id from [dbo].[Empleado] where Usuario = @Usuario;", conexionBD.connection);
                    command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(UsuarioAnterior));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                    }
                    reader.Close();
                    command = new SqlCommand("ActualizarEmpleado", conexionBD.connection);
                    // Especificar que se va a ejecutar un procedimiento almacenado
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros al comando
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Usuario", conexionBD.Encriptar(empleado.Usuario));
                    command.Parameters.AddWithValue("@CorreoElectronico", conexionBD.Encriptar(empleado.CorreoElectronico));
                    command.Parameters.AddWithValue("@Clave", conexionBD.Encriptar(empleado.Clave));
                    command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    command.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                    command.Parameters.AddWithValue("@NumeroDeTelefono", empleado.Telefono);
                    command.Parameters.AddWithValue("@Estado", empleado.Estado);
                    command.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento);
                    command.Parameters.AddWithValue("@ImagenEmpleado", conexionBD.Encriptar(empleado.ImagenEmpleado));
                    command.Parameters.AddWithValue("@Cedula", conexionBD.Encriptar(empleado.Cedula));
                    command.Parameters.AddWithValue("@IdRol", empleado.IdRol);


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
        public bool EliminarEmpleado(int Id, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = new SqlCommand("EliminarEmpleado", conexionBD.connection);
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
        [WebMethod]
        public bool RolExiste(string nombre, string Remitente = "Anonimo", int Origen = 1)
        {
            conexionBD.OpenConnection();
            try
            {
                bool RolExiste = false;
                string consulta = "SELECT COUNT(*) FROM Rol WHERE Nombre = @Nombre";
                SqlCommand Command = new SqlCommand(consulta, conexionBD.connection);
                Command.Parameters.AddWithValue("@Nombre", nombre);

                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    RolExiste = true;
                }
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = RolExiste.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return RolExiste;
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
        public bool AgregarRol(Rol rol, string Remitente = "Anonimo", int Origen = 1) 
        {
            try
            {
                LOG lOG = new LOG();
                if (RolExiste(rol.Nombre, Remitente, Origen))
                {
                    
                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    return false;
                }
                conexionBD.OpenConnection();
                SqlCommand command = new SqlCommand("AgregarRol", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                command.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
                command.ExecuteNonQuery();
                conexionBD.CloseConnection();
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
        public string ObtenerRol(string Remitente = "Anonimo", int Origen = 1) 
        {
            conexionBD.OpenConnection();
            try
            {
                List<Rol> list = new List<Rol>();
                int i = 0;
                SqlCommand command = new SqlCommand("SELECT  * FROM [dbo].[Rol] order by Id asc;", conexionBD.connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Rol());
                    list[i].Id = reader.GetInt32(0);
                    list[i].Nombre = reader.GetString(1);
                    list[i].Descripcion = reader.GetString(2);
                    i++;
                }
                reader.Close();
                string Json = JsonConvert.SerializeObject(list);
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = Json;
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
        public bool ActualizarRol(Rol rol, string Remitente = "Anonimo", int Origen = 1) 
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = command = new SqlCommand("ActualizarRol", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", rol.Id);
                command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                command.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
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
        [WebMethod]
        public bool EliminarRol(int Id, string Remitente = "Anonimo", int Origen = 1) 
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = new SqlCommand("EliminarRol", conexionBD.connection);
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
