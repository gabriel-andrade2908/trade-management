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


            // Services
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IEmployeesServices, EmployeesServices>();
            services.AddScoped<IClientsServices, ClientsServices>();
            services.AddScoped<IProductsServices, ProductsServices>();

            return services;
        }
    }
}

