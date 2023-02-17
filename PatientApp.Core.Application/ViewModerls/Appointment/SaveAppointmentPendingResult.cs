namespace PatientApp.Core.Application.ViewModerls.Appointment
{
    public class SaveAppointmentPendingResult
    {
        public int Id { get; set; }
        public string LaboratoryTestName { get; set; }
        public int LaboratoryResultStatus { get; set; }

        public int? AppointmentId { get; set; }
    }
}
