using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Interfaces.Repositories
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        public Task<Patient> AddAsync(Patient entity);
        public Task DeleteAsync(Patient entity);
        public Task<List<Patient>> GetAllAsync();
        public Task<List<Patient>> GetAllWithIncludeAsync(List<string> properties);
        public Task<Patient> GetByIdAsync(int id);
        public Task UpdateAsync(Patient entity);

    }
}
