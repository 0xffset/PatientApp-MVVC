using PatientApp.Core.Application.ViewModerls.User;

namespace PatientApp.Core.Application.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
    }
}
