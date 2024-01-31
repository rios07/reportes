using Newtonsoft.Json;
using System.Net;
using System.Text;
using Application.interfaces;
using Infraestructure;

namespace Application
{
    public class Application : IServiceHelper
    {
        private readonly WebClient client;
        private readonly IConfiguration _configuration;
        public Application(IConfiguration configuration)
        {
            client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = Encoding.UTF8;
            _configuration = configuration;
        }
        public T GetRequest<T>(string servicio, string metodo, string[] args, bool? ssl = null)
        {
            var protocol = (ssl.HasValue ? ssl.Value : false) ? "https" : "http";

            Uri webApiUrl = new Uri($"{protocol}://{servicio}/{metodo}");

            //QUERYSTRING
            string queryString = string.Empty;
            args.ToList().ForEach(s => queryString += $"/{s}");

            //REQUEST
            var response = client.DownloadString(webApiUrl + queryString);

            var resultado = JsonConvert.DeserializeObject<T>(response);

            return resultado;
        }

        public string GetStrRequest<T>(string servicio, string metodo, string[] args, bool? ssl = null)
        {
            throw new NotImplementedException();
        }

        public T PostRequest<T>(string servicio, string metodo, object sender, bool? ssl = null)
        {
            var protocol = (ssl.HasValue ? ssl.Value : false) ? "https" : "http";
            Uri webApiUrl = new Uri($"http://{servicio}/{metodo}");
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };

            // When you serialize, DateTime and DateTimeOffset values will be in this format.
            string inputJson = JsonConvert.SerializeObject(sender, settings);

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;

            var response = client.UploadString(webApiUrl, inputJson);

            var resultado = JsonConvert.DeserializeObject<T>(response);
            return resultado;
        }

        
        public async Task<T> Post<T>(Uri webApiUrl, object sender)
        {

            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };

            string inputJson = JsonConvert.SerializeObject(sender, settings);
            var data = new StringContent(inputJson, Encoding.UTF8, "application/json");
            HttpClient httpclient = new HttpClient();

            var response = await httpclient.PostAsync(webApiUrl, data);

            string result = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<T>(result);

            return resultado;
        }
    }
}