using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModerls.LaboratoryResult;
using PatientApp.Middlewares;

namespace PatientApp.Controllers
{
    public class LaboratoryResultController : Controller
    {
        private readonly IAppointmetService appointmetService;
        private readonly UserSessionValidation _userSessionValidation;
        private readonly ILaboratoryTestService laboratoryTestService;
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;
        private readonly ILaboratoryResultService laboratoryResultService;

        public LaboratoryResultController(
            IAppointmetService appointmetService,
            UserSessionValidation userSessionValidation,
            ILaboratoryTestService laboratoryTestService,
            IDoctorService doctorService,
            IPatientService patientService,
            ILaboratoryResultService laboratoryResultService)
        {
            this.appointmetService = appointmetService;
            _userSessionValidation = userSessionValidation;
            this.laboratoryTestService = laboratoryTestService;
            this.doctorService = doctorService;
            this.patientService = patientService;
            this.laboratoryResultService = laboratoryResultService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await laboratoryResultService.GetAllViewModel());
        }

        public async Task<IActionResult> Report(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "LaboratoryResult", action = "Index" });
            }
            SaveLaboratoyResultViewModel vm = new();
            vm.Id = id;
            return View("ReportResult", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Report(SaveLaboratoyResultViewModel vm)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "LaboratoryResult", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("ReportResult", vm);
            }
            await laboratoryResultService.Update(vm);
            return RedirectToRoute(new { controller = "LaboratoryResult", action = "Index" });

        }
    }
}
