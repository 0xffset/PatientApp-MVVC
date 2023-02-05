using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientApp.Infrastructure.Persistence.Contexts;

namespace PatientApp.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<PatientAppContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<PatientAppContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               m => m.MigrationsAssembly(typeof(PatientAppContext).Assembly.FullName)));
            }
        }
    }
}
