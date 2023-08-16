using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TMA.Mobile.Domain.Services
{
    public class ApiClient
    {
        private HttpClient _httpClient;
        private readonly string _baseAddress = Constants.BaseUrl;

        public ApiClient()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
           (message, cert, chain, errors) => { return true; };
            _httpClient = new HttpClient(httpClientHandler);     
        }

        public async Task<HttpResponseMessage> GetAsync(string token, string path)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path);
            if (token != string.Empty)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string token, string path, string json)
        {
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var fullPath = _baseAddress + path;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, fullPath);
            if (token != string.Empty)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            request.Content = data;
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response;

        }

        public async Task<HttpResponseMessage> DeleteAsync(string token, string path)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, path);
            if (token != string.Empty)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response;

        }
        public async Task<HttpResponseMessage> UpdateAsync(string token, string path, string json)
        {
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, path);
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
