using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModels.User;
using PatientApp.Middlewares;
using PatientApp.Models;
using System.Diagnostics;

namespace PatientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly UserSessionValidation _userSessionValidation;

        public HomeController(ILogger<HomeController> logger, IUserService userService, UserSessionValidation userSessionValidation)
        {
            _logger = logger;
            _userService = userService;
            _userSessionValidation = userSessionValidation;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            if (_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            UserViewModel userVm = await _userService.Login(vm);
            if (userVm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Unable to access");
            }

            return View(vm);
        }
        public IActionResult Register()
        {
            if (_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _userService.Add(vm);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}