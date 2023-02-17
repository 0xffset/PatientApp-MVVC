using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class LaboratoryResult : AuditableBaseEntity
    {
        public string? Result { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int? LaboratoryTestId { get; set; }

        // Navigation Property

        public Patient? Patient { get; set; }
        public LaboratoryTest? LaboratoryTest { get; set; }

    }
}
