namespace PatientApp.Core.Application.ViewModerls.Patient
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DNI { get; set; }
        public DateTime BirthDate { get; set; }
        public bool isSmoker { get; set; } = false;
        public bool hasAllergies { get; set; } = false;
        public string UrlImage { get; set; }
    }
}
