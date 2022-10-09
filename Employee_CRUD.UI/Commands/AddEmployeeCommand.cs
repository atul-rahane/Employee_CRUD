using System;
using System.Threading.Tasks;
using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Stores;
using Employee_CRUD.UI.ViewModels;

namespace Employee_CRUD.UI.Commands
{
    public class AddEmployeeCommand : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel;
        private readonly EmployeesStore _employeesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel, EmployeesStore employeesStore, ModalNavigationStore modalNavigationStore)
        {
            _addEmployeeViewModel = addEmployeeViewModel;
            _employeesStore = employeesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            EmployeeDetailsFormViewModel formViewModel = _addEmployeeViewModel.EmployeeDetailsFormViewModel;

            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            Employee employee = new Employee(
                null,
                formViewModel.Name,
                formViewModel.Email,
                formViewModel.Gender,
                formViewModel.Status);

            try
            {
                await _employeesStore.Add(employee);

                _modalNavigationStore.Close();
            }
            catch (Exception exception)
            {
                formViewModel.ErrorMessage = "Failed to add employee. Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
