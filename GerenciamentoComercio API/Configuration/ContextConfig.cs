using GerenciamentoComercio_Domain.Utils.Security;
using GerenciamentoComercio_Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoComercio_API.Configuration
{
    public static class ContextConfig
    {
        public static IServiceCollection AddContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GerenciamentoComercioContext>(options => options
            .UseSqlServer(Security.DecryptString(configuration.GetConnectionString("GerenciamentoComercio"))));

            return services;
        }
    }
}
