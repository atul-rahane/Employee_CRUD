using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employee_CRUD.Domain;
using Employee_CRUD.Domain.Models;

namespace Employee_CRUD.UI.Stores
{
    public class EmployeesStore
    {
        private readonly EmployeeCrudHttpClient _client;

        private readonly List<Employee> _employees;
        public IEnumerable<Employee> Employees => _employees;

        public event Action EmployeesLoaded;
        public event Action<Employee> EmployeeAdded;
        public event Action<Employee> EmployeeUpdated;
        public event Action<int> EmployeeDeleted;

        public EmployeesStore(EmployeeCrudHttpClient client)
        {
            _client = client;

            _employees = new List<Employee>();
        }

        public async Task Load()
        {
            IEnumerable<Employee> employees = await _client.GetAsync<IEnumerable<Employee>>("");

            _employees.Clear();
            _employees.AddRange(employees);

            EmployeesLoaded?.Invoke();
        }

        public async Task Add(Employee employee)
        {
            Employee? newEmployee = await _client.PostAsync<Employee>("", employee);
            employee.Id = newEmployee?.Id;
            _employees.Add(employee);
            EmployeeAdded?.Invoke(employee);
        }

        public async Task Update(Employee employee)
        {
            await _client.PutAsync<Employee>($"{employee.Id}", employee);

            int currentIndex = _employees.FindIndex(y => y.Id == employee.Id);

            if (currentIndex != -1)
            {
                _employees[currentIndex] = employee;
            }
            else
            {
                _employees.Add(employee);
            }

            EmployeeUpdated?.Invoke(employee);
        }

        public async Task Delete(int id)
        {
            bool isDeleted = await _client.DeleteAsync(id);
            if (isDeleted)
            {
                _employees.RemoveAll(y => y.Id == id);
                EmployeeDeleted?.Invoke(id);
            }
        }

        public async Task Search(string searchText)
        {
            string uri= $"?name={searchText}";
            IEnumerable<Employee> employees = await _client.GetAsync<IEnumerable<Employee>>(uri);

            _employees.Clear();
            _employees.AddRange(employees);

            EmployeesLoaded?.Invoke();
        }
    }
}
