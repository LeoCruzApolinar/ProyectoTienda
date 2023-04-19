using System;
using System.Collections.Generic;
using System.Web.UI;

namespace MyNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
        
            ServicioWebTienda.ServicioGenerales servicioWeb = new ServicioWebTienda.ServicioGenerales();
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
            results = servicioWeb.Cloner("af94a2976bc34391be3c29b504434b01 8c1d9930e0284fb59ee706078fd45899 2d5deb882d634a17b77b569c9f986a8d 6e9652c0060b49e0a5db42d1019422b0 aaf70f02a434420fb182198201058871 38235fc5d8b5402abbc36b391b5ec85b 14cc41d9622f4d42be2a65dc87df4bf8 c4170df8f3ce4054a9e6c6d52ecdf2ad cb708d8dc7454033a68a3c50268b36cc 97c4d055466744758163048566b82485", "Usuario");
            Console.ReadLine();
        }
    }
}
