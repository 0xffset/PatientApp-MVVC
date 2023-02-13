using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Domain.Entitites;
using PatientApp.Infrastructure.Persistence.Contexts;
using PatientApp.Infrastructure.Persistence.Repository;

namespace PatientApp.Infrastructure.Persistence.Repositories
{
    public class LaboratoryTestRepository : GenericRepository<LaboratoryTest>, ILaboratoryTestRepository
    {
        public readonly PatientAppContext _dbContext;

        public LaboratoryTestRepository(PatientAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
