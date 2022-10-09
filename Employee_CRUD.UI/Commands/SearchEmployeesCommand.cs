using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Vml;
using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Stores;
using Employee_CRUD.UI.ViewModels;

namespace Employee_CRUD.UI.Commands
{
    public class SearchEmployeesCommand : AsyncCommandBase
    {
        private readonly EmployeesViewModel _employeesViewModel;
        private readonly EmployeesStore _employeesStore;
        
        public SearchEmployeesCommand(EmployeesViewModel employeesViewModel, EmployeesStore employeesStore)
        {
            _employeesViewModel = employeesViewModel;
            _employeesStore = employeesStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _employeesViewModel.ErrorMessage = null;
            _employeesViewModel.IsLoading = true;

            string searchText = ((System.Windows.Controls.TextBox)parameter).Text;

            try
            {
                await _employeesStore.Search(searchText);
            }
            catch (Exception)
            {
                _employeesViewModel.ErrorMessage = "Failed to search employees.";
            }
            finally
            {
                _employeesViewModel.IsLoading = false;
            }
        }
    }
}
