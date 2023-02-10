using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModels.User;
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

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await userRepository.GetAllWithIncludeAsync(new List<string> { "AccessLevels" });
            return userList.Select(x => new UserViewModel
            {
                Name = $"{x.FirstName} {x.LastName}",
                Id = x.Id,
                Username = x.Username,
                AccessLevelId = x.AccessLevelId,
                Email = x.Email
            }).ToList();
        }

        public async Task Update(SaveUserViewModel vm)
        {
            User user = await userRepository.GetByIdAsync(vm.Id);
            user.Id = vm.Id;
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            await userRepository.UpdateAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            await userRepository.DeleteAsync(user);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            SaveUserViewModel vm = new();
            vm.Id = user.Id;
            vm.Username = user.Username;
            vm.Email = user.Email;
            vm.FirstName = user.FirstName;
            vm.LastName = user.LastName;
            return vm;
        }
    }
}

