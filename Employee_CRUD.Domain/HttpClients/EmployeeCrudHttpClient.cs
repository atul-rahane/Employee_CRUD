using System.Net.Http.Headers;
using Employee_CRUD.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Employee_CRUD.Domain.HttpClients
{
    public class EmployeeCrudHttpClient: CrudHttpClient<Employee>
    {
        public EmployeeCrudHttpClient(HttpClient client, CRUDAPIKey apiKey)
        : base(client, apiKey)
        {
        }
    }
}
