using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebPOS.Application.Contracts;
using WebPOS.Application.Services;
using WebPOS.Application.Validators.Categories;

namespace WebPOS.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICategoryApplication, CategoryApplication>();

            /*services.AddFluentValidation(options => {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });*/
            
            //services.AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<CategoryValidator>());
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

            return services;
        }
    }
}
