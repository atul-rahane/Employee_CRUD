namespace Employee_CRUD.Domain.Services
{
    public interface IMajorIndexService
    {
        Task<IEnumerable<T>?> GetAll();

        Task<T> Get(int id);

        Task<T> Create(T entity);

        Task<T> Update(int id, T entity);

        Task<bool> Delete(int id);

        Task<IEnumerable<T>> Search(string searchText);
    }
}
