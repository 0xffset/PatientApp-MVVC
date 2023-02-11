using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Domain.Entitites;
using PatientApp.Infrastructure.Persistence.Contexts;
using PatientApp.Infrastructure.Persistence.Repository;

namespace PatientApp.Infrastructure.Persistence.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public readonly PatientAppContext _dbContext;
        public DoctorRepository(PatientAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
