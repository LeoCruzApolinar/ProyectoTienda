using MesaDePrueba.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesaDePrueba
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //LlenadoDeUsuarios();
            //LlenadosDeEstado();
            //llamado(1);
            //llamado(2);
            //llamado(3);
            //llamado(4);
            //llamado(5);
            //llamado(6);
            //ServicioWebTienda.ServicioA servicio = new ServicioWebTienda.ServicioA();
            //ServicioWebTienda.Usuario miUsuario = new ServicioWebTienda.Usuario();
            ServiceReference2.ServicioASoapClient servicio = new ServiceReference2.ServicioASoapClient();
            ServiceReference2.Usuario miUsuario = new ServiceReference2.Usuario();
            miUsuario.usuario = "leonardocruzapolinar12";
            miUsuario.CorreoElectronico = "leonardocruzapolinar12@gmail.com";
            miUsuario.Clave = "123";
            miUsuario.Nombre = "Jose Leonardo";
            miUsuario.Apellido = "Cruz Apolinar";
            miUsuario.Telefono = "829-559-5352";
            miUsuario.Estado = 1;
            miUsuario.FechaNacimiento = new DateTime(2004, 2, 8);
            miUsuario.ImagenUsuario = "miimagen.jpg";
            miUsuario.Direccion = "Calle A, Torre Melian";

            Console.WriteLine(servicio.IngresarUsuario(miUsuario));
            Console.ReadLine();
        }


