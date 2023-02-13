using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Interfaces.Repositories
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        public Task<Doctor> AddAsync(Doctor entity);
        public Task DeleteAsync(Doctor entity);
        public Task<List<Doctor>> GetAllAsync();
        public Task<List<Doctor>> GetAllWithIncludeAsync(List<string> properties);
        public Task<Doctor> GetByIdAsync(int id);
        public Task UpdateAsync(Doctor entity);

    }
}
