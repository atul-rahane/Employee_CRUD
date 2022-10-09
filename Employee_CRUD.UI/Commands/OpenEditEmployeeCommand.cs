using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Stores;
using Employee_CRUD.UI.ViewModels;

namespace Employee_CRUD.UI.Commands
{
    public class OpenEditEmployeeCommand : CommandBase
    {
        private readonly EmployeesListingItemViewModel _employeesListingItemViewModel;
        private readonly EmployeesStore _employeesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditEmployeeCommand(EmployeesListingItemViewModel employeesListingItemViewModel,
            EmployeesStore employeesStore, 
            ModalNavigationStore modalNavigationStore)
        {
            _employeesListingItemViewModel = employeesListingItemViewModel;
            _employeesStore = employeesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            Employee employee = _employeesListingItemViewModel.Employee;

            EditEmployeeViewModel editEmployeeViewModel = 
                new EditEmployeeViewModel(employee, _employeesStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editEmployeeViewModel;
        }
    }
}
