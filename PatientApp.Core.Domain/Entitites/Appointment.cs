using PatientApp.Core.Domain.Common;

namespace PatientApp.Core.Domain.Entitites
{
    public class Appointment : AuditableBaseEntity
    {
        public string Cause { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int LaboratoryResultId { get; set; }
        // Navigation Property
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public LaboratoryResult LaboratoryResult { get; set; }
    }
}
