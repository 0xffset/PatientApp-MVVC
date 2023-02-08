using System.ComponentModel.DataAnnotations;

namespace PatientApp.Core.Application.ViewModerls.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter a proper first name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a proper last name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "You must enter a proper Username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter a proper Password")]
        [DataType(DataType.Text)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "You must enter a proper Email")]
        [DataType(DataType.Text)]
        public string Email { get; set; }
    }
}
