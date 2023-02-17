using PatientApp.Core.Application.ViewModerls.Appointment;
using PatientApp.Core.Application.ViewModerls.LaboratoryResult;

namespace PatientApp.Core.Application.Interfaces.Services
{
    public interface ILaboratoryResultService : IGenericService<SaveLaboratoyResultViewModel, LaboratoryResultViewModel>
    {
        Task<List<SaveAppointmentPendingResult>> GetAllLaboratoryTest(int id);
        Task<List<SaveAppointmentPendingResult>> GetAllLaboratoyTestComplete(int id);

    }
}
