using System;
using System.Windows;
using Employee_CRUD.UI.HostBuilders;
using Employee_CRUD.UI.Stores;
using Employee_CRUD.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Employee_CRUD.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddConfiguration()
                .AddEmployeeCRUDAPI()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<EmployeesStore>();
                    services.AddSingleton<SelectedEmployeeStore>();

                    services.AddTransient<EmployeesViewModel>(CreateEmployeesViewModel);
                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
        private EmployeesViewModel CreateEmployeesViewModel(IServiceProvider services)
        {
            return EmployeesViewModel.LoadViewModel(
                services.GetRequiredService<EmployeesStore>(),
                services.GetRequiredService<SelectedEmployeeStore>(),
                services.GetRequiredService<ModalNavigationStore>()
                );
        }
    }
}
