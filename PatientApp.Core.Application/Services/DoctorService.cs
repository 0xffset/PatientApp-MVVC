using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModerls.Doctor;

namespace PatientApp.Core.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public Task<SaveDoctorViewModel> Add(SaveDoctorViewModel vm)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DoctorViewModel>> GetAllViewModel()
        {
            var userList = await doctorRepository.GetAllAsync();
            return userList.Select(x => new DoctorViewModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                DNI = x.DNI,
                Image = x.Image,
                Email = x.Email
            }).ToList();
        }

        public Task<SaveDoctorViewModel> GetByIdSaveViewModel(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(SaveDoctorViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
