using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
//using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Cognos.Consola
{
    class Program
    {
        static string urlApi = "http://clientes.cognosys.com.bo/RedSaludService/";

        static void Main(string[] args)
        {
            var authTask = Autenticar("alianza", "alianza!");
            authTask.Wait();
            var auth = authTask.Result;
            InsertarAsegurado(auth.access_token);
            //ActualizarEstadoPoliza(auth.access_token);
            Console.WriteLine("done!");
            Console.ReadKey();
            // comentario
        }

        public static async Task<AuthenticationToken> Autenticar(string usr, string password)
        {
            string data = string.Format("grant_type=password&username={0}&password={1}", usr,password);
            HttpClient client;
            client = new HttpClient();

            client.BaseAddress = new Uri(urlApi);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMessage = client.PostAsync("Token", new StringContent(data, Encoding.UTF8)).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<AuthenticationToken>(responseData);
                Console.WriteLine(result.access_token);
                Console.WriteLine(result.clientes);
                return result;
            }
            return null;
        }

        public static async Task<dynamic> InsertarAsegurado(string token)
        {
            DateTime fechaNac = new DateTime(1986, 06, 29);
            DateTime fechaIni = new DateTime(2019, 01, 01);
            DateTime fechaFin = new DateTime(2019, 12, 31);
            Asegurado asegurado = new Asegurado()
            {
                CodigoCliente = "ALIANZA SEGUROS",
                NombreAsegurado = "Juan Perez",
                CI = "65112434",
                Genero = true, //Masculino False Femenino,
                FechaNacimiento = fechaNac,
                NumeroPoliza = "123456", //Nro Poliza
                RelacionDT = "T", //D dependiente, T titular
                FechaInicio = fechaIni,
                FechaFin = fechaFin,
                Ciudad = "CBB",//ALT,CBB,COB,LPZ,MON,ORU,PTS,SCR,STC,TRI,TRJ
                NombrePlan = "FULL SALUD",
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlApi);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = client.PostAsync("Api/RedSalud/InsertarAsegurado", new StringContent(
                new JavaScriptSerializer().Serialize(asegurado), Encoding.UTF8, "application/json")).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<string>(responseData);
                Console.WriteLine(result);
                return result;
            }
            return false;
        }

        public static async Task<dynamic> ActualizarEstadoPoliza(string token)
        {
            DateTime fechaIni = new DateTime(2019, 01, 31);
            DateTime fechaFin = new DateTime(2019, 12, 31);
            Poliza asegurado = new Poliza()
            {
                CodigoCliente = "ALIANZA SEGUROS",
                NumeroPoliza = "123456", //Nro Poliza
                Estado = "A",//A activo, I inactivo
                //FechaInicio = fechaIni,//pueden ir en nulo si no se quiere cambiar 
                //FechaFin = fechaFin,//pueden ir en nulo si no se quiere cambiar 
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlApi);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = client.PutAsync("Api/RedSalud/ActualizarEstadoPoliza", new StringContent(
                new JavaScriptSerializer().Serialize(asegurado), Encoding.UTF8, "application/json")).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<string>(responseData);
                Console.WriteLine(result);
                return result;
            }
            return false;
        }
    }
    public class AuthenticationToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        public string clientes { get; set; }
    }
    public class Asegurado
    {
        public string CodigoCliente { get; set; }
        public string NombreAsegurado { get; set; }
        public DateTime? FechaNacimiento { get; set; } //si se envia nulo sera igual a 01/01/1901
        public bool? Genero { get; set; } // nulo significara masculino
        public string RelacionDT { get; set; } //blanco o nulo significara T (Titular)
        public string CI { get; set; }
        public string Ciudad { get; set; }
        public string NumeroPoliza { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NombrePlan { get; set; }
}
    public class Poliza
    {
        public string CodigoCliente { get; set; }
        public string NumeroPoliza { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
