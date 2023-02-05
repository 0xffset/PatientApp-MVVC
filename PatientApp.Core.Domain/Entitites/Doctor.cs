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

        // Navigation property
        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<LaboratoryResult>? LaboratoryResults { get; set; }
    }
}