//        static void LlenadoDeUsuarios() 
//        {
//            List<string> nombresUsuarios = new List<string>
//{
//    "JSmith",
//    "KJohnson",
//    "SMiller",
//    "DWilliams",
//    "RJones",
//    "AJohnson",
//    "TWilson",
//    "CAnderson",
//    "JBrown",
//    "MThomas",
//    "EHarris",
//    "KClark",
//    "LJackson",
//    "BThompson",
//    "JWhite",
//    "GGreen",
//    "BAdams",
//    "KDavis",
//    "MScott",
//    "DEvans",
//    "KAllen",
//    "PTurner",
//    "DJames",
//    "SPhillips",
//    "JCampbell",
//    "CEdwards",
//    "CCollins",
//    "AStewart",
//    "KBennett",
//    "RGray",
//    "MBaker",
//    "NCooper",
//    "TLewis",
//    "AParker",
//    "JMitchell",
//    "MReed",
//    "CSanchez",
//    "SGonzalez",
//    "JLopez",
//    "BMartin",
//    "CGomez",
//    "JReyes",
//    "GJenkins",
//    "ASullivan",
//    "JKim",
//    "RMurphy",
//    "LGarcia",
//    "HNguyen",
//    "EChen",
//    "MYang"
//};
//            List<string> Contrasenas = new List<string>
//{
//    "H9JfT2Ls",
//    "x6NpE1Zg",
//    "A2cWk8Tb",
//    "f7GhX9Pd",
//    "s5UjM6Kl",
//    "B8rVn4Qf",
//    "C3mRt7Yx",
//    "v1JzH0qD",
//    "W6dZk5Pn",
//    "L9pXc3Bf",
//    "S5gF2QmR",
//    "E8eD6tNf",
//    "j4uT2hVx",
//    "K7bY5aZn",
//    "G1sF3fWc",
//    "P9dL6kXm",
//    "M4tV8eZj",
//    "N5uB2cWq",
//    "Q3pA7fDx",
//    "R8gJ1hKl",
//    "z2kU6sTn",
//    "y9vC4nXb",
//    "I7wE5tHf",
//    "F1mL8pZd",
//    "o6rN9sTb",
//    "U2jG4fCn",
//    "T5xW7hKl",
//    "h9cD3kXm",
//    "G2fT6rVx",
//    "B7nU1sQm",
//    "R3mP8tLs",
//    "E9jK5hVx",
//    "M6zH2bNc",
//    "Y5aW8xQf",
//    "X1pS7tRd",
//    "w4cD3eKl",
//    "u9nF6sTb",
//    "q2vG1hXm",
//    "V7fZ5tLs",
//    "J4kT9pNc",
//    "I8hC6jWx",
//    "F3bL2sQm",
//    "o5sT9pXn",
//    "N7jK1tRf",
//    "M4zH8cVx",
//    "P9nR5fLs",
//    "Q1mW3tNc",
//    "Z6xG2kQf",
//    "Y3hA9dXm",
//    "X8pF5wTb",
//    "w2cD7eLs"
//};
//            List<string> nombres = new List<string>
//{
//    "Liam",
//    "Emma",
//    "Noah",
//    "Olivia",
//    "William",
//    "Ava",
//    "James",
//    "Isabella",
//    "Oliver",
//    "Sophia",
//    "Benjamin",
//    "Mia",
//    "Elijah",
//    "Charlotte",
//    "Lucas",
//    "Amelia",
//    "Mason",
//    "Harper",
//    "Logan",
//    "Evelyn",
//    "Alexander",
//    "Abigail",
//    "Ethan",
//    "Emily",
//    "Jacob",
//    "Elizabeth",
//    "Michael",
//    "Mila",
//    "Daniel",
//    "Ella",
//    "Henry",
//    "Avery",
//    "Jackson",
//    "Sofia",
//    "Sebastian",
//    "Camila",
//    "Aiden",
//    "Aria",
//    "Matthew",
//    "Scarlett",
//    "Samuel",
//    "Victoria",
//    "David",
//    "Madison",
//    "Joseph",
//    "Luna",
//    "Carter",
//    "Grace",
//    "Owen",
//    "Chloe"
//};
//            List<string> apellidos = new List<string>
//{
//    "Smith",
//    "Johnson",
//    "Brown",
//    "Garcia",
//    "Miller",
//    "Davis",
//    "Rodriguez",
//    "Martinez",
//    "Hernandez",
//    "Lopez",
//    "Gonzalez",
//    "Perez",
//    "Taylor",
//    "Anderson",
//    "Wilson",
//    "Jackson",
//    "Moore",
//    "Martin",
//    "Lee",
//    "Gomez",
//    "Harris",
//    "Clark",
//    "Lewis",
//    "Robinson",
//    "Walker",
//    "Parker",
//    "Hall",
//    "Young",
//    "Allen",
//    "King",
//    "Wright",
//    "Scott",
//    "Green",
//    "Baker",
//    "Adams",
//    "Nelson",
//    "Carter",
//    "Mitchell",
//    "Perez",
//    "Roberts",
//    "Turner",
//    "Phillips",
//    "Campbell",
//    "Parker",
//    "Evans",
//    "Edwards",
//    "Collins",
//    "Stewart",
//    "Sanchez",
//    "Morris",
//    "Rogers"
//};
//            List<string> numeros = new List<string>
//{
//    "123-456-7890",
//    "234-567-8901",
//    "345-678-9012",
//    "456-789-0123",
//    "567-890-1234",
//    "678-901-2345",
//    "789-012-3456",
//    "890-123-4567",
//    "901-234-5678",
//    "012-345-6789",
//    "111-222-3333",
//    "222-333-4444",
//    "333-444-5555",
//    "444-555-6666",
//    "555-666-7777",
//    "666-777-8888",
//    "777-888-9999",
//    "888-999-0000",
//    "999-000-1111",
//    "000-111-2222",
//    "987-654-3210",
//    "876-543-2109",
//    "765-432-1098",
//    "654-321-0987",
//    "543-210-9876",
//    "432-109-8765",
//    "321-098-7654",
//    "210-987-6543",
//    "109-876-5432",
//    "098-765-4321",
//    "555-555-5555",
//    "666-666-6666",
//    "777-777-7777",
//    "888-888-8888",
//    "999-999-9999",
//    "111-111-1111",
//    "222-222-2222",
//    "333-333-3333",
//    "444-444-4444",
//    "012-345-6789",
//    "098-765-4321",
//    "987-654-3210",
//    "456-789-1230",
//    "789-123-4560",
//    "321-654-9870",
//    "654-987-3210",
//    "789-456-1230",
//    "258-369-7410",
//    "963-852-7410",
//    "147-258-3690",
//    "369-258-1470",
//    "258-147-3690"
//};
//            List<DateTime> fechas = new List<DateTime>
//{
//    new DateTime(2022, 01, 01),
//    new DateTime(2022, 02, 02),
//    new DateTime(2022, 03, 03),
//    new DateTime(2022, 04, 04),
//    new DateTime(2022, 05, 05),
//    new DateTime(2022, 06, 06),
//    new DateTime(2022, 07, 07),
//    new DateTime(2022, 08, 08),
//    new DateTime(2022, 09, 09),
//    new DateTime(2022, 10, 10),
//    new DateTime(2022, 11, 11),
//    new DateTime(2022, 12, 12),
//    new DateTime(2022, 01, 13),
//    new DateTime(2022, 02, 14),
//    new DateTime(2022, 03, 15),
//    new DateTime(2022, 04, 16),
//    new DateTime(2022, 05, 17),
//    new DateTime(2022, 06, 18),
//    new DateTime(2022, 07, 19),
//    new DateTime(2022, 08, 20),
//    new DateTime(2022, 09, 21),
//    new DateTime(2022, 10, 22),
//    new DateTime(2022, 11, 23),
//    new DateTime(2022, 12, 24),
//    new DateTime(2023, 01, 01),
//    new DateTime(2023, 02, 02),
//    new DateTime(2023, 03, 03),
//    new DateTime(2023, 04, 04),
//    new DateTime(2023, 05, 05),
//    new DateTime(2023, 06, 06),
//    new DateTime(2023, 07, 07),
//    new DateTime(2023, 08, 08),
//    new DateTime(2023, 09, 09),
//    new DateTime(2023, 10, 10),
//    new DateTime(2023, 11, 11),
//    new DateTime(2023, 12, 12),
//    new DateTime(2023, 01, 13),
//    new DateTime(2023, 02, 14),
//    new DateTime(2023, 03, 15),
//    new DateTime(2023, 04, 16),
//    new DateTime(2023, 05, 17),
//    new DateTime(2023, 06, 18),
//    new DateTime(2023, 07, 19),
//    new DateTime(2023, 08, 20),
//    new DateTime(2023, 09, 21),
//    new DateTime(2023, 10, 22),
//    new DateTime(2023, 11, 23),
//    new DateTime(2023, 12, 24)
//};
//            List<string> direcciones = new List<string>
//{
//    "Calle Falsa 123",
//    "Avenida de la Luz 567",
//    "Calle del Sol 890",
//    "Avenida de la Paz 234",
//    "Calle del Mar 567",
//    "Avenida del Río 890",
//    "Calle del Viento 123",
//    "Avenida del Bosque 456",
//    "Calle del Campo 789",
//    "Avenida del Jardín 123",
//    "Calle de la Montaña 456",
//    "Avenida del Océano 789",
//    "Calle del Cielo 123",
//    "Avenida del Pueblo 456",
//    "Calle del Puerto 789",
//    "Avenida del Mercado 123",
//    "Calle de la Luna 456",
//    "Avenida de la Estrella 789",
//    "Calle de la Primavera 123",
//    "Avenida del Verano 456",
//    "Calle del Otoño 789",
//    "Avenida del Invierno 123",
//    "Calle del Arco Iris 456",
//    "Avenida del Unicornio 789",
//    "Calle de la Fantasía 123",
//    "Avenida de la Magia 456",
//    "Calle del Misterio 789",
//    "Avenida del Encanto 123",
//    "Calle de la Ilusión 456",
//    "Avenida del Hechizo 789",
//    "Calle de la Imaginación 123",
//    "Avenida de los Sueños 456",
//    "Calle del Paraíso 789",
//    "Avenida de la Esperanza 123",
//    "Calle del Amor 456",
//    "Avenida de la Alegría 789",
//    "Calle del Corazón 123",
//    "Avenida de la Felicidad 456",
//    "Calle de la Vida 789",
//    "Avenida de la Libertad 123",
//    "Calle de la Verdad 456",
//    "Avenida de la Justicia 789",
//    "Calle del Honor 123",
//    "Avenida de la Lealtad 456",
//    "Calle del Respeto 789",
//    "Avenida de la Tolerancia 123",
//    "Calle del Diálogo 456",
//    "Avenida de la Convivencia 789",
//    "Calle de la Solidaridad 123",
//    "Avenida de la Fraternidad 456",
//    "Calle de la Paz 789"
//};
//            UsuarioTableAdapter adapter = new UsuarioTableAdapter();
//            for (int i = 0; i < nombresUsuarios.Count; i++)
//            {
//                adapter.Insert(nombresUsuarios[i], nombresUsuarios[i] + "@gmail.com", Contrasenas[i], nombres[i], apellidos[i], numeros[i], true, fechas[i],"Imagen " + i + ".com", direcciones[i]);
//            }





//        }
//        static void LlenadosDeEstado() 
//        {
//            List<string> estadosPedido = new List<string>
//{
//    "En proceso",
//    "En camino",
//    "Entregado"
//};
//            List<string> descripcionesEstadosPedido = new List<string>
//{
//    "El pedido se encuentra actualmente en proceso y está siendo preparado para su envío.",
//    "El pedido ha sido enviado y se encuentra en camino hacia su destino.",
//    "El pedido ha sido entregado y recibido por el cliente."
//};
//            EstadoTableAdapter adapter = new EstadoTableAdapter();
//            for (int i = 0; i < estadosPedido.Count; i++)
//            {
//                adapter.Insert(estadosPedido[i], descripcionesEstadosPedido[i]);
//            }
//        }
    }
}
