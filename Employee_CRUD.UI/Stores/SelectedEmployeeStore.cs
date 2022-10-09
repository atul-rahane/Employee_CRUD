using System;
using Employee_CRUD.Domain.Models;

namespace Employee_CRUD.UI.Stores
{
    public class SelectedEmployeeStore
    {
        private readonly EmployeesStore _employeesStore;

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                SelectedEmployeeChanged?.Invoke();
            }
        }

        public event Action SelectedEmployeeChanged;

        public SelectedEmployeeStore(EmployeesStore employeesStore)
        {
            _employeesStore = employeesStore;

            _employeesStore.EmployeeAdded += EmployeesStoreEmployeeAdded;
            _employeesStore.EmployeeUpdated += EmployeesStoreEmployeeUpdated;
            _employeesStore.EmployeeDeleted += EmployeesStoreEmployeeDeleted;
        }

        private void EmployeesStoreEmployeeAdded(Employee employee)
        {
            SelectedEmployee = employee;
        }

        private void EmployeesStoreEmployeeUpdated(Employee employee)
        {
            if(employee.Id == SelectedEmployee?.Id)
            {
                SelectedEmployee = employee;
            }
        }

        private void EmployeesStoreEmployeeDeleted(int id)
        {
            SelectedEmployee = null;
        }
    }
}
