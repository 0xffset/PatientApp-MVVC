using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class Patient : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DNI { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergies { get; set; }
        public string? Image { get; set; }
        public int UserId { get; set; }
    }
}
