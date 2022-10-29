//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cognos.Library.Login
{
    public class LoginCognos
    {
        public LoginCognos()
        {

        }
        static LoginCognos()
        {

        }

        public static async Task<bool> Autenticar(string mode, string user, string password)
        {
            //string urlApiSeguridad = ConfigurationManager.AppSettings.Get("urlApiSeguridad"); //"http://localhost:8148";
            //string url = string.Format("{0}/api/user/getlogin?mode={1}&user={2}&password={3}", urlApiSeguridad, mode, user, password);
            //HttpClient client;
            //client = new HttpClient();

            //client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage responseMessage = await client.GetAsync(url);

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            //    var result = JsonConvert.DeserializeObject<Response.ResponseData<bool>>(responseData);
            //    return result.Result;
            //}
            return false;
        }
        public static async Task<LoginInfo> Autorizar(string user)
        {
            //string urlApiSeguridad = ConfigurationManager.AppSettings.Get("urlApiSeguridad");//"http://localhost:8148";
            //string url = string.Format("{0}/api/user/getpropiedades?user={1}", urlApiSeguridad, user);
            //HttpClient client;
            //client = new HttpClient();

            //client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage responseMessage = await client.GetAsync(url);

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            //    var result = JsonConvert.DeserializeObject<LoginInfo>(responseData);
            //    return result;
            //}
            return null;
        }
    }
}
