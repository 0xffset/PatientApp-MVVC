using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.Services;
using PatientApp.Core.Application.ViewModerls.User;
using PatientApp.Middlewares;

namespace PatientApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserSessionValidation userSessionValidation;

        public UserController(IUserService userService, UserSessionValidation userSessionValidation)
        {
            this.userService = userService;
            this.userSessionValidation = userSessionValidation;
        }

        public IActionResult Index()
        {
            if (userSessionValidation.HasUser())
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
            if (userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            UserViewModel userVm = await userService.Login(vm);
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
            if (userSessionValidation.HasUser())
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
            if (userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await userService.Add(vm);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
