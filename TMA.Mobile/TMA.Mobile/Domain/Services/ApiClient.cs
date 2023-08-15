using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Services.Interfaces;

namespace TMA.Mobile.Domain.Services
{
    public class ApiClient
    {
        private HttpClient _httpClient;
        private readonly string _baseAddress = string.Empty;

        public ApiClient()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
           (message, cert, chain, errors) => { return true; };
            _httpClient = new HttpClient(httpClientHandler);
            _httpClient.BaseAddress = new Uri(_baseAddress);
        }

        public async Task<HttpResponseMessage> PostAsync(string token, string path, string json)
        {
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            if (token != string.Empty)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            request.Content = data;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response;

        }
    }
}
