using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PatientApp.Core.Application.ViewModerls.Patient
{
    public class SavePatientViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter a proper first name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must enter a proper last name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You must enter a proper phone")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "You must enter a proper address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }
        [Required(ErrorMessage = "You must enter a proper DNI")]
        [DataType(DataType.Text)]
        public string DNI { get; set; }
        [Required(ErrorMessage = "You must enter a proper birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergies { get; set; }
        public string? ImageUrl { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }


    }
}
