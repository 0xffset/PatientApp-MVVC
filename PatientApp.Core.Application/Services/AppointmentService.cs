using Microsoft.AspNetCore.Http;
using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModels.User;
using PatientApp.Core.Application.ViewModerls.Appointment;
using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Services
{
    public class AppointmentService : IAppointmetService
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserViewModel userViewModel;

        public AppointmentService(IAppointmentRepository appointmentRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.appointmentRepository = appointmentRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userViewModel = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public async Task<SaveAppointmentViewModel> Add(SaveAppointmentViewModel vm)
        {
            Appointment appointment = new();
            appointment.DoctorId = vm.IdDoctor;
            appointment.PatientId = vm.IdPatient;
            appointment.Cause = vm.Cause;
            appointment.Date = vm.Date;
            appointment.UserId = userViewModel.Id;
            appointment.Status = 1;

            appointment = await appointmentRepository.AddAsync(appointment);
            SaveAppointmentViewModel saveAppointmentViewModel = new();
            saveAppointmentViewModel.Id = appointment.Id;
            saveAppointmentViewModel.IdDoctor = appointment.DoctorId;
            saveAppointmentViewModel.IdPatient = appointment.PatientId;
            saveAppointmentViewModel.Cause = appointment.Cause;
            saveAppointmentViewModel.Date = appointment.Date;
            saveAppointmentViewModel.Status = appointment.Status;
            return saveAppointmentViewModel;
        }

        public async Task Delete(int id)
        {
            var appointment = await appointmentRepository.GetByIdAsync(id);
            await appointmentRepository.DeleteAsync(appointment);
        }

        public async Task<List<AppointmentViewModel>> GetAllViewModel()
        {
            var appointList = await appointmentRepository.GetAllWithIncludeAsync(new List<string> { "Doctor", "Patient" });

            var listViewModels = appointList.Where(x => x.UserId == userViewModel.Id).Select(x => new AppointmentViewModel
            {
                Id = x.Id,
                DoctorName = $"{x.Doctor.FirstName} {x.Doctor.LastName}",
                PatientName = $"{x.Patient.FirstName} {x.Patient.LastName}",
                Cause = x.Cause,
                Date = x.Date,
                Status = x.Status
            }).ToList();

            return listViewModels;
        }

        public async Task<List<AppointmentViewModel>> GetAllViewModelWithoutAuthentication()
        {
            var appointList = await appointmentRepository.GetAllWithIncludeAsync(new List<string> { "Doctor", "Patient" });

            var listViewModels = appointList.Select(x => new AppointmentViewModel
            {
                Id = x.Id,
                DoctorName = $"{x.Doctor.FirstName} {x.Doctor.LastName}",
                PatientName = $"{x.Patient.FirstName} {x.Patient.LastName}",
                DoctorId = x.Doctor.Id,
                PatientId = x.Patient.Id,
                Cause = x.Cause,
                Date = x.Date,
                Status = x.Status
            }).ToList();

            return listViewModels;
        }

        public async Task<SaveAppointmentViewModel> GetByIdSaveViewModel(int id)
        {
            var appointment = await appointmentRepository.GetByIdAsync(id);

            SaveAppointmentViewModel vm = new();
            vm.Id = appointment.Id;
            vm.Date = appointment.Date;
            vm.Status = vm.Status;
            return vm;
        }

        public async Task Update(SaveAppointmentViewModel vm)
        {
            Appointment laboratoryTest = await appointmentRepository.GetByIdAsync(vm.Id);
            laboratoryTest.Status = (int)vm.Status;
            await appointmentRepository.UpdateAsync(laboratoryTest);
        }
    }
}
