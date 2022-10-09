using System.Windows.Input;
using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Commands;
using Employee_CRUD.UI.Stores;

namespace Employee_CRUD.UI.ViewModels
{
    public class EmployeesListingItemViewModel : ViewModelBase
    {
        public Employee Employee { get; private set; }

        public string Name => Employee?.Name;
        public string Email => Employee?.Email;
        public string Gender => Employee?.Gender;
        public string Status => Employee?.Status;

        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public EmployeesListingItemViewModel(Employee employee, EmployeesStore employeesStore, ModalNavigationStore modalNavigationStore)
        {
            Employee = employee;

            EditCommand = new OpenEditEmployeeCommand(this, employeesStore, modalNavigationStore);
            DeleteCommand = new DeleteEmployeeCommand(this, employeesStore);
        }

        public void Update(Employee employee)
        {
            Employee = employee;

            OnPropertyChanged(nameof(Name));
        }
    }
}
