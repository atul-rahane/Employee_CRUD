using System;
using System.Threading.Tasks;
using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Stores;
using Employee_CRUD.UI.ViewModels;

namespace Employee_CRUD.UI.Commands
{
    public class EditEmployeeCommand : AsyncCommandBase
    {
        private readonly EditEmployeeViewModel _editEmployeeViewModel;
        private readonly EmployeesStore _employeesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditEmployeeCommand(EditEmployeeViewModel editEmployeeViewModel, EmployeesStore employeesStore, ModalNavigationStore modalNavigationStore)
        {
            _editEmployeeViewModel = editEmployeeViewModel;
            _employeesStore = employeesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            EmployeeDetailsFormViewModel formViewModel = _editEmployeeViewModel.EmployeeDetailsFormViewModel;

            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            Employee employee = new Employee(
                _editEmployeeViewModel.EmployeeId,
                formViewModel.Name,
                formViewModel.Email,
                formViewModel.Gender,
                formViewModel.Status);

            try
            {
                await _employeesStore.Update(employee);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to update employee. Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
