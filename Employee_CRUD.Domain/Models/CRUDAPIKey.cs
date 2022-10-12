namespace Employee_CRUD.Domain.Models
{
    public class CRUDAPIKey
    {
        public string Key { get; }

        public CRUDAPIKey(string key)
        {
            Key = key;
        }
    }
}
