using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using StudentCore.Models;
using StudentMvc.Config;
using StudentWebAPI.Models;

namespace StudentMvc.Services
{
    public class HttpClientService
    {
        private IOptions<ApiConfig> _apiConfig;

        private readonly HttpClient _httpClient;

        public HttpClientService(IOptions<ApiConfig> apiConfig, IHttpClientFactory httpClientFactory)
        {
            _apiConfig = apiConfig;
            _httpClient = httpClientFactory.CreateClient("WebApiServer");

        }

        public async Task<Tout?> Post<TIn, Tout>(string uri, TIn postData) where Tout : class
        {
            var response = await _httpClient.PostAsJsonAsync(uri, postData);

            try
            {
                var model = await response.Content.ReadFromJsonAsync<Tout>();
                return model;
            }
            catch
            {
            }

            return default;
        }

        public async Task Post<TIn>(string uri, TIn postData)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, postData);
            //var model = await response.Content.ReadFromJsonAsync<Tout>();

            return;
        }

        public async Task Delete(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            //var model = await response.Content.ReadFromJsonAsync<Tout>();

            //return;
        }

        public async Task Put<TIn>(string uri, TIn putData)
        {
            var response = await _httpClient.PutAsJsonAsync(uri, putData);
            //var model = await response.Content.ReadFromJsonAsync<Tout>();

            //return;
        }

        public async Task<TOut> Get<TOut>(string uri)
        {
            var model = await _httpClient.GetFromJsonAsync<TOut>(uri);
            //var model = await response.Content.ReadFromJsonAsync<Tout>();

            return model;
        }
    }
}
