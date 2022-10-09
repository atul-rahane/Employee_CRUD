using System;
using Employee_CRUD.Domain;
using Employee_CRUD.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Employee_CRUD.UI.HostBuilders
{
    public static class AddEmployeeCRUDAPIHostBuilderExtensions
    {
        public static IHostBuilder AddEmployeeCRUDAPI(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string baseAddress = context.Configuration.GetValue<string>("BaseAddress");
                string apiKey = context.Configuration.GetValue<string>("AuthKey");
                services.AddSingleton(new EmployeeCRUDAPIKey(apiKey));

                services.AddHttpClient<EmployeeCrudHttpClient>(c =>
                {
                    c.BaseAddress = new Uri(baseAddress);
                });
            });

            return host;
        }
    }
}
