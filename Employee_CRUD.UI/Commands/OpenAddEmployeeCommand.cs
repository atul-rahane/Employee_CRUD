using Employee_CRUD.UI.Stores;
using Employee_CRUD.UI.ViewModels;

namespace Employee_CRUD.UI.Commands
{
    public class OpenAddEmployeeCommand : CommandBase
    {
        private readonly EmployeesStore _employeesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddEmployeeCommand(EmployeesStore employeesStore, ModalNavigationStore modalNavigationStore)
        {
            _employeesStore = employeesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new AddEmployeeViewModel(_employeesStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;

        }
    }
}
