using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Employee_CRUD.Domain.Models;
using Employee_CRUD.UI.Stores;

namespace Employee_CRUD.UI.ViewModels
{
    public class EmployeesListingViewModel : ViewModelBase
    {
        private readonly EmployeesStore _EmployeesStore;
        private readonly SelectedEmployeeStore _selectedEmployeeStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly ObservableCollection<EmployeesListingItemViewModel> _EmployeesListingItemViewModels;
        public IEnumerable<EmployeesListingItemViewModel> EmployeesListingItemViewModels => _EmployeesListingItemViewModels;

        public EmployeesListingItemViewModel SelectedEmployeeListingItemViewModel
        {
            get
            {
                return _EmployeesListingItemViewModels
                    .FirstOrDefault(y => y.Employee?.Id == _selectedEmployeeStore.SelectedEmployee?.Id);
            }
            set
            {
                _selectedEmployeeStore.SelectedEmployee = value?.Employee;
            }
        }

        public EmployeesListingViewModel(EmployeesStore EmployeesStore, SelectedEmployeeStore selectedEmployeeStore, ModalNavigationStore modalNavigationStore)
        {
            _EmployeesStore = EmployeesStore;
            _selectedEmployeeStore = selectedEmployeeStore;
            _modalNavigationStore = modalNavigationStore;
            _EmployeesListingItemViewModels = new ObservableCollection<EmployeesListingItemViewModel>();

            _selectedEmployeeStore.SelectedEmployeeChanged += SelectedEmployeeStore_SelectedEmployeeChanged;

            _EmployeesStore.EmployeesLoaded += EmployeesStore_EmployeesLoaded;
            _EmployeesStore.EmployeeAdded += EmployeesStore_EmployeeAdded;
            _EmployeesStore.EmployeeUpdated += EmployeesStore_EmployeeUpdated;
            _EmployeesStore.EmployeeDeleted += EmployeesStore_EmployeeDeleted;

            _EmployeesListingItemViewModels.CollectionChanged += EmployeesListingItemViewModels_CollectionChanged;
        }

        protected override void Dispose()
        {
            _selectedEmployeeStore.SelectedEmployeeChanged -= SelectedEmployeeStore_SelectedEmployeeChanged;

            _EmployeesStore.EmployeesLoaded -= EmployeesStore_EmployeesLoaded;
            _EmployeesStore.EmployeeAdded -= EmployeesStore_EmployeeAdded;
            _EmployeesStore.EmployeeUpdated -= EmployeesStore_EmployeeUpdated;
            _EmployeesStore.EmployeeDeleted -= EmployeesStore_EmployeeDeleted;

            base.Dispose();
        }

        private void SelectedEmployeeStore_SelectedEmployeeChanged()
        {
            OnPropertyChanged(nameof(SelectedEmployeeListingItemViewModel));
        }

        private void EmployeesStore_EmployeesLoaded()
        {
            _EmployeesListingItemViewModels.Clear();

            foreach (Employee Employee in _EmployeesStore.Employees)
            {
                AddEmployee(Employee);
            }
        }

        private void EmployeesStore_EmployeeAdded(Employee Employee)
        {
            AddEmployee(Employee);
        }

        private void EmployeesStore_EmployeeUpdated(Employee Employee)
        {
            EmployeesListingItemViewModel EmployeeViewModel = 
                _EmployeesListingItemViewModels.FirstOrDefault(y => y.Employee.Id == Employee.Id);

            if(EmployeeViewModel != null)
            {
                EmployeeViewModel.Update(Employee);
            }
        }

        private void EmployeesStore_EmployeeDeleted(int id)
        {
            EmployeesListingItemViewModel itemViewModel = _EmployeesListingItemViewModels.FirstOrDefault(y => y.Employee?.Id == id);

            if(itemViewModel != null)
            {
                _EmployeesListingItemViewModels.Remove(itemViewModel);                
            }
        }

        private void EmployeesListingItemViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedEmployeeListingItemViewModel));
        }

        private void AddEmployee(Employee Employee)
        {
            EmployeesListingItemViewModel itemViewModel = 
                new EmployeesListingItemViewModel(Employee, _EmployeesStore, _modalNavigationStore);
            _EmployeesListingItemViewModels.Add(itemViewModel);
        }
    }
}
