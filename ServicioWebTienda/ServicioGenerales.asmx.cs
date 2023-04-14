using Firebase.Storage;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace ServicioWebTienda
{
    /// <summary>
    /// Descripción breve de ServicioGenerales
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioGenerales : System.Web.Services.WebService
    {
        ConexionBD conexionBD = new ConexionBD();

        [WebMethod]
        public string SubirImagen(int orden, byte[] archivoB, string nombre, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                string URL = null;
                Stream archivo = new MemoryStream(archivoB);
                switch (orden)
                {
                    case 1:
                        URL = SubirStorage(archivo, nombre, "FotoPrincipalProducto", Remitente, Origen);
                        break;
                    case 2:
                        URL = SubirStorage(archivo, nombre, "FotosProducto", Remitente, Origen);
                        break;
                    case 3:
                        URL = SubirStorage(archivo, nombre, "FotoUsuario", Remitente, Origen);
                        break;
                    case 4:
                        URL = SubirStorage(archivo, nombre, "FotoEmpleado", Remitente, Origen);
                        break;
                    case 5:
                        URL = SubirStorage(archivo, nombre, "FotoMarca", Remitente, Origen);
                        break;
                }
                LOG lOG = new LOG();
                lOG.Resultado = URL;
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return conexionBD.Encriptar(URL);
            }
            catch (Exception ex)
            {
                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return null;
            }


        }
        [WebMethod]
        private string SubirStorage(Stream archivo, string nombre, string carpeta, string Remitente = "Anonimo", int Origen = 1)
        {
            conexionBD.client = new FireSharp.FirebaseClient(conexionBD.Conexion);
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

                FirebaseResponse Fotos = conexionBD.client.Set("/Imagenes/" + nombre, conexionBD.Encriptar(downloadURL));

                LOG lOG = new LOG();
                lOG.Resultado =conexionBD.Encriptar(downloadURL);
                lOG.Remitente = Remitente;
                lOG.Tipo = 1;
                conexionBD.RegistroHistorico(lOG, Origen);
                return downloadURL;
            }
            catch (Exception ex)
            {

                LOG lOG = new LOG();
                lOG.Resultado = ex.Message;
                lOG.Remitente = Remitente;
                lOG.Tipo = 2;
                conexionBD.RegistroHistorico(lOG, Origen);
                conexionBD.CloseConnection();
                return null;
            }
        }
        [WebMethod]
        public bool ImagenExiste(string nombre, string Remitente = "Anonimo", int Origen = 1)
        {
            try
            {
                conexionBD.client = new FireSharp.FirebaseClient(conexionBD.Conexion);
                FirebaseResponse Fotos = conexionBD.client.Get("/Imagenes/" + nombre);
                if (Fotos.Body != "null")
                {
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
