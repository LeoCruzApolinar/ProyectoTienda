using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MyNamespace
{
    public class MyClass
    {
        public async Task<string> IniciarSesionFirebase()
        {
            var url = "https://capaintegracion20230413222753.azurewebsites.net/api/producto/IniciarSesionFirebase/{email}/{contraseña}/{remitente}/{Origen}";

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var serializer = new JavaScriptSerializer();
            var json = serializer.Deserialize<string>(result);

            return json;
        }
    }
}
