using Employee_CRUD.Domain.Models;

namespace Employee_CRUD.Domain.HttpClients
{
    public interface ICrudHttpClient<T>
        where T : IKeyedObject
    {
        Task<T?> GetAsync<T>(string uri);
        Task<T?> PostAsync<T>(string uri, T payload);
        Task<T?> PutAsync<T>(string uri, T payload);
        Task<bool> DeleteAsync(int id);
    }
}
