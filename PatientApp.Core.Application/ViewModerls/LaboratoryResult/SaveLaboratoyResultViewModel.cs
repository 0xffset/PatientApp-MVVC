using System.ComponentModel.DataAnnotations;

namespace PatientApp.Core.Application.ViewModerls.LaboratoryResult
{
    public class SaveLaboratoyResultViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a proper report result")]
        [DataType(DataType.Text, ErrorMessage = "Appointment cause does not report result")]
        public string ReportResult { get; set; }
        public int? Status { get; set; }
        public int? LaboratoryTestId { get; set; }
        public int? AppointmentId { get; set; }
    }
}
