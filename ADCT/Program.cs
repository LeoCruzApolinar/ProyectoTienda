using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ADCT
{
    internal class Program
    {
        public class Error
        {
            public string Mensaje;
            public int Estado;
        }
        static async Task Main(string[] args)
        {
            var config = new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = "oYbtwGC8zihW0MqyztWw7o8i8Ryd9Tw6e0bSsIPJ",
                BasePath = "https://tiendabd-204c4-default-rtdb.firebaseio.com/"
            };
            var client = new FireSharp.FirebaseClient(config);

            int orden = await GetNextOrder(client);
            Console.WriteLine("En ejecucion");

            while (true)
            {
                var error = await GetNextError(client, orden);
                if (error == null)
                {
                    await Task.Delay(1000); // wait 1 second before checking again
                    continue;
                }

                await FixError(client, error, orden);
                orden++;
            }
        }

        static async Task<int> GetNextOrder(IFirebaseClient client)
        {
            var response = await client.GetTaskAsync("/FilaErrores/Orden");
            int result = int.Parse(response.Body);
            return result;
        }

        static async Task<Error> GetNextError(IFirebaseClient client, int orden)
        {
            var response = await client.GetTaskAsync($"/FilaErrores/ObtenerProductos/Error {orden}");
            Error result = Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(response.Body); ;
            return result;
        }

        static async Task FixError(IFirebaseClient client, Error error, int orden)
        {
            if (error.Estado == 1)
            {
                return;
            }

            error.Mensaje = "resuelto";
            error.Estado = 1;
            await client.UpdateTaskAsync($"/FilaErrores/ObtenerProductos/Error {orden}", error);
            Console.WriteLine($"Error {orden} has been fixed");
        }
    }
}
