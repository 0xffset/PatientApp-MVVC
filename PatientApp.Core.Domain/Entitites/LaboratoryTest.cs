using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class LaboratoryTest : AuditableBaseEntity
    {
        public string Name { get; set; }
        public int? LaboratoryResultId { get; set; }

        // Navigation Property
        public LaboratoryResult LaboratoryResult { get; set; }

    }
}
