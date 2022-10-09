using System;
using System.Threading.Tasks;
using Employee_CRUD.UI.Stores;
using Employee_CRUD.UI.ViewModels;

namespace Employee_CRUD.UI.Commands
{
    public class LoadEmployeesCommand : AsyncCommandBase
    {
        private readonly EmployeesViewModel _employeesViewModel;
        private readonly EmployeesStore _employeesStore;

        public LoadEmployeesCommand(EmployeesViewModel employeesViewModel, EmployeesStore employeesStore)
        {
            _employeesViewModel = employeesViewModel;
            _employeesStore = employeesStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _employeesViewModel.ErrorMessage = null;
            _employeesViewModel.IsLoading = true;

            try
            {
                await _employeesStore.Load();
            }
            catch (Exception)
            {
                _employeesViewModel.ErrorMessage = "Failed to load employees. Please restart the application.";
            }
            finally
            {
                _employeesViewModel.IsLoading = false;
            }
        }
    }
}
