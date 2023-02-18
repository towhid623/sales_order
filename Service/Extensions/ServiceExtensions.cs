using Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Service.SampleData;
using Service.Services;

namespace Service.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddData(configuration);

            services.AddScoped<ISampleDataHelper, SampleDataHelper>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IWindowService, WindowService>();
        }
    }
}
