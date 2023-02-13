using System.ComponentModel.DataAnnotations;

namespace PatientApp.Core.Application.ViewModerls.LaboratoryTest
{
    public class SaveLaboratoryTestViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must type a proper Laboratory Test Name")]
        public string Name { get; set; }
    }
}
