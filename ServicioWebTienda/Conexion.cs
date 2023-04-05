using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace ServicioWebTienda
{
    public sealed class ConexionBD
    {
        private static readonly ConexionBD instance = new ConexionBD();
        private readonly string connectionString = "Data Source=softwaretienda.database.windows.net;Initial Catalog=Tienda;Persist Security Info=True;User ID=AdminSql;Password=Software1234";
        private readonly SqlConnection connection;

        private ConexionBD()
        {
            connection = new SqlConnection(connectionString);
         
        }

        public static ConexionBD Instance
        {
            get
            {
                return instance;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return connection;
            }
        }
    }

}