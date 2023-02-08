using PatientApp.Core.Application.Interfaces.Repositories;
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
            user = await userRepository.AddAsync(user);

            SaveUserViewModel userVm = new();

            userVm.Id = user.Id;
            userVm.FirstName = user.FirstName;
            userVm.LastName = user.LastName;
            userVm.Email = user.Email;
            userVm.Username = user.Username;
            userVm.Password = user.Password;

            return userVm;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserViewModel>> GetAllViewModel()
        {
            throw new NotImplementedException();
        }

        public Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel> Login(LoginViewModel vm)
        {
            throw new NotImplementedException();
        }

        public Task Update(SaveUserViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
