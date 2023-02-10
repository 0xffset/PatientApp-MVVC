using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModerls.User;
using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            User user = new();
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.Username = vm.Username;
            user.Password = vm.Password;
            user.Email = vm.Email;
            user.AccessLevelId = vm.UserType;
            user = await userRepository.AddAsync(user);

            SaveUserViewModel userVm = new();

            userVm.Id = user.Id;
            userVm.FirstName = user.FirstName;
            userVm.LastName = user.LastName;
            userVm.Email = user.Email;
            userVm.Username = user.Username;
            userVm.Password = user.Password;
            userVm.UserType = user.AccessLevelId;
            return userVm;
        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            UserViewModel userVm = new();
            User user = await userRepository.LoginAsync(vm);

            if (user == null)
            {
                return null;
            }

            userVm.Id = user.Id;
            userVm.Name = $"{user.FirstName} {user.LastName}";
            userVm.Username = user.Username;
            userVm.Password = user.Password;
            userVm.AccessLevelId = user.AccessLevelId;
            userVm.Email = user.Email;

            return userVm;
        }

    }
}

