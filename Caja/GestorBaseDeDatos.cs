using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caja
{
    internal class GestorBaseDeDatos
    {
        public void LlamarCloner() 
        {
            string[] Tablas = new string[] { "Marca", "Categoria", "Producto", "Usuario" };
            for (int i = 0; i < Tablas.Length; i++)
            {
                GestorBaseDeDatos gestorBaseDeDatos = new GestorBaseDeDatos();
                ServicioWebTienda.ServicioGenerales servicioWeb = new ServicioWebTienda.ServicioGenerales();
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
                results = servicioWeb.Cloner("af94a2976bc34391be3c29b504434b01 8c1d9930e0284fb59ee706078fd45899 2d5deb882d634a17b77b569c9f986a8d 6e9652c0060b49e0a5db42d1019422b0 aaf70f02a434420fb182198201058871 38235fc5d8b5402abbc36b391b5ec85b 14cc41d9622f4d42be2a65dc87df4bf8 c4170df8f3ce4054a9e6c6d52ecdf2ad cb708d8dc7454033a68a3c50268b36cc 97c4d055466744758163048566b82485", Tablas[i]);
                gestorBaseDeDatos.ElCloner(results, "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\leona\\source\\repos\\ProyectoTienda\\Caja\\TiendaBD.mdf;Integrated Security=True", Tablas[i]);
            }
        }
        public void ElCloner(List<Dictionary<string, object>> data, string localConnectionString, string tableName)
        {
            // Definir la cadena de conexión a la base de datos local
            SqlConnection localConnection = new SqlConnection(localConnectionString);
            localConnection.Open();

            // Obtener la lista de tablas en la base de datos de Azure
           
                // Obtener la lista de columnas para la tabla actual
                string columnsQuery = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "'";
                SqlCommand columnsCommand = new SqlCommand(columnsQuery, localConnection);
                SqlDataReader columnsReader = columnsCommand.ExecuteReader();
                List<string> columns = new List<string>();
                while (columnsReader.Read())
                {
                    columns.Add(columnsReader[0].ToString());
                }
                columnsReader.Close();

                // Crear la consulta SQL para insertar o actualizar los datos en la tabla local
                string insertQuery = "MERGE " + tableName + " AS Target " +
                    "USING (SELECT " + string.Join(", ", columns.Select(c => "@" + c)) + ") AS Source (" + string.Join(", ", columns) + ") " +
                    "ON (" + string.Join(" AND ", columns.Select(c => "Target." + c + " = Source." + c)) + ") " +
                    "WHEN MATCHED THEN " +
                    "UPDATE SET " + string.Join(", ", columns.Select(c => "Target." + c + " = Source." + c)) + " " +
                    "WHEN NOT MATCHED THEN " +
                    "INSERT (" + string.Join(", ", columns) + ") VALUES (" + string.Join(", ", columns.Select(c => "Source." + c)) + ");";
                SqlCommand insertCommand = new SqlCommand(insertQuery, localConnection);

                // Recorrer los diccionarios devueltos por el servicio web y ejecutar la consulta INSERT para cada uno
                foreach (Dictionary<string, object> record in data)
                {
                    // Asignar valores a los parámetros de la consulta INSERT
                    // (los valores deben coincidir con el orden y el tipo de las columnas en la tabla local)
                    foreach (string column in columns)
                    {
                        object value;
                        if (record.TryGetValue(column, out value))
                        {
                            if (!insertCommand.Parameters.Contains("@" + column))
                            {
                                insertCommand.Parameters.AddWithValue("@" + column, value ?? DBNull.Value);
                            }
                            else
                            {
                                insertCommand.Parameters["@" + column].Value = value ?? DBNull.Value;
                            }
                        }
                        else
                        {
                            if (!insertCommand.Parameters.Contains("@" + column))
                            {
                                insertCommand.Parameters.AddWithValue("@" + column, DBNull.Value);
                            }
                            else
                            {
                                insertCommand.Parameters["@" + column].Value = DBNull.Value;
                            }
                        }
                    }

                    if (VerificarInser(insertCommand, tableName))
                    {
                        object idValue = insertCommand.Parameters["@" + "Id"].Value;
                        string query = "DELETE FROM "+ tableName +" WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(query, insertCommand.Connection);
                        command.Parameters.AddWithValue("@Id", idValue);
                        command.ExecuteNonQuery();
                        insertCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                    
                }
            

            localConnection.Close();
        }
        private bool VerificarInser(SqlCommand insertCommand, string tablename)
        {
            bool idExists = false;
            string idColumnName = "Id"; // reemplazar por el nombre real de la columna de ID en la tabla
            object idValue = insertCommand.Parameters["@" + idColumnName].Value;

            // Consultar si la ID ya existe en la tabla
            string selectQuery = "SELECT COUNT(*) FROM " + tablename + " WHERE " + idColumnName + " = @ID";
            SqlCommand selectCommand = new SqlCommand(selectQuery, insertCommand.Connection);
            selectCommand.Parameters.AddWithValue("@ID", idValue);
            int count = (int)selectCommand.ExecuteScalar();

            if (count > 0)
            {
                idExists = true;
            }

            return idExists;
        }





    }
}
