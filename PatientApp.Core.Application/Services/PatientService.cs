using Microsoft.AspNetCore.Http;
using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModels.User;
using PatientApp.Core.Application.ViewModerls.Patient;
using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;

        public PatientService(IPatientRepository patientRepository, IHttpContextAccessor httpContextAccessor)
        {
            _patientRepository = patientRepository;
            _httpContextAccessor = httpContextAccessor;
            this.userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user"); ;
        }

        public async Task<SavePatientViewModel> Add(SavePatientViewModel vm)
        {
            Patient patient = new();
            patient.FirstName = vm.FirstName;
            patient.LastName = vm.LastName;
            patient.Address = vm.Address;
            patient.DNI = vm.DNI;
            patient.BirthDate = vm.BirthDate;
            patient.IsSmoker = vm.IsSmoker;
            patient.HasAllergies = vm.HasAllergies;
            patient.Image = vm.ImageUrl;
            patient.UserId = userViewModel.Id;
            patient.Phone = vm.Phone;
            patient = await _patientRepository.AddAsync(patient);

            SavePatientViewModel viewModel = new();

            viewModel.Id = patient.Id;
            viewModel.FirstName = patient.FirstName;
            viewModel.LastName = patient.LastName;
            viewModel.Address = patient.Address;
            viewModel.DNI = patient.DNI;
            viewModel.Address = patient.Address;
            viewModel.ImageUrl = patient.Image;
            viewModel.IsSmoker = patient.IsSmoker;
            viewModel.HasAllergies = patient.HasAllergies;
            viewModel.Phone = patient.Phone;
            return viewModel;

        }

        public async Task Delete(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            await _patientRepository.DeleteAsync(patient);
        }

        public async Task<List<PatientViewModel>> GetAllViewModel()
        {
            var patientList = await _patientRepository.GetAllAsync();
            var data = patientList.Where(x => x.UserId == userViewModel.Id).Select(x => new PatientViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                Address = x.Address,
                DNI = x.DNI,
                hasAllergies = x.HasAllergies,
                isSmoker = x.IsSmoker,
                UrlImage = x.Image,
                BirthDate = x.BirthDate

            }).ToList();
            return data;
        }
        public async Task<SavePatientViewModel> GetByIdSaveViewModel(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            SavePatientViewModel vm = new();
            vm.Id = patient.Id;
            vm.FirstName = patient.FirstName;
            vm.LastName = patient.LastName;
            vm.DNI = patient.DNI;
            vm.IsSmoker = patient.IsSmoker;
            vm.HasAllergies = patient.HasAllergies;
            vm.Address = patient.Address;
            vm.ImageUrl = patient.Image;
            vm.BirthDate = patient.BirthDate;
            vm.Phone = patient.Phone;
            return vm;
        }
        public async Task Update(SavePatientViewModel vm)
        {
            Patient patient = await _patientRepository.GetByIdAsync(vm.Id);
            patient.Id = vm.Id;
            patient.FirstName = vm.FirstName;
            patient.LastName = vm.LastName;
            patient.Address = vm.Address;
            patient.DNI = vm.DNI;
            patient.Address = vm.Address;
            patient.Image = vm.ImageUrl;
            patient.IsSmoker = vm.IsSmoker;
            patient.HasAllergies = vm.HasAllergies;
            patient.Phone = vm.Phone;
            await _patientRepository.UpdateAsync(patient);
        }
    }
}
