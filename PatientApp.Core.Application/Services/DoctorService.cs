using Microsoft.AspNetCore.Http;
using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModels.User;
using PatientApp.Core.Application.ViewModerls.Doctor;
using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        public DoctorService(IDoctorRepository doctorRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.doctorRepository = doctorRepository;
            this._httpContextAccessor = httpContextAccessor;
            this.userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<SaveDoctorViewModel> Add(SaveDoctorViewModel vm)
        {

            Doctor doctor = new();
            doctor.FirstName = vm.FirstName;
            doctor.LastName = vm.LastName;
            doctor.Phone = vm.Phone;
            doctor.Email = vm.Email;
            doctor.DNI = vm.DNI;
            doctor.ImageUrl = vm.ImageUrl;
            doctor.UserId = userViewModel.Id;

            doctor = await doctorRepository.AddAsync(doctor);

            SaveDoctorViewModel saveDoctorViewModel = new();
            saveDoctorViewModel.Id = doctor.Id;
            saveDoctorViewModel.FirstName = doctor.FirstName;
            saveDoctorViewModel.LastName = doctor.LastName;
            saveDoctorViewModel.Phone = doctor.Phone;
            saveDoctorViewModel.Email = doctor.Email;
            saveDoctorViewModel.DNI = doctor.DNI;
            saveDoctorViewModel.ImageUrl = doctor.ImageUrl;
            return saveDoctorViewModel;

        }

        public async Task Delete(int id)
        {
            var doctor = await doctorRepository.GetByIdAsync(id);
            await doctorRepository.DeleteAsync(doctor);
        }

        public async Task<List<DoctorViewModel>> GetAllViewModel()
        {
            var doctorList = await doctorRepository.GetAllAsync();
            var data = doctorList.Where(x => x.UserId == userViewModel.Id).Select(x => new DoctorViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                DNI = x.DNI,
                Image = x.ImageUrl,
                Email = x.Email
            }).ToList();
            return data;
        }

        public async Task<SaveDoctorViewModel> GetByIdSaveViewModel(int id)
        {
            var product = await doctorRepository.GetByIdAsync(id);

            SaveDoctorViewModel vm = new();
            vm.Id = product.Id;
            vm.FirstName = product.FirstName;
            vm.LastName = product.LastName;
            vm.DNI = product.DNI;
            vm.Phone = product.Phone;
            vm.Email = product.Email;
            vm.ImageUrl = product.ImageUrl;

            return vm;
        }

        public async Task Update(SaveDoctorViewModel vm)
        {
            Doctor doctor = await doctorRepository.GetByIdAsync(vm.Id);
            doctor.Id = vm.Id;
            doctor.FirstName = vm.FirstName;
            doctor.LastName = vm.LastName;
            doctor.DNI = vm.DNI;
            doctor.ImageUrl = vm.ImageUrl;
            doctor.Email = doctor.Email;
            doctor.Phone = doctor.Phone;

            await doctorRepository.UpdateAsync(doctor);
        }
    }
}
