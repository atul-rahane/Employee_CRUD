using System.Windows.Input;
using Employee_CRUD.UI.Stores;

namespace Employee_CRUD.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public EmployeesViewModel EmployeesViewModel { get; }

        public MainViewModel(ModalNavigationStore modalNavigationStore, EmployeesViewModel employeesViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            EmployeesViewModel = employeesViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;

            base.Dispose();
        }

        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
