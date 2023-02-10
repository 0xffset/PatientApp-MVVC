using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModerls.User;
using PatientApp.Middlewares;

namespace PatientApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserSessionValidation _userSessionValidation;

        public UserController(IUserService userService, UserSessionValidation userSessionValidation)
        {
            _userService = userService;
            _userSessionValidation = userSessionValidation;
        }

        public IActionResult Index()
        {
            if (_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
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
                ModelState.AddModelError("userValidation", "Datos de acceso incorrecto");
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
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
