using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IProvidedServiceRepository, ProvidedServiceRepository>();

            return services;
        }
    }
}
