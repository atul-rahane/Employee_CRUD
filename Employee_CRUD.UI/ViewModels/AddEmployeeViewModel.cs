using System.Windows.Input;
using Employee_CRUD.UI.Commands;
using Employee_CRUD.UI.Stores;

namespace Employee_CRUD.UI.ViewModels
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        public EmployeeDetailsFormViewModel EmployeeDetailsFormViewModel { get; }

        public AddEmployeeViewModel(EmployeesStore employeesStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand submitCommand = new AddEmployeeCommand(this, employeesStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            EmployeeDetailsFormViewModel = new EmployeeDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
