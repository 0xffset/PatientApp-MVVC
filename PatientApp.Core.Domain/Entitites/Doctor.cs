using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class Doctor : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DNI { get; set; }
        public string Image { get; set; }

        public int? AppointmentId { get; set; }
        public int? LaboratoryResultId { get; set; }
        // Navigation property
        public Appointment Appointment { get; set; }

        public LaboratoryResult LaboratoryResult { get; set; }
    }
}
