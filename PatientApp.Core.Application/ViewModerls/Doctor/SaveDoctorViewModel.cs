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
        [DataType(DataType.PhoneNumber, ErrorMessage = "Email does not phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must enter a proper Image")]
        [DataType(DataType.Text, ErrorMessage = "Email does not Image")]
        public string Image { get; set; }

        [RegularExpression("^(([A-Z]\\d{8})|(\\d{8}[A-Z]))$", ErrorMessage = "Wrong DNI")]
        [DataType(DataType.Text, ErrorMessage = "DNI does not valid")]
        public string DNI { get; set; }
    }
}
