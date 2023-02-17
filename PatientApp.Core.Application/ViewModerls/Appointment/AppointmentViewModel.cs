namespace PatientApp.Core.Application.ViewModerls.Appointment
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public DateTime Date { get; set; }
        public string Cause { get; set; }
        public int Status { get; set; }


        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
