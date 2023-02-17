using Microsoft.AspNetCore.Http;
using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModels.User;
using PatientApp.Core.Application.ViewModerls.Appointment;
using PatientApp.Core.Application.ViewModerls.LaboratoryResult;
using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Services
{
    public class LaboratoryResultService : ILaboratoryResultService
    {
        private readonly ILaboratoryResultRepository laboratoryResultRepository;
        private readonly ILaboratoryTestRepository laboratoryTestRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserViewModel userViewModel;

        public LaboratoryResultService(
            IHttpContextAccessor httpContextAccessor,
            ILaboratoryResultRepository laboratoryResultRepository,
            IAppointmentRepository appointmentRepository,
            ILaboratoryTestRepository laboratoryTestRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userViewModel = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            this.laboratoryResultRepository = laboratoryResultRepository;
            this.appointmentRepository = appointmentRepository;
            this.laboratoryTestRepository = laboratoryTestRepository;
        }

        public async Task<SaveLaboratoyResultViewModel> Add(SaveLaboratoyResultViewModel vm)
        {
            LaboratoryResult laboratoryResult = new();
            laboratoryResult.Result = vm.ReportResult;
            laboratoryResult.Status = 0;
            laboratoryResult.UserId = userViewModel.Id;
            var appointment = await appointmentRepository.GetByIdAsync((int)vm.AppointmentId);
            var patientId = appointment.PatientId;
            laboratoryResult.PatientId = patientId;
            laboratoryResult.LaboratoryTestId = vm.LaboratoryTestId;
            laboratoryResult.AppointmentId = (int)vm.AppointmentId;
            laboratoryResult.DoctorId = appointment.DoctorId;


            laboratoryResult = await laboratoryResultRepository.AddAsync(laboratoryResult);
            SaveLaboratoyResultViewModel resultViewModel = new();
            resultViewModel.Id = laboratoryResult.Id;
            resultViewModel.Status = laboratoryResult.Status;
            resultViewModel.ReportResult = laboratoryResult.Result;
            resultViewModel.LaboratoryTestId = vm.LaboratoryTestId;
            return resultViewModel;

        }


        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SaveAppointmentPendingResult>> GetAllLaboratoryTest(int id)
        {


            var appointList = await laboratoryResultRepository.GetAllWithIncludeAsync(new List<string> { "LaboratoryTest" });

            List<SaveAppointmentPendingResult> vm = new();
            var listViewModels = appointList.Where(x => x.UserId == userViewModel.Id && x.AppointmentId == id).ToList();

            foreach (var item in listViewModels)
            {
                vm.Add(new SaveAppointmentPendingResult
                {
                    Id = item.Id,
                    LaboratoryResultStatus = item.Status,
                    LaboratoryTestName = item.LaboratoryTest.Name,
                });
            }

            return vm;
        }

        public async Task<List<SaveAppointmentPendingResult>> GetAllLaboratoyTestComplete(int id)
        {
            var appointList = await laboratoryResultRepository.GetAllWithIncludeAsync(new List<string> { "LaboratoryTest" });

            var listViewModels = appointList.Where(x => x.UserId == userViewModel.Id && x.AppointmentId == id && x.Status == 1).Select(x => new SaveAppointmentPendingResult
            {
                Id = x.Id,
                LaboratoryResultStatus = x.Status,
                LaboratoryTestName = x.LaboratoryTest.Name,

            }).ToList();

            return listViewModels;
        }
        public async Task<List<LaboratoryResultViewModel>> GetAllViewModel()
        {
            var appointList = await laboratoryResultRepository.GetAllWithIncludeAsync(new List<string> { "Patient", "LaboratoryTest" });

            var listViewModels = appointList.Where(x => x.UserId == userViewModel.Id && x.Status == 0).Select(x => new LaboratoryResultViewModel
            {
                Id = x.Id,
                PatientName = $"{x.Patient.FirstName} {x.Patient.LastName}",
                LaboratoryTestName = x.LaboratoryTest.Name,
                PatientDNI = x.Patient.DNI,
            }).ToList();

            return listViewModels;
        }

        public async Task<List<LaboratoryResultViewModel>> GetAllViewModelWithoutAuthentication()
        {
            var appointList = await laboratoryResultRepository.GetAllWithIncludeAsync(new List<string> { "Patient" });

            var listViewModels = appointList.Where(x => x.Status == 0).Select(x => new LaboratoryResultViewModel
            {
                Id = x.Id,
                PatientName = $"{x.Patient.FirstName} {x.Patient.LastName}",
                LaboratoryTestName = x.LaboratoryTest.Name,
                PatientDNI = x.Patient.DNI,
            }).ToList();

            return listViewModels;
        }


        public Task<SaveLaboratoyResultViewModel> GetByIdSaveViewModel(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(SaveLaboratoyResultViewModel vm)
        {
            LaboratoryResult laboratoryResult = await laboratoryResultRepository.GetByIdAsync(vm.Id);
            laboratoryResult.Status = 1;
            laboratoryResult.Result = vm.ReportResult;
            await laboratoryResultRepository.UpdateAsync(laboratoryResult);
        }
    }
}
