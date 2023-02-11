using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Middlewares;

namespace PatientApp.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDoctorService _doctorService;
        private readonly UserSessionValidation _userSessionValidation;

        public DoctorController(ILogger<HomeController> logger, IDoctorService doctorService, UserSessionValidation userSessionValidation)
        {
            _logger = logger;
            _doctorService = doctorService;
            _userSessionValidation = userSessionValidation;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _doctorService.GetAllViewModel());
        }
    }
}
