using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class Appointment : AuditableBaseEntity
    {
        public string Cause { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public int UserId { get; set; }
        // Navigation Property
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}
