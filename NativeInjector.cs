using Microsoft.Extensions.DependencyInjection;
using ProvaPub.Commom;
using ProvaPub.Commom.Pagamentos.Metodos;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IRandomService, RandomService>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentMethod, PixPaymentMethod>();           
            services.AddScoped<IPaymentMethod, PayPalPaymentMethod>();
            services.AddScoped<IPaymentMethod, CreditCardPaymentMethod>();

            #endregion

            #region Repositories

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            #endregion
        }
    }
}