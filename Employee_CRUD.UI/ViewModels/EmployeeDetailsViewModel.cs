using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Stores;

namespace Employee_CRUD.UI.ViewModels
{
    public class EmployeesDetailsViewModel : ViewModelBase 
    {
        private readonly SelectedEmployeeStore _selectedEmployeeStore;

        //TODO -- check why nullable is not accepted here
        private Employee SelectedEmployee => _selectedEmployeeStore.SelectedEmployee;

        public bool HasSelectedEmployee => SelectedEmployee != null;
        public string Name => SelectedEmployee?.Name;
        public string Email => SelectedEmployee?.Email;
        public string Gender => SelectedEmployee?.Gender;
        public string Status => SelectedEmployee?.Status;

        public EmployeesDetailsViewModel(SelectedEmployeeStore selectedEmployeeStore)
        {
            _selectedEmployeeStore = selectedEmployeeStore;

            _selectedEmployeeStore.SelectedEmployeeChanged += SelectedEmployeeStore_SelectedEmployeeChanged;
        }

        protected override void Dispose()
        {
            _selectedEmployeeStore.SelectedEmployeeChanged -= SelectedEmployeeStore_SelectedEmployeeChanged;

            base.Dispose();
        }

        private void SelectedEmployeeStore_SelectedEmployeeChanged()
        {
            OnPropertyChanged(nameof(HasSelectedEmployee));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Gender));
            OnPropertyChanged(nameof(Status));
        }
    }
}
