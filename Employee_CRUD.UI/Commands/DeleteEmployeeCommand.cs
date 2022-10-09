using System;
using System.Threading.Tasks;
using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Stores;
using Employee_CRUD.UI.ViewModels;

namespace Employee_CRUD.UI.Commands
{
    public class DeleteEmployeeCommand : AsyncCommandBase
    {
        private readonly EmployeesListingItemViewModel _employeesListingItemViewModel;
        private readonly EmployeesStore _employeesStore;

        public DeleteEmployeeCommand(EmployeesListingItemViewModel employeesListingItemViewModel, EmployeesStore employeesStore)
        {
            _employeesListingItemViewModel = employeesListingItemViewModel;
            _employeesStore = employeesStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _employeesListingItemViewModel.ErrorMessage = null;
            _employeesListingItemViewModel.IsDeleting = true;

            Employee employee = _employeesListingItemViewModel.Employee;

            try
            {
                await _employeesStore.Delete(employee.Id ?? 0);
            }
            catch (Exception)
            {
                _employeesListingItemViewModel.ErrorMessage = "Failed to delete employee. Please try again later.";
            }
            finally
            {
                _employeesListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
