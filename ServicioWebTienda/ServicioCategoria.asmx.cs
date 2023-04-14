using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Reflection;

namespace ServicioWebTienda
{
    /// <summary>
    /// Descripción breve de ServicioCategoria
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioCategoria : System.Web.Services.WebService
    {
        ConexionBD conexionBD = new ConexionBD();
        //Categoria
        [WebMethod]
        private bool CategoriaExiste(string nombre, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();
                bool Categoria = false;
                string consulta = "SELECT COUNT(*) FROM Categoria WHERE Nombre = @Nombre";
                SqlCommand Command = new SqlCommand(consulta, conexionBD.connection);
                Command.Parameters.AddWithValue("@Nombre", nombre);

                int Registro = (int)Command.ExecuteScalar();

                if (Registro > 0)
                {
                    Categoria = true;
                }
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = Categoria.ToString();
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return Categoria;
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
        public bool AgregarCategoria(Categoria categoria, string Remitente = "Anonimo", int Origen = 1)
        {
            LOG lOG = new LOG();
            try
            {
                if (CategoriaExiste(categoria.Nombre, Remitente, Origen))
                {
                    
                    lOG.Resultado = false.ToString();
                    lOG.Remitente = Remitente;
                    lOG.Tipo = 2;
                    conexionBD.RegistroHistorico(lOG, Origen);
                    return false;
                }
                conexionBD.OpenConnection();
                SqlCommand command = new SqlCommand("AgregarCategoria", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                command.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
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
                conexionBD.CloseConnection();

                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                return false;
            }
        }
        [WebMethod]
        public string ObtenerCategoria(string Remitente = "Anonimo", int Origen = 1)
        {
            conexionBD.OpenConnection();
            try
            {
                List<Categoria> list = new List<Categoria>();
                int i = 0;
                SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Categoria] order by Id asc;", conexionBD.connection);
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
        public bool ActualizarCategoria(Categoria categoria, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = command = new SqlCommand("ActualizarCategoria", conexionBD.connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", categoria.Id);
                command.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                command.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

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
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                return false;
            }
        }
        [WebMethod]
        public bool EliminarCategoria(int Id, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.OpenConnection();

                SqlCommand command = new SqlCommand("EliminarCategoria", conexionBD.connection);
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
                conexionBD.CloseConnection();
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                return false;
            }
        }
    }
}
