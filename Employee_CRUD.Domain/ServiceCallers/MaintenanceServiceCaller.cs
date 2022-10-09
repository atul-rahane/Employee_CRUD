namespace Employee_CRUD.Domain.ServiceCallers
{
    public class MaintenanceServiceCaller<T> : IMaintenanceServiceCaller<T>
    {
        private readonly EmployeeCrudHttpClient _client;

        public MaintenanceServiceCaller(EmployeeCrudHttpClient client)
        {
            _client = client;
        }

        public virtual async Task<IEnumerable<T>?> GetAll()
        {
            return await _client.GetAsync<IEnumerable<T>>("");
        }

        public virtual async Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
