using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModerls.Appointment;
using PatientApp.Core.Application.ViewModerls.LaboratoryResult;
using PatientApp.Middlewares;

namespace PatientApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmetService appointmetService;
        private readonly UserSessionValidation _userSessionValidation;
        private readonly ILaboratoryTestService laboratoryTestService;
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;
        private readonly ILaboratoryResultService laboratoryResultService;
        public AppointmentController(IAppointmetService appointmetService,
                                     UserSessionValidation userSessionValidation,
                                     IDoctorService doctorService,
                                     IPatientService patientService,
                                     ILaboratoryTestService laboratoryTestServic, ILaboratoryResultService laboratoryResultService)
        {
            this.appointmetService = appointmetService;
            this._userSessionValidation = userSessionValidation;
            this.doctorService = doctorService;
            this.patientService = patientService;
            this.laboratoryTestService = laboratoryTestServic;
            this.laboratoryResultService = laboratoryResultService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await appointmetService.GetAllViewModel());
        }
        public async Task<IActionResult> Create()
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Appointment", action = "Index" });
            }
            SaveAppointmentViewModel viewModel = new SaveAppointmentViewModel();
            viewModel.Doctors = await doctorService.GetAllViewModelWithoutAuthentication();
            viewModel.Patients = await patientService.GetAllViewModelWithoutAuthentication();
            return View("SaveAppointment", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveAppointmentViewModel vm)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Appointment", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Doctors = await doctorService.GetAllViewModelWithoutAuthentication();
                vm.Patients = await patientService.GetAllViewModelWithoutAuthentication();
                return View("SaveAppointment", vm);
            }
            await appointmetService.Add(vm);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> AppointmentPendingResult(int id)
        {
            TempData["id"] = id;
            return View("SaveAppointmentPendingResult", await laboratoryResultService.GetAllLaboratoryTest(id));
        }

        public async Task<IActionResult> AppointmentComplete(int id)
        {
            return View("SaveAppointmentCompleteResult", await laboratoryResultService.GetAllLaboratoyTestComplete(id));
        }
        public async Task<IActionResult> AppointmentPending(int id)
        {
            SaveAppointmentViewModel vm = new SaveAppointmentViewModel()
            {
                Id = id,
                LaboratoryTests = await laboratoryTestService.GetAllViewModelWithoutAuthentication()
            };
            return View("SaveAppointmentPending", vm);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Appointment", action = "Index" });
            }

            return View(await appointmetService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Appointment", action = "Index" });
            }

            await appointmetService.Delete(id);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> AppointmentPending(SaveAppointmentViewModel saveAppointmentViewModel)
        {
            var data = saveAppointmentViewModel.SelectedMultiplesLaboratoryTest;
            if (data == null)
            {
                return RedirectToRoute(new { controller = "AppointmentPending", action = "Index" });
            }
            SaveLaboratoyResultViewModel saveLaboratoyResultViewModel = new();
            foreach (var item in data)
            {
                saveLaboratoyResultViewModel.LaboratoryTestId = item;
                saveLaboratoyResultViewModel.AppointmentId = saveAppointmentViewModel.Id;
                await laboratoryResultService.Add(saveLaboratoyResultViewModel);
            }
            saveAppointmentViewModel.Status = 2;
            await appointmetService.Update(saveAppointmentViewModel);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }


        public async Task<IActionResult> Complete(SaveAppointmentPendingResult vm)
        {
            SaveAppointmentViewModel viewModel = new();
            viewModel.Id = vm.Id;
            viewModel.Status = 3;
            await appointmetService.Update(viewModel);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });

        }
    }
}
