using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PatientApp.Core.Application.ViewModerls.Doctor
{
    public class SaveDoctorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a proper first name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a proper last name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter a proper Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email does not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter a proper phone number")]
        [DataType(DataType.Text, ErrorMessage = "Email does not phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must enter a proper DNI")]
        [DataType(DataType.Text, ErrorMessage = "DNI does not valid")]
        public string DNI { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public string? ImageUrl { get; set; }
    }
}
