using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using @switch.application.Interface;
using @switch.application.Implementation;
using @switch.domain.Entities;
using @switch.domain.Repository;
using @switch.infrastructure.DAL;

namespace @switch.infrastructure.Extensions
{
    public static class SwitchServiceCollectionExtensions
    {
        public static IServiceCollection AddSwitch(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddDbContext<SwitchDbContext>(optionsAction);
            
            services.AddScoped<IRepository<SwitchToggle>, SqlFeatureToggleRepository>();
            services.AddScoped<ISwitchToggleService, SwitchToggleService>();

            return services;
        }
    }
}