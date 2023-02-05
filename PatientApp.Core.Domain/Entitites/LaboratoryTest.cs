using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class LaboratoryTest : AuditableBaseEntity
    {
        public string Name { get; set; }

        // Navigation Property

        public ICollection<LaboratoryResult> LaboratoryResults { get; set; }

    }
}
