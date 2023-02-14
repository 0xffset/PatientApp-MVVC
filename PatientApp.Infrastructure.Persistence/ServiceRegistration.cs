using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Infrastructure.Persistence.Contexts;
using PatientApp.Infrastructure.Persistence.Repositories;
using PatientApp.Infrastructure.Persistence.Repository;

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

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<ILaboratoryTestRepository, LaboratoryTestRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();

            #endregion
        }
    }
}
