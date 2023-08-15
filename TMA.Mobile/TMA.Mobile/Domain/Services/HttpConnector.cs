using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Services.Interfaces;

namespace TMA.Mobile.Domain.Services
{
    public class HttpConnector : HttpClient, IHttpConnector
    {
        protected static JsonSerializerOptions _camelCaseOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public HttpConnector() : base(CreateClientHandler())
        {

        }

        private static HttpMessageHandler CreateClientHandler()
        {
            var httpClientHandler = new HttpClientHandler();

            //allow connecting to untrusted certificates when running a DEBUG assembly
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };

            return httpClientHandler;
        }

        public async Task<T> GetApiResult<T>(string uri)
        {
            try
            {
                string response = await GetStringAsync(uri);
                return JsonSerializer.Deserialize<T>(response, _camelCaseOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
