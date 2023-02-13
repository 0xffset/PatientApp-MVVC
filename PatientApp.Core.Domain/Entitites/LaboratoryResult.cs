using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class LaboratoryResult : AuditableBaseEntity
    {
        public string Result { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }

        // Navigation Property

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Patient> Patients { get; set; }

    }
}
