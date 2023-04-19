using ServicioWebTienda;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja
{
    public sealed class ConexionBD
    {

       
        public SqlConnection connection;


        public ConexionBD()
        {
            string connectionString = "Data Source=softwaretienda.database.windows.net;Initial Catalog=Tienda;Persist Security Info=True;User ID=AdminSql;Password=Software1234";
            connection = new SqlConnection(connectionString);
        }
        public void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public string Encriptar(string X)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(X);
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public string Desencriptar(string X)
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

    }
}
