using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModels.User;
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
        public async Task<IActionResult> Index()
        {

            return View(await _userService.GetAllViewModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View("SaveUser", await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);
            }

            await _userService.Update(vm);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


        public IActionResult Create()
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("SaveUser", new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);
            }

            var users = _userService.GetAllViewModel().Result.Any(x => x.Username == vm.Username);
            if (users == false)
            {
                await _userService.Add(vm);
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else
            {
                TempData["Message"] = "Already exist a user with that username";
                return View("SaveUser", vm);
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _userService.Delete(id);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
