using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebPOS.Infrastructure.Entities;
using WebPOS.Infrastructure.Persistences.Contracts;
using WebPOS.Infrastructure.Persistences.Repositories;

namespace WebPOS.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            string? assembly = typeof(PosContext).Assembly.FullName;

            services.AddDbContext<PosContext>(option => option.UseSqlServer(
                    configuration.GetConnectionString("POSConnection"), b => b.MigrationsAssembly(assembly)),
                    ServiceLifetime.Transient
                );

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
