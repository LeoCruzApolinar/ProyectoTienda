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
    /// Descripción breve de ServicioMarca
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioMarca : System.Web.Services.WebService
    {
        ConexionBD conexionBD = new ConexionBD();
        //Marca
        [WebMethod]
        private bool MarcaExiste(string nombre, string Remitente = "Anonimo", int Origen = 1)
        {
            conexionBD.OpenConnection();
            try
            {
                bool MarcaExiste = false;
                string consulta = "SELECT COUNT(*) FROM Marca WHERE Nombre = @Nombre";
                SqlCommand Command = new SqlCommand(consulta, conexionBD.connection);
                Command.Parameters.AddWithValue("@Nombre", nombre);

                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    MarcaExiste = true;
                }
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = MarcaExiste.ToString();
                lOG.Remitente =Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return MarcaExiste;
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
        public bool AgregarMarca(Marca marca, string Remitente = "Anonimo", int Origen = 1)
        {
            LOG lOG = new LOG();
            try
            {
                if (MarcaExiste(marca.Nombre,Remitente,Origen))
                {

                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 1;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return false;
                }
                conexionBD.OpenConnection();
                SqlCommand command = new SqlCommand("AgregarMarca", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                command.Parameters.AddWithValue("@Descripcion", marca.Descripcion);
                command.Parameters.AddWithValue("@Logo", marca.Logo);
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
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return false;
            }
        }
        [WebMethod]
        public string ObtenerMarca(string Remitente = "Anonimo", int Origen = 1)
        {
            conexionBD.OpenConnection();
            try
            {
                List<Marca> list = new List<Marca>();
                int i = 0;
                SqlCommand command = new SqlCommand("SELECT  * FROM [dbo].[Marca] order by Id asc;", conexionBD.connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Marca());
                    list[i].Id = reader.GetInt32(0);
                    list[i].Nombre = reader.GetString(1);
                    list[i].Descripcion = reader.GetString(2);
                    list[i].Logo = conexionBD.Desencriptar(reader.GetString(3));
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
        public bool ActualizarMarca(Marca marca, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = command = new SqlCommand("ActualizarMarca", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", marca.Id);
                command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                command.Parameters.AddWithValue("@Descripcion", marca.Descripcion);
                command.Parameters.AddWithValue("@Logo", marca.Logo);

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
        public bool EliminarMarca(int Id, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = new SqlCommand("EliminarMarca", conexionBD.connection);
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
