using Microsoft.AspNetCore.Http;
using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModels.User;
using PatientApp.Core.Application.ViewModerls.LaboratoryTest;
using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Services
{
    public class LaboratoryTestService : ILaboratoryTestService
    {
        private readonly ILaboratoryTestRepository laboratoryTestRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;

        public LaboratoryTestService(ILaboratoryTestRepository laboratoryTestRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.laboratoryTestRepository = laboratoryTestRepository;
            this.userViewModel = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

        }

        public async Task<SaveLaboratoryTestViewModel> Add(SaveLaboratoryTestViewModel vm)
        {
            LaboratoryTest laboratoryTest = new();
            laboratoryTest.Name = vm.Name;
            laboratoryTest.UserId = userViewModel.Id;

            laboratoryTest = await laboratoryTestRepository.AddAsync(laboratoryTest);
            SaveLaboratoryTestViewModel saveLaboratoryTestViewModel = new();
            saveLaboratoryTestViewModel.Id = laboratoryTest.Id;
            saveLaboratoryTestViewModel.Name = laboratoryTest.Name;
            return saveLaboratoryTestViewModel;
        }

        public async Task Delete(int id)
        {
            var lab = await laboratoryTestRepository.GetByIdAsync(id);
            await laboratoryTestRepository.DeleteAsync(lab);
        }

        public async Task<List<LaboratoryTestViewModel>> GetAllViewModel()
        {
            var userList = await laboratoryTestRepository.GetAllAsync();
            var data = userList.Where(x => x.UserId == userViewModel.Id).Select(x => new LaboratoryTestViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            return data;
        }

        public async Task<SaveLaboratoryTestViewModel> GetByIdSaveViewModel(int id)
        {
            var lab = await laboratoryTestRepository.GetByIdAsync(id);

            SaveLaboratoryTestViewModel vm = new();
            vm.Id = lab.Id;
            vm.Name = lab.Name;
            return vm;
        }

        public async Task Update(SaveLaboratoryTestViewModel vm)
        {
            LaboratoryTest laboratoryTest = await laboratoryTestRepository.GetByIdAsync(vm.Id);
            laboratoryTest.Id = vm.Id;
            laboratoryTest.Name = vm.Name;

            await laboratoryTestRepository.UpdateAsync(laboratoryTest);
        }
    }
}
