using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;
using System.Text.Json;
using Practica2doParcial.Models;
using System.Security.Cryptography.X509Certificates;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using static System.Net.WebRequestMethods;
using System.Data.Common;

namespace Practica2doParcial
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (Models.telefoniaContext DB = new telefoniaContext())
            { 

            { 
                string opcion = "";

           

            do
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("** menu principal **" +
                    "\n 1. Mantenimiento Cliente" +
                    "\n 2. Mantenimiento Llamadas " +
                    "\n 3. Mantenimiento Planes"+
                    "\n 4. Mantenimiento Telefono" +
                    "\n 5.Exportar todas las tablas en CSV " +
                    "\n 6. Reporte Llamadas" +
                    "\n 7. Importar Telefono csv" +
                    "\n 8. Salir " +
                    "\n"
                    );

                opcion = Console.ReadLine();


                switch (opcion)
                {
                    case "1":
                     {
                            do
                            {
                                Console.Clear();

                                Console.WriteLine("\n");
                                Console.WriteLine("** Cliente **" +
                              "\n 1. Ver Clientes" +
                              "\n 2. Agregar Cliente" +
                              "\n 3. Eliminar Cliente" +
                              "\n 4. Editar Cliente" +
                              "\n 5. atras" +
                              "\n"
                              );
                                opcion = Console.ReadLine();
                                switch (opcion)
                                {
                                    case "1":
                                                try
                                                {
                                                    string url = "https://localhost:7177/api/Clientes";
                                                    HttpClient Client = new HttpClient();

                                                    var httpResponse = await Client.GetAsync(url);

                                                    if (httpResponse.IsSuccessStatusCode)
                                                    {
                                                        var content = await httpResponse.Content.ReadAsStringAsync();
                                                        var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                                                        List<Models.Cliente> clientes = System.Text.Json.JsonSerializer.Deserialize<List<Models.Cliente>>(content, jsonSerializerOptions);

                                                        Console.WriteLine($"IDCliente    Nombre        Apellido       fecha de nacimiento");

                                                        foreach (var X in clientes)

                                                            Console.WriteLine($" {X.IdCliente}         {X.Nombre}         {X.Apellido}          {X.FechaNacimiento}");


                                                        Console.ReadLine();
                                                    }
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Ha ocurrido un error,Revise la connecion con la api");
                                                    Console.Read();
                                                }


                                        break;

                                    case "2":
                                                
                                        string url1 = "https://localhost:7177/api/Clientes/Post";
                                        var Client1 = new HttpClient();
                                        var nombre = "";
                                        Console.WriteLine("escriba el nombre del cliente");
                                        nombre = Console.ReadLine();
                                        var apellido = "";
                                        Console.WriteLine("escriba el apellido del cliente");
                                        apellido = Console.ReadLine();
                                        var fechaNacimiento = "";
                                        Console.WriteLine("escriba la fecha de nacimiento de la siguiente forma yyy/mm/dd");
                                        fechaNacimiento = Console.ReadLine();
                                        Cliente cliente = new Cliente()
                                        {

                                            Nombre = nombre,
                                            Apellido = apellido,
                                            FechaNacimiento = fechaNacimiento


                                        };



                                                try
                                                {
                                                    var data1 = JsonSerializer.Serialize<Cliente>(cliente);
                                                    HttpContent content1 = new StringContent(data1, System.Text.Encoding.UTF8, "application/json");

                                                    var httpResponse1 = await Client1.PostAsync(url1, content1);

                                                    if (httpResponse1.IsSuccessStatusCode)
                                                    {
                                                        var result = await httpResponse1.Content.ReadAsStringAsync();
                                                        var postResult = JsonSerializer.Deserialize<Post>(result);
                                                        Console.WriteLine("Cliente agregado");
                                                        Console.ReadLine();
                                                    }
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Ha ocurrido un error");
                                                    Console.Read();
                                                }

                                        break;
                                    case "3":


                                        Console.WriteLine("Escriba el id del cliente que quiere eliminar");
                                                try
                                                {
                                                    var id = Console.ReadLine();

                                                    string url2 = $"https://localhost:7177/api/Clientes?id={id}";
                                                    var Client2 = new HttpClient();





                                                    ///var data2 = JsonSerializer.Serialize<Cliente>(cliente2);
                                                    //HttpContent content2 = new StringContent(data2, System.Text.Encoding.UTF8, "application/json");

                                                    var httpResponse2 = await Client2.DeleteAsync(url2);

                                                    if (httpResponse2.IsSuccessStatusCode)
                                                    {
                                                        var result = await httpResponse2.Content.ReadAsStringAsync();
                                                        // var postResult = JsonSerializer.Deserialize<Post>(result);
                                                        Console.WriteLine("Cliente eliminado");
                                                        Console.ReadLine();
                                                    }
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Ha ocurrido un error");
                                                    Console.Read();
                                                }

                                        break;


                                    case "4":
                                        string url4 = "https://localhost:7177/api/Clientes/put";
                                        var Client4 = new HttpClient();
                                        var numero = "";
                                        Console.WriteLine("escriba el id del cliente");
                                        numero = Console.ReadLine();
                                        int id1 = Int32.Parse(numero);
                                        var nombre4 = "";
                                        Console.WriteLine("escriba el telefono del cliente");
                                        nombre = Console.ReadLine();
                                        var apellido4 = "";
                                        Console.WriteLine("escriba el apellido del cliente");
                                        apellido = Console.ReadLine();
                                        var fechaNacimiento4 = "";
                                        Console.WriteLine("escriba la fecha de nacimiento de la siguiente forma yyy/mm/dd");
                                        fechaNacimiento = Console.ReadLine();
                                        Cliente cliente4 = new Cliente()
                                        {
                                            IdCliente = id1,
                                            Nombre = nombre,
                                            Apellido = apellido,
                                            FechaNacimiento = fechaNacimiento


                                        };



                                                try
                                                {
                                                    var data4 = JsonSerializer.Serialize<Cliente>(cliente4);
                                                    HttpContent content4 = new StringContent(data4, System.Text.Encoding.UTF8, "application/json");

                                                    var httpResponse4 = await Client4.PutAsync(url4, content4);

                                                    if (httpResponse4.IsSuccessStatusCode)
                                                    {
                                                        var result = await httpResponse4.Content.ReadAsStringAsync();
                                                        var postResult = JsonSerializer.Deserialize<Post>(result);
                                                        Console.WriteLine("Cliente Editado");
                                                        Console.ReadLine();
                                                    }

                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Ha ocurrido un error");
                                                    Console.Read();
                                                }

                                        break;




                                    case "5":




                                        opcion = "5";
                                        break;


                                }





                            } while (opcion != "5");
                          

                        }

                        break;

                    case "2":

                        do
                        {
                            Console.Clear();

                            Console.WriteLine("\n");
                            Console.WriteLine("** Llamadas **" +
                          "\n 1. Ver Llamadas" +
                          "\n 2. Agregar Llamada" +
                          "\n 3. Eliminar Llamada" +
                          "\n 4. Editar Llamada" +
                          "\n 5. atras" +
                          "\n"
                          );
                            opcion = Console.ReadLine();
                            switch (opcion)
                            {
                                case "1":
                                            try
                                            {
                                                string url = "https://localhost:7177/api/Llamadas";
                                                HttpClient Client = new HttpClient();

                                                var httpResponse = await Client.GetAsync(url);

                                                if (httpResponse.IsSuccessStatusCode)
                                                {
                                                    var content = await httpResponse.Content.ReadAsStringAsync();
                                                    var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                                                    List<Models.Llamada> clientes = System.Text.Json.JsonSerializer.Deserialize<List<Models.Llamada>>(content, jsonSerializerOptions);

                                                    Console.WriteLine($"Cod         Telefono                Fecha                Duracion");

                                                    foreach (var X in clientes)

                                                        Console.WriteLine($" {X.CodLlamada}         {X.Telefono}         {X.Fecha}          {X.Duracion} Min");


                                                    Console.ReadLine();
                                                }
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error,Revise la connecion con la api");
                                                Console.Read();
                                            }


                                            break;

                                case "2":
                                    string url1 = "https://localhost:7177/api/Llamadas/Post";
                                    var Client1 = new HttpClient();
                                    var telefono = "";
                                    Console.WriteLine("escriba el # de telefono");
                                    telefono = Console.ReadLine();
                                    var duracion = "";
                                    var fecha = "";
                                    Console.WriteLine("escriba la fecha de la llamada de la sigiente forma  May 24 2019 12:00AM");
                                    fecha = Console.ReadLine();
                                    Console.WriteLine("escriba la duracion de la llamada en minutos");
                                    duracion = Console.ReadLine();
                                    int du = Int32.Parse(duracion);
                                    Llamada llamada = new Llamada()
                                    {
                                        
                                        Telefono = telefono,
                                        Fecha = fecha,
                                        Duracion = du


                                    };


                                            try
                                            {

                                                var data1 = JsonSerializer.Serialize<Llamada>(llamada);
                                                HttpContent content1 = new StringContent(data1, System.Text.Encoding.UTF8, "application/json");

                                                var httpResponse1 = await Client1.PostAsync(url1, content1);

                                                if (httpResponse1.IsSuccessStatusCode)
                                                {
                                                    var result = await httpResponse1.Content.ReadAsStringAsync();
                                                    var postResult = JsonSerializer.Deserialize<Post>(result);
                                                    Console.WriteLine("Llamada agregada");
                                                    Console.ReadLine();
                                                }
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error");
                                                Console.Read();
                                            }

                                    break;
                                case "3":


                                    Console.WriteLine("Escriba el codLlamada del cliente que quiere eliminar");
                                            try
                                            {
                                                var id = Console.ReadLine();

                                                string url2 = $"https://localhost:7177/api/Llamadas?cod={id}";
                                                var Client2 = new HttpClient();




                                                var httpResponse2 = await Client2.DeleteAsync(url2);

                                                if (httpResponse2.IsSuccessStatusCode)
                                                {
                                                    var result = await httpResponse2.Content.ReadAsStringAsync();
                                                    Console.WriteLine("Llamada eliminada");
                                                    Console.ReadLine();
                                                }
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error");
                                                Console.Read();
                                            }

                                    break;


                                case "4":
                                    string url4 = "https://localhost:7177/api/Llamadas/put";
                                    var Client4 = new HttpClient();
                                    var numero = "";
                                    var numero1 = "";


                                    Console.WriteLine("escriba el codLLamada");
                                    numero = Console.ReadLine();
                                    int id1 = Int32.Parse(numero);
                                  
                                    Console.WriteLine("escriba el telefono");
                                   var telefono1 = Console.ReadLine();
                                 
                                    Console.WriteLine("escriba la fecha de la llamada");
                                   var apellido = Console.ReadLine();
                                
                  

                                    Console.WriteLine("escriba la duracion de la llamada");
                                    numero1 = Console.ReadLine();
                                    int id2 = Int32.Parse(numero1);

                                    Llamada llamada2 = new Llamada()
                                    {
                                        CodLlamada = id1,
                                        Telefono = telefono1,
                                        Fecha = apellido,
                                        Duracion = id2


                                    };


                                            try
                                            {

                                                var data4 = JsonSerializer.Serialize<Llamada>(llamada2);
                                                HttpContent content4 = new StringContent(data4, System.Text.Encoding.UTF8, "application/json");

                                                var httpResponse4 = await Client4.PutAsync(url4, content4);

                                                if (httpResponse4.IsSuccessStatusCode)
                                                {
                                                    var result = await httpResponse4.Content.ReadAsStringAsync();
                                                    var postResult = JsonSerializer.Deserialize<Post>(result);
                                                    Console.WriteLine("Cliente Editado");
                                                    Console.ReadLine();
                                                }
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error");
                                                Console.Read();
                                            }


                                    break;


                                    break;




                                case "5":
                                    opcion = "5";
                                    break;


                            }





                        } while (opcion != "5");

                        break;

                    case "3":
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("\n");
                            Console.WriteLine("** Planes **" +
                          "\n 1. Ver Planes" +
                          "\n 2. Agregar Plan" +
                          "\n 3. Eliminar Plan" +
                          "\n 4. Editar Plan" +
                          "\n 5. atras" +
                          "\n"
                          );
                            opcion = Console.ReadLine();
                            switch (opcion)
                            { 

                                case "1":
                                            try { 
                                    string url = "https://localhost:7177/api/Planes";
                                    HttpClient Client = new HttpClient();

                                    var httpResponse = await Client.GetAsync(url);

                                    if (httpResponse.IsSuccessStatusCode)
                                    {
                                        var content = await httpResponse.Content.ReadAsStringAsync();
                                        var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                                        List<Models.Plane> clientes = System.Text.Json.JsonSerializer.Deserialize<List<Models.Plane>>(content, jsonSerializerOptions);

                                        Console.WriteLine($"ID         Descripcion                Renta                CostoMin");

                                        foreach (var X in clientes)

                                            Console.WriteLine($" {X.IdPlan}         {X.Descripcion}                   {X.Renta}                {X.CostoMin}");


                                        Console.ReadLine();
                                    }

                                            }catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error,Revise la connecion con la api");
                                                Console.Read();
                                            }

                                            break;

                                case "2":
                                            try { 
                                    string url1 = "https://localhost:7177/api/Planes/Post";
                                    var Client1 = new HttpClient();
                                    var IdPlan = "";
                                    Console.WriteLine("escriba id del plan");
                                    IdPlan = Console.ReadLine();
                                    var Renta = "";
                                    var costo = "";
                                    var descripcion = "";
                                    Console.WriteLine("escriba el telefono del plan");
                                    descripcion = Console.ReadLine();
                                    Console.WriteLine("escriba el precio de renta");
                                    Renta = Console.ReadLine();
                                    int re = Int32.Parse(Renta);
                                    Console.WriteLine("escriba el costo por minuto");
                                    costo = Console.ReadLine();
                                    int cos = Int32.Parse(costo);
                                            
                                    Plane planes = new Plane()
                                    {

                                        IdPlan = IdPlan,
                                        Descripcion = descripcion,
                                        Renta = re,
                                        CostoMin = cos


                                    };
                                            



                                    var data1 = JsonSerializer.Serialize<Plane>(planes);
                                    HttpContent content1 = new StringContent(data1, System.Text.Encoding.UTF8, "application/json");

                                    var httpResponse1 = await Client1.PostAsync(url1, content1);

                                    if (httpResponse1.IsSuccessStatusCode)
                                    {
                                        var result = await httpResponse1.Content.ReadAsStringAsync();
                                        var postResult = JsonSerializer.Deserialize<Post>(result);
                                        Console.WriteLine("Plan agregada");
                                        Console.ReadLine();
                                    }
                                            } catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error");
                                                Console.Read();
                                            }

                                    break;
                                case "3":


                                    Console.WriteLine("Escriba el id del Plan que quiere eliminar");
                                            try { 
                                    var id = Console.ReadLine();

                                    string url2 = $"https://localhost:7177/api/Planes?id={id}";
                                    var Client2 = new HttpClient();





                                    ///var data2 = JsonSerializer.Serialize<Cliente>(cliente2);
                                    //HttpContent content2 = new StringContent(data2, System.Text.Encoding.UTF8, "application/json");

                                    var httpResponse2 = await Client2.DeleteAsync(url2);

                                    if (httpResponse2.IsSuccessStatusCode)
                                    {
                                        var result = await httpResponse2.Content.ReadAsStringAsync();
                                        // var postResult = JsonSerializer.Deserialize<Post>(result);
                                        Console.WriteLine("Plan eliminado");
                                        Console.ReadLine();
                                    }
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error, puede que el id plan no exista");
                                                Console.Read();

                                            }

                                    break;


                                case "4":

                                            try
                                            {
                                                string url4 = "https://localhost:7177/api/Planes/put";
                                                var Client4 = new HttpClient();
                                                var numero = "";
                                                var numero1 = "";

                                                Console.WriteLine("escriba el id del plan");
                                                var plan = Console.ReadLine();

                                                Console.WriteLine("escriba el nombre del plan");
                                                var descripcion1 = Console.ReadLine();


                                                Console.WriteLine("escriba el precio de renta");
                                                numero = Console.ReadLine();
                                                int id1 = Int32.Parse(numero);

                                                Console.WriteLine("escriba el costo por minuto");
                                                numero1 = Console.ReadLine();
                                                int id2 = Int32.Parse(numero1);


                                                Plane plane = new Plane()
                                                {
                                                    IdPlan = plan,
                                                    Descripcion = descripcion1,
                                                    Renta = id1,
                                                    CostoMin = id2


                                                };




                                                var data4 = JsonSerializer.Serialize<Plane>(plane);
                                                HttpContent content4 = new StringContent(data4, System.Text.Encoding.UTF8, "application/json");

                                                var httpResponse4 = await Client4.PutAsync(url4, content4);

                                                if (httpResponse4.IsSuccessStatusCode)
                                                {
                                                    var result = await httpResponse4.Content.ReadAsStringAsync();
                                                    var postResult = JsonSerializer.Deserialize<Post>(result);
                                                    Console.WriteLine("Plan Editado");
                                                    Console.ReadLine();
                                                }

                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error");
                                                Console.Read();
                                            }

                                    break;




                                case "5":
                                    opcion = "5";
                                    break;


                            }





                        } while (opcion != "5");


                        break;


                    case "4":
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("\n");
                            Console.WriteLine("** Telefonos **" +
                          "\n 1. Ver Telefonos" +
                          "\n 2. Agregar Telefono" +
                          "\n 3. Eliminar Telefono" +
                          "\n 4. Editar Telefono" +
                          "\n 5. atras" +
                          "\n"
                          );
                            opcion = Console.ReadLine();
                            switch (opcion)
                            {
                                case "1": 
                                            try { 
                                    string url = "https://localhost:7177/api/Telefonoes";
                                    HttpClient Client = new HttpClient();

                                    var httpResponse = await Client.GetAsync(url);

                                    if (httpResponse.IsSuccessStatusCode)
                                    {
                                        var content = await httpResponse.Content.ReadAsStringAsync();
                                        var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                                        List<Models.Telefono> clientes = System.Text.Json.JsonSerializer.Deserialize<List<Models.Telefono>>(content, jsonSerializerOptions);

                                        Console.WriteLine($"IdCliente         Telefono               Tipo de Plan");

                                        foreach (var X in clientes)

                                            Console.WriteLine($" {X.IdCliente}             {X.Telefono1}                   {X.TipoPlan}");


                                        Console.ReadLine();
                                    }

                                            } catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error");
                                                Console.Read();
                                            }

                                    break;

                                case "2":
                                            try
                                            {
                                                string url1 = "https://localhost:7177/api/Telefonoes/Post";
                                                var Client1 = new HttpClient();
                                                var idCliente = "";
                                                Console.WriteLine("escriba id del Cliente");
                                                idCliente = Console.ReadLine();
                                                var Telefono = "";
                                                Console.WriteLine("escriba el # de telefono que desea agragar");
                                                Telefono = Console.ReadLine();
                                                var tipoPlan = "";
                                                Console.WriteLine("escriba el tipo del plan");
                                                tipoPlan = Console.ReadLine();
                                                int cos = Int32.Parse(idCliente);

                                                Telefono telefono = new Telefono()
                                                {

                                                    Telefono1 = Telefono,
                                                    IdCliente = cos,
                                                    TipoPlan = tipoPlan



                                                };




                                                var data1 = JsonSerializer.Serialize<Telefono>(telefono);
                                                HttpContent content1 = new StringContent(data1, System.Text.Encoding.UTF8, "application/json");

                                                try
                                                {
                                                    var httpResponse1 = await Client1.PostAsync(url1, content1);

                                                    ;
                                                    if (httpResponse1.IsSuccessStatusCode)
                                                    {
                                                        var result = await httpResponse1.Content.ReadAsStringAsync();
                                                        var postResult = JsonSerializer.Deserialize<Post>(result);
                                                        Console.WriteLine("Telefono agregado");
                                                        Console.ReadLine();
                                                    }
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("error detectado");
                                                    Console.Read();
                                                };
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error");
                                                Console.Read();
                                            }

                                    break;
                                case "3":


                                    Console.WriteLine("Escriba el # de telefono que desea eliminar");
                                            try
                                            {
                                                var id = Console.ReadLine();

                                                string url2 = $"https://localhost:7177/api/Telefonoes?numero={id}";
                                                var Client2 = new HttpClient();





                                                ///var data2 = JsonSerializer.Serialize<Cliente>(cliente2);
                                                //HttpContent content2 = new StringContent(data2, System.Text.Encoding.UTF8, "application/json");

                                                var httpResponse2 = await Client2.DeleteAsync(url2);

                                                if (httpResponse2.IsSuccessStatusCode)
                                                {
                                                    var result = await httpResponse2.Content.ReadAsStringAsync();
                                                    // var postResult = JsonSerializer.Deserialize<Post>(result);
                                                    Console.WriteLine("Telefono eliminado");
                                                    Console.ReadLine();
                                                }

                                            } catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error");
                                                Console.Read();
                                            }
                                    break;


                                case "4":
                                            try
                                            {
                                                string url4 = "https://localhost:7177/api/Telefonoes/put";
                                                var Client4 = new HttpClient();
                                                var numero = "";



                                                Console.WriteLine("escriba el id del cliente");
                                                numero = Console.ReadLine();
                                                int id1 = Int32.Parse(numero);

                                                Console.WriteLine("escriba el # de telefono");
                                                var telefono2 = Console.ReadLine();


                                                Console.WriteLine("escriba tipo de plan");
                                                var tipoplan1 = Console.ReadLine();


                                                Telefono telefono1 = new Telefono()
                                                {
                                                    Telefono1 = telefono2,
                                                    IdCliente = id1,
                                                    TipoPlan = tipoplan1,

                                                };




                                                var data4 = JsonSerializer.Serialize<Telefono>(telefono1);
                                                HttpContent content4 = new StringContent(data4, System.Text.Encoding.UTF8, "application/json");

                                                var httpResponse4 = await Client4.PutAsync(url4, content4);

                                                if (httpResponse4.IsSuccessStatusCode)
                                                {
                                                    var result = await httpResponse4.Content.ReadAsStringAsync();
                                                    var postResult = JsonSerializer.Deserialize<Post>(result);
                                                    Console.WriteLine("Telefono Editado");
                                                    Console.ReadLine();
                                                }


                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ha ocurrido un error");
                                                Console.Read();
                                            }
                                    break;




                                case "5":
                                    opcion = "5";
                                    break;


                            }





                        } while (opcion != "5");




                        break;


                    case "5":
                                do
                                {
                                    Console.Clear();

                                    Console.WriteLine("\n");
                                    Console.WriteLine("** Cliente **" +
                                  "\n 1. Crear ClientesCSV" +
                                  "\n 2. Crear LlamadasCSV" +
                                  "\n 3. Crear PlanesCSV" +
                                  "\n 4. Crear TelefonoCSV" +
                                  "\n 5. atras" +
                                  "\n"
                                  );
                                    opcion = Console.ReadLine();
                                    switch (opcion)
                                    {
                                        case "1":

                                            GetClienteCSV();
                                            Console.WriteLine("Archivo Creado");
                                            Console.Read();
                                            break;

                                        case "2":

                                            GetLlamada();
                                            Console.WriteLine("Archivo Creado");
                                            Console.Read();
                                            break;

                                        case "3":

                                            GetPlanes();
                                            Console.WriteLine("Archivo Creado");
                                            Console.Read();


                                            break;


                                        case "4":

                                            GetTelefonos();
                                            Console.WriteLine("Archivo Creado");
                                            Console.Read();

                                            break;




                                        case "5":
                                            opcion = "5";
                                            break;


                                    }





                                } while (opcion != "5");
                                break;




                    case "6":

                                 Console.WriteLine("escriba el id del cliente");
                                try
                                {
                                    var id12 = Console.ReadLine();
                                    int id11 = Int32.Parse(id12);





                                    var a = from t in DB.Telefono
                                            from l in DB.Llamadas
                                            where t.Telefono1 == l.Telefono
                                            where t.IdCliente == id11

                                            select new
                                            {
                                                l.CodLlamada,
                                                l.Telefono,
                                                l.Fecha,
                                                l.Duracion
                                            };

                                    Console.WriteLine("\n");
                                    Console.WriteLine("CodLlamada          Telefono               Fecha              Duracion ");
                                    foreach (var x in a)
                                    {
                                       
                                            Console.WriteLine($"    {x.CodLlamada}            {x.Telefono}         {x.Fecha}         {x.Duracion}Min ");

                                    }
                                    Console.Read();
                                }
                                catch
                                {
                                    Console.WriteLine("Ha ocurrido un error");
                                    Console.Read();

                                }
                                break;


                            case "7":

                                var lineNumber = 0;
                                using (SqlConnection conn = new SqlConnection(@"Server=DESKTOP-448GVJA;Database=telefonia; Integrated Security=true; Pooling = False"))
                                {
                                    conn.Open();
                                   
                                    using (StreamReader reader = new StreamReader(@"C:\Users\joanj\Desktop\Telefono.csv"))
                                    {
                                        while (!reader.EndOfStream)
                                        {
                                            var line = reader.ReadLine();
                                            if (lineNumber != 0)
                                            {
                                                var values = line.Split(';');

                                                var sql = "INSERT INTO Telefonia.dbo.Telefono VALUES ('" + values[0] + "','" + values[1] + "'," + values[2] + ")";

                                                var cmd = new SqlCommand();
                                                cmd.CommandText = sql;
                                                cmd.CommandType = System.Data.CommandType.Text;
                                                cmd.Connection = conn;
                                                cmd.ExecuteNonQuery();
                                            }
                                            lineNumber++;
                                        }
                                    }
                                    conn.Close();
                                }
                                Console.WriteLine("Telefono importado");
                                Console.ReadLine();


                                break;


                            case "8":
                        opcion = "8";
                        break;

                    default:
                        Console.WriteLine("elige una opcion del menu");
                        break;
                }

            } while (opcion != "8");






                }
            }





        }



        private static string GetClienteCSV()
        {
            using (SqlConnection cn = new SqlConnection(GetConnectionsString()))
            {
                cn.Open();
                return CreateClienteCSV(new SqlCommand("select * from Cliente",cn).ExecuteReader());
            }
        }

        
        private static string CreateClienteCSV(IDataReader reader)
        {
            string file = @"C:\Users\joanj\Desktop\\tablaCliente.csv";
            List<string> lines = new List<string>();


            string headerLines = "";
            if (reader.Read())
            {
                string[] columns = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns[i] = reader.GetName(i);
                }
                headerLines = string.Join(",", columns);
                lines.Add(headerLines);
            }

            // data 

            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines.Add(string.Join(",", values));
            }

            // create file
            System.IO.File.WriteAllLines(file, lines);



            return file;
        }


        private static string GetLlamada()
        {
            using (SqlConnection cn = new SqlConnection(GetConnectionsString()))
            {
                cn.Open();
                return CreateLlamadaCSV(new SqlCommand("select * from Llamadas", cn).ExecuteReader());
            }
        }


        private static string CreateLlamadaCSV(IDataReader reader)
        {
            string file = @"C:\Users\joanj\Desktop\\tablaLlamada.csv";
            List<string> lines = new List<string>();


            string headerLines = "";
            if (reader.Read())
            {
                string[] columns = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns[i] = reader.GetName(i);
                }
                headerLines = string.Join(",", columns);
                lines.Add(headerLines);
            }

            // data 

            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines.Add(string.Join(",", values));
            }

            // create file
            System.IO.File.WriteAllLines(file, lines);



            return file;
        }


        private static string GetPlanes()
        {
            using (SqlConnection cn = new SqlConnection(GetConnectionsString()))
            {
                cn.Open();
                return CreatePlanesCSV(new SqlCommand("select * from Planes", cn).ExecuteReader());
            }
        }


        private static string CreatePlanesCSV(IDataReader reader)
        {
            string file = @"C:\Users\joanj\Desktop\\tablaPlanes.csv";
            List<string> lines = new List<string>();


            string headerLines = "";
            if (reader.Read())
            {
                string[] columns = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns[i] = reader.GetName(i);
                }
                headerLines = string.Join(",", columns);
                lines.Add(headerLines);
            }

            // data 

            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines.Add(string.Join(",", values));
            }

            // create file
            System.IO.File.WriteAllLines(file, lines);



            return file;
        }

        private static string GetTelefonos()
        {
            using (SqlConnection cn = new SqlConnection(GetConnectionsString()))
            {
                cn.Open();
                return CreateTelefonosCSV(new SqlCommand("select * from Telefono", cn).ExecuteReader());
            }
        }


        private static string CreateTelefonosCSV(IDataReader reader)
        {
            string file = @"C:\Users\joanj\Desktop\\tablaTelefonos.csv";
            List<string> lines = new List<string>();


            string headerLines = "";
            if (reader.Read())
            {
                string[] columns = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns[i] = reader.GetName(i);
                }
                headerLines = string.Join(",", columns);
                lines.Add(headerLines);
            }

            // data 

            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines.Add(string.Join(",", values));
            }

            // create file
            System.IO.File.WriteAllLines(file, lines);



            return file;
        }















        private static string GetConnectionsString()
        {
            return @"Server=DESKTOP-448GVJA;Database=telefonia; Integrated Security=true; Pooling = False";

         


        }



    }








}
