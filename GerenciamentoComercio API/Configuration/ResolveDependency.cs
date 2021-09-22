using GerenciamentoComercio_API.Configuration;
using GerenciamentoComercio_Domain.Utils.EmailSender;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
using GerenciamentoComercio_Domain.Utils.UnitOfWork;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Domain.v1.Repositories;
using GerenciamentoComercio_Domain.v1.Services;
using Incidentes.Business.v1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sistema_Incidentes.Configuration
{
    public static class DependencyConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Work Interfaces
            services.AddScoped<IUserApp, UserApp>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            //Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IProductHistoricRepository, ProductHistoricRepository>();
            services.AddScoped<IServiceHistoricRepository, ServiceHistoricRepository>();
            services.AddScoped<IClientTransactionRepository, ClientTransactionRepository>();
            services.AddScoped<IClientTransactionProductRepository, ClientTransactionProductRepository>();
            services.AddScoped<IClientTransactionServiceRepository, ClientTransactionServiceRepository>();

            // Services
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IEmployeesServices, EmployeesServices>();
            services.AddScoped<IClientsServices, ClientsServices>();
            services.AddScoped<IProductsServices, ProductsServices>();
            services.AddScoped<IProductCategoriesServices, ProductCategoriesServices>();
            services.AddScoped<IServicesServices, ServicesServices>();
            services.AddScoped<IServiceCategoriesServices, ServiceCategoriesServices>();
            services.AddScoped<IProductsHistoricServices, ProductsHistoricServices>();
            services.AddScoped<IClientTransactionsServices, ClientTransactionsServices>();
            services.AddScoped<ITransactionsCommonServices, TransactionsCommonServices>();
            services.AddScoped<IProductTransactionServices, ProductTransactionServices>();
            services.AddScoped<IServiceTransactionServices, ServiceTransactionServices>();

            return services;
        }
    }
}

