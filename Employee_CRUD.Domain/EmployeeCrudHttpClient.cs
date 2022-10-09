using System.Net.Http.Headers;
using System.Net.Http.Json;
using Employee_CRUD.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Employee_CRUD.Domain
{
    public class EmployeeCrudHttpClient
    {

        private readonly HttpClient _client;
        private readonly string _apiKey;

        public EmployeeCrudHttpClient(HttpClient client, EmployeeCRUDAPIKey apiKey)
        {
            _client = client;
            _apiKey = apiKey.Key;

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<T?> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        public async Task<T?> PostAsync<T>(string uri, T payload)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            using (var content = new StringContent(JsonConvert.SerializeObject(payload, serializerSettings), System.Text.Encoding.UTF8,
                       "application/json"))
            {
                HttpResponseMessage response = await _client.PostAsync(uri, content);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(jsonResponse);
            }
        }

        public async Task<T?> PutAsync<T>(string uri, T payload)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            using (var content = new StringContent(JsonConvert.SerializeObject(payload, serializerSettings), System.Text.Encoding.UTF8,
                       "application/json"))
            {
                HttpResponseMessage response = await _client.PutAsync(uri, content);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(jsonResponse);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string uri = $"{id}";
            HttpResponseMessage response = await _client.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }
    }
}
