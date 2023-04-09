using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace ADCT
{
    internal class Program
    {
        public static string User = null;
        public static string BloqueVer = null;
        static IFirebaseConfig Conexion = new FirebaseConfig
        {
            AuthSecret = "oYbtwGC8zihW0MqyztWw7o8i8Ryd9Tw6e0bSsIPJ",
            BasePath = "https://tiendabd-204c4-default-rtdb.firebaseio.com/"
        };
        static IFirebaseClient client = null;
        class Bloque
        {
            public int Orden = 0;
            public bool Estado= false;
        }
        class Error 
        {
            public int Metodo;
            public string Propiedad;
            public string Mensaje;
            public bool Estado;

        }
        static async Task Main(string[] args)
        {
            Console.WriteLine("En ejecucion");
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CerrarPrograma);
            Console.WriteLine("Presiona Ctrl+C o Ctrl+Break para salir del programa.");
            client = new FireSharp.FirebaseClient(Conexion);
            User = GetUserName() + GetMachineId();
            if (UsuarioExiste()) 
            {
                FirebaseResponse OUsuario = client.Get("/CDE/BR/" + User);
                BloqueVer = JsonConvert.DeserializeObject<string>(OUsuario.Body.ToString());
                FirebaseResponse ActualizarEstadBloque = client.Set("/CDE/" + BloqueVer +"/Estado",true);
            }
            else
            {
                BloqueVer = JsonConvert.DeserializeObject<string>(CrearUsuarioYBloque());
                   FirebaseResponse ActualizarEstadBloque = client.Set("/CDE/" + BloqueVer +"/Estado",true);
            }
            if (BloqueVer != null)
            {
                int orden = await GetNextOrder(client, BloqueVer);

                while (true)
                {
                    var error = await GetNextError(client, orden, BloqueVer);
                    if (error == null)
                    {
                        await Task.Delay(1000); // wait 1 second before checking again
                        continue;
                    }

                    await FixError(client, error, orden, BloqueVer);
                    orden++;
                }
            }
           
        }
        static void CerrarPrograma(object sender, ConsoleCancelEventArgs args)
        {
            FirebaseResponse ActualizarEstadBloque = client.Set("/CDE/" + BloqueVer + "/Estado", false);
            Console.WriteLine("Cerrando");
            Thread.Sleep(2000);
            Environment.Exit(0);
            // Aquí puedes poner el código que quieres ejecutar al cerrar el programa.
        }
        static async Task<int> GetNextOrder(IFirebaseClient client,string bloque)
        {
            var response = await client.GetTaskAsync($"/CDE/{bloque}/Orden");
            int result = int.Parse(response.Body);
            return result;
        }
        static async Task<Error> GetNextError(IFirebaseClient client, int orden, string bloque)
        {
            var response = await client.GetTaskAsync($"/CDE/{bloque}/Error{orden}");
            Error result = Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(response.Body); ;
            return result;
        }
        public static string Metodo(int A,string propiedad) 
        {
            ProductoMetodo productoMetodo = new ProductoMetodo();
            switch (A)
            {
                case 1:
                    return productoMetodo.ComandoProductos(int.Parse(propiedad));
                break;
            }
            return null;
        }
        static async Task FixError(IFirebaseClient client, Error error, int orden, string bloque)
        {
            if (error.Estado)
            {
                string resultado = null;
                resultado = Metodo(error.Metodo,error.Propiedad);
                if (resultado != null) 
                {
                    error.Estado = false;
                    error.Mensaje = resultado;
                }
            }
            await client.UpdateTaskAsync($"/CDE/{bloque}/Error{orden}", error);
            Console.WriteLine($"Error {orden} has been fixed");
        }
        public static bool BloqueExiste(string Bloque) 
        {
            FirebaseResponse OBloque = client.Get("CDE/" + Bloque);
            if (OBloque.Body != "null")
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public static bool CBloque(string X) 
        {
            Bloque bloque = new Bloque();
            FirebaseResponse CUsuario = client.Set("/CDE/" + X, bloque);
            return true;
        }
        public static string CrearUsuarioYBloque()
        {
            int i = 0;
            string Bloque = "Bloque";
            string X = "";
            do
            {
                i++;
                X = Bloque + i;
            } while (BloqueExiste(X));
            Bloque = X;
            if (CBloque(Bloque))
            {
                FirebaseResponse CUsuario = client.Set("/CDE/BR/" + User, Bloque);
            }
            if (UsuarioExiste())
            {
                FirebaseResponse OUsuario = client.Get("/CDE/BR/" + User);
                return OUsuario.Body;
            }
            return null;
 
        }
        public static bool UsuarioExiste() 
        {
            FirebaseResponse OUsuario = client.Get("/CDE/BR/"+ User);
            if (OUsuario.Body != "null") 
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public static string GetMachineId()
        {
            string computerId = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");
            foreach (ManagementObject obj in searcher.Get())
            {
                computerId = obj["UUID"].ToString();
                break;
            };
            return computerId;
        }
        public static string GetUserName()
        {
            return Environment.UserName;
        }

    }
}
