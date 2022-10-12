using System.Net.Http.Headers;
using Employee_CRUD.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Employee_CRUD.Domain.HttpClients
{
    public abstract class CrudHttpClient<T> : ICrudHttpClient<T>
    where T : IKeyedObject
    {
        private readonly HttpClient _client;

        public CrudHttpClient(HttpClient client, CRUDAPIKey apiKey)
        {
            _client = client;
            
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey.Key);
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
