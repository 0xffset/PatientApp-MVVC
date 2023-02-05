using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class AccessLevel : AuditableBaseEntity
    {
        public int NumberAccess { get; set; }
        public string Descripcion { get; set; }
        // Propery Navigation
        public User? User { get; set; }
    }
}
