using PatientApp.Core.Application.ViewModerls.User;
using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel loginVm);

    }
}
