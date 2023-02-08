using System.ComponentModel.DataAnnotations;

namespace PatientApp.Core.Application.ViewModerls.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You must enter a proper username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter a proper password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
