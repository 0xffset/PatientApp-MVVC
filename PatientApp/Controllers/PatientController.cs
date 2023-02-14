using Microsoft.AspNetCore.Mvc;
using PatientApp.Core.Application.Interfaces.Services;
using PatientApp.Core.Application.ViewModerls.Patient;
using PatientApp.Middlewares;

namespace PatientApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService patientService;
        private readonly UserSessionValidation _userSessionValidation;

        public PatientController(IPatientService patientService, UserSessionValidation userSessionValidation)
        {
            this.patientService = patientService;
            this._userSessionValidation = userSessionValidation;
        }
        public async Task<IActionResult> Index()
        {
            return View(await patientService.GetAllViewModel());
        }
        public IActionResult Create()
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Patient", action = "Index" });
            }

            return View("SavePatient", new SavePatientViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(SavePatientViewModel vm)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Patient", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SavePatient", vm);
            }
            SavePatientViewModel patientViewModel = await patientService.Add(vm);
            if (patientViewModel.Id != 0 && patientViewModel != null)
            {
                patientViewModel.ImageUrl = UploadFile(vm.File, patientViewModel.Id);

                await patientService.Update(patientViewModel);
            }
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Patient", action = "Index" });
            }

            return View(await patientService.GetByIdSaveViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Patient", action = "Index" });
            }

            await patientService.Delete(id);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Patient", action = "Index" });
            }

            SavePatientViewModel vm = await patientService.GetByIdSaveViewModel(id);
            return View("SavePatient", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePatientViewModel vm)
        {
            if (!_userSessionValidation.HasUser())
            {
                return RedirectToRoute(new { controller = "Patient", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SavePatient", vm);
            }
            SavePatientViewModel doctorVm = await patientService.GetByIdSaveViewModel(vm.Id);
            vm.ImageUrl = UploadFile(vm.File, vm.Id, true, doctorVm.ImageUrl);
            await patientService.Update(vm);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
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
            string basePath = $"/Images/Patient/{id}";
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
