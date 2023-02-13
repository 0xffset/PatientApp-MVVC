using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModerls.Doctor;
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
        public IActionResult Create()
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Doctor", action = "Index" });
            }

            return View("SaveDoctor", new SaveDoctorViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveDoctorViewModel vm)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Doctor", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveDoctor", vm);
            }
            SaveDoctorViewModel doctorViewModel = await _doctorService.Add(vm);
            if (doctorViewModel.Id != 0 && doctorViewModel != null)
            {
                doctorViewModel.ImageUrl = UploadFile(vm.File, doctorViewModel.Id);

                await _doctorService.Update(doctorViewModel);
            }
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Doctor", action = "Index" });
            }

            return View(await _doctorService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Doctor", action = "Index" });
            }

            await _doctorService.Delete(id);
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Doctor", action = "Index" });
            }

            SaveDoctorViewModel vm = await _doctorService.GetByIdSaveViewModel(id);
            return View("SaveDoctor", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveDoctorViewModel vm)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Doctor", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveDoctor", vm);
            }
            SaveDoctorViewModel doctorVm = await _doctorService.GetByIdSaveViewModel(vm.Id);
            vm.ImageUrl = UploadFile(vm.File, vm.Id, true, doctorVm.ImageUrl);
            await _doctorService.Update(vm);
            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }


        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Doctor/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
