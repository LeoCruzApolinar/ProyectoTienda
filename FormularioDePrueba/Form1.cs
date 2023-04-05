using Firebase.Auth;
using Firebase.Storage; 
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FormularioDePrueba
{
    public partial class Form1 : Form
    {
        public class error 
        {
            public string Mensaje;
            public int Estado;
        }
        static IFirebaseConfig Conexion = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "oYbtwGC8zihW0MqyztWw7o8i8Ryd9Tw6e0bSsIPJ",
            BasePath = "https://tiendabd-204c4-default-rtdb.firebaseio.com/"
        };
        static IFirebaseClient client = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var error = new error()
            {
                Mensaje = "Nuevo error",
                Estado = 0,
            };
            SetResponse response = client.Set("/FilaErrores/ObtenerProductos" + "/" + "Error " + ObtenerSiguiente(), error);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(Conexion);
        }
        int ObtenerSiguiente() 
        {
            FirebaseResponse response =  client.Get("/FilaErrores/ObtenerProductos");
            if (response.Body != "null") 
            {
                Dictionary<string, error> Diccionario = new Dictionary<string, error>();
                Diccionario = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, error>>(response.Body);
    
                return Diccionario.Count + 1;
            }
            else
            {
           
                response = client.Set("/FilaErrores/ObtenerProductos", "");
                return 1;
            }
        }

        private void IncrementoOrden()
        {
            FirebaseResponse response = client.Get("/FilaErrores/Orden");
            int X = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(response.Body);
            X++;
            response = client.Set("/FilaErrores/Orden",X);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceReference1.ServicioASoapClient servicioA = new ServiceReference1.ServicioASoapClient();
            ServicioWebTienda.ServicioA servicioA1 = new ServicioWebTienda.ServicioA();
            

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Filtro de archivos para que sólo se muestren imágenes
            openFileDialog1.Filter = "Archivos de imagen (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png";

            // Se muestra el diálogo y se espera a que el usuario seleccione un archivo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Se obtiene la ruta del archivo seleccionado
                string rutaImagen = openFileDialog1.FileName;

                // Aquí puedes hacer lo que quieras con la imagen seleccionada, por ejemplo:
                pictureBox1.Image = Image.FromFile(rutaImagen);

                
                MemoryStream ms = new MemoryStream();
                Image.FromFile(rutaImagen).Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] archivoB = ms.ToArray();
               
                string url =  servicioA.SubirImagen(1, archivoB, "imagen69");
                label1.Text = url;

            }
        }
        //private string SubirStorage(Stream archivo, string nombre, string carpeta)
        //{

        //    try
        //    {
        //        string ruta = "tiendabd-204c4.appspot.com";

        //        var cancellation = new CancellationTokenSource();

        //        var task = new FirebaseStorage(
        //            ruta,
        //            new FirebaseStorageOptions
        //            {
        //                ThrowOnCancel = true
        //            })
        //            .Child(carpeta)
        //            .Child(nombre)
        //            .PutAsync(archivo, cancellation.Token);


        //        var downloadURL = task;


        //        return downloadURL.ToString();
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }

        //}
        //public async Task<string> SubirStorage(Stream archivo, string nombre)
        //{

        //    string ruta = "tiendabd-204c4.appspot.com";

        //    var cancellation = new CancellationTokenSource();

        //    var task = new FirebaseStorage(
        //        ruta,
        //        new FirebaseStorageOptions
        //        {
        //            ThrowOnCancel = true
        //        })
        //        .Child("Fotos_Perfil")
        //        .Child(nombre)
        //        .PutAsync(archivo, cancellation.Token);


        //    var downloadURL = await task;


        //    return downloadURL;


        //}

    }
}
