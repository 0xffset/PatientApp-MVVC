using PatientApp.Core.Application.ViewModerls.Doctor;
using PatientApp.Core.Application.ViewModerls.LaboratoryTest;
using PatientApp.Core.Application.ViewModerls.Patient;
using System.ComponentModel.DataAnnotations;

namespace PatientApp.Core.Application.ViewModerls.Appointment
{
    public class SaveAppointmentViewModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must enter a proper patient")]
        public int IdPatient { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must enter a proper doctor")]
        public int IdDoctor { get; set; }

        [Required(ErrorMessage = "You must enter a proper appointment date")]
        [DataType(DataType.DateTime, ErrorMessage = "Appointment date does not phone number")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "You must enter a proper appointment cause")]
        [DataType(DataType.Text, ErrorMessage = "Appointment cause does not valid")]
        public string Cause { get; set; }

        public int? Status { get; set; }
        public List<PatientViewModel>? Patients { get; set; }
        public List<DoctorViewModel>? Doctors { get; set; }
        public List<LaboratoryTestViewModel>? LaboratoryTests { get; set; }

        public int? AppointmentId { get; set; }
        public List<int>? SelectedMultiplesLaboratoryTest { get; set; }
    }
}
