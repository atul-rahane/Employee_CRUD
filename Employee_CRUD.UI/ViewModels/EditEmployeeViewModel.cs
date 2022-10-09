using System;
using System.Windows.Input;
using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Commands;
using Employee_CRUD.UI.Stores;

namespace Employee_CRUD.UI.ViewModels
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        public int? EmployeeId { get; }

        public EmployeeDetailsFormViewModel EmployeeDetailsFormViewModel { get; }

        public EditEmployeeViewModel(Employee employee, EmployeesStore employeesStore, ModalNavigationStore modalNavigationStore)
        {
            EmployeeId = employee.Id;

            ICommand submitCommand = new EditEmployeeCommand(this, employeesStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            EmployeeDetailsFormViewModel = new EmployeeDetailsFormViewModel(submitCommand, cancelCommand)
            { 
                Name = employee.Name,
                Email = employee.Email,
                Gender = employee.Gender,
                Status = employee.Status
            };
        }
    }
}
