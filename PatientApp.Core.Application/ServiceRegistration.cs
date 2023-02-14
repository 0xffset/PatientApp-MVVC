using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.Services;

namespace PatientApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration) =>
        #region Services
            services.AddTransient<IUserService, UserService>()
            .AddTransient<IDoctorService, DoctorService>()
            .AddTransient<ILaboratoryTestService, LaboratoryTestService>()
            .AddTransient<IPatientService, PatientService>();

        #endregion

    }
}
