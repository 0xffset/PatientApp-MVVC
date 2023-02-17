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
        public string? ImageUrl { get; set; }
        public int UserId { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
