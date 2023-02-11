using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Core.Application.Interfaces.Repositories
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        public Task<Doctor> AddAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Doctor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Doctor>> GetAllWithIncludeAsync(List<string> properties)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Doctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
