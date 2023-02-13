using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class LaboratoryTest : AuditableBaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
