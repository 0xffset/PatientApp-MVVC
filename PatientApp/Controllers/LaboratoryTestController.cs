using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModerls.LaboratoryTest;
using PatientApp.Middlewares;

namespace PatientApp.Controllers
{
    public class LaboratoryTestController : Controller
    {
        private readonly ILaboratoryTestService _laboratoryTestService;
        private readonly UserSessionValidation _validateUserSession;

        public LaboratoryTestController(ILaboratoryTestService laboratoryTestService, UserSessionValidation validateUserSession)
        {
            _laboratoryTestService = laboratoryTestService;
            _validateUserSession = validateUserSession;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
            }
            return View(await _laboratoryTestService.GetAllViewModel());
        }
        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
            }

            return View("SaveLaboratoryTest", new SaveLaboratoryTestViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveLaboratoryTestViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveLaboratoryTest", vm);
            }
            await _laboratoryTestService.Add(vm);
            return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
            }

            return View(await _laboratoryTestService.GetByIdSaveViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
            }

            await _laboratoryTestService.Delete(id);
            return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
            }
            return View("SaveLaboratoryTest", await _laboratoryTestService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveLaboratoryTestViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveLaboratoryTest", vm);
            }

            await _laboratoryTestService.Update(vm);
            return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
        }
    }
}
