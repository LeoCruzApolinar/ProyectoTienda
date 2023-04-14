using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using FireSharp.Interfaces;
using FireSharp.Config;
using System.Data;
using FireSharp.Response;

namespace ServicioWebTienda
{
    public sealed class ConexionBD
    {
        //Firebase TiendaBD
        public IFirebaseConfig Conexion = new FirebaseConfig
        {
            AuthSecret = "oYbtwGC8zihW0MqyztWw7o8i8Ryd9Tw6e0bSsIPJ",
            BasePath = "https://tiendabd-204c4-default-rtdb.firebaseio.com/"
        };
        public IFirebaseClient client = null;

        //Firebase RegistroHistorico
        public IFirebaseConfig Conect = new FirebaseConfig
        {
            AuthSecret = "xj5TnAyrgJnzvD3VhcQI2X0CuUqCRQuBHScetgSW",
            BasePath = "https://historico-38ee4-default-rtdb.firebaseio.com/"
        };
        //Base de datos
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
        public void RegistroHistorico(LOG lOG, int origen)
        {
            try
            {
                string fechaHora = DateTime.Now.ToString("dd-MM-yyyy/HH/mm/ss/FFF-ffff-fffff");
                client = new FireSharp.FirebaseClient(Conect);

                // Rutas de ubicación en la base de datos
                Dictionary<int, string> rutas = new Dictionary<int, string>()
        {
            { 1, "RegistroHistorico/Web/Core" },
            { 2, "RegistroHistorico/Caja/Core" }
        };

                // Verifica si la ruta de ubicación está definida
                if (!rutas.TryGetValue(origen, out string rutaBase))
                {
                    throw new ArgumentException($"Origen no válido: {origen}");
                }

                // Determina si es un registro de éxito o de error
                string rutaTipo = (lOG.Tipo == 1) ? "Exito" : "Errores";

                // Ruta completa de ubicación en la base de datos
                string ruta = $"{rutaBase}/{rutaTipo}/{lOG.Remitente}/{fechaHora}";

                // Establece el registro histórico en la base de datos
                FirebaseResponse respuesta = client.Set(ruta, lOG);
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir
            }
        }
    }

}