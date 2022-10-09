using System.Windows.Input;
using Employee_CRUD.UI.Commands;
using Employee_CRUD.UI.Stores;

namespace Employee_CRUD.UI.ViewModels
{
    public class EmployeesViewModel : ViewModelBase
    {
        public EmployeesListingViewModel EmployeesListingViewModel { get; }
        public EmployeesDetailsViewModel EmployeesDetailsViewModel { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
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

        public ICommand LoadEmployeesCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand ExportToExcelCommand { get; }

        public EmployeesViewModel(EmployeesStore employeesStore, SelectedEmployeeStore selectedEmployeeStore, ModalNavigationStore modalNavigationStore)
        {
            EmployeesListingViewModel = new EmployeesListingViewModel(employeesStore, selectedEmployeeStore, modalNavigationStore);
            EmployeesDetailsViewModel = new EmployeesDetailsViewModel(selectedEmployeeStore);

            LoadEmployeesCommand = new LoadEmployeesCommand(this, employeesStore);
            AddEmployeeCommand = new OpenAddEmployeeCommand(employeesStore, modalNavigationStore);
            ExportToExcelCommand = new ExportToExcelCommand(employeesStore);
        }

        public static EmployeesViewModel LoadViewModel(EmployeesStore employeesStore, SelectedEmployeeStore selectedEmployeeStore, ModalNavigationStore modalNavigationStore)
        {
            EmployeesViewModel viewModel = new EmployeesViewModel(employeesStore, selectedEmployeeStore, modalNavigationStore);

            viewModel.LoadEmployeesCommand.Execute(null);

            return viewModel;
        }
    }
}
