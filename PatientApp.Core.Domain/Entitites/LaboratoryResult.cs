using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class LaboratoryResult : AuditableBaseEntity
    {
        public string Result { get; set; }
        public int DoctorId { get; set; }
        public int LaboratoryTestId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }

        // Navigation Property
        public Doctor? Doctor { get; set; }
        public LaboratoryTest? LaboratoryTest { get; set; }
        public Appointment? Appointment { get; set; }
        public Patient? Patient { get; set; }

    }
}
