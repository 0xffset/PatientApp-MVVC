using Microsoft.EntityFrameworkCore;
using PatientApp.Core.Application.Helpers;
using PatientApp.Core.Application.Interfaces.Repositories;
using PatientApp.Core.Application.ViewModels.User;
using PatientApp.Core.Domain.Entitites;
using PatientApp.Infrastructure.Persistence.Contexts;

namespace PatientApp.Infrastructure.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public readonly PatientAppContext _dbContext;
        public UserRepository(PatientAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryptation.ComputeSha256Hash(entity.Password);
            await base.AddAsync(entity);
            return entity;
        }
        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncryptation.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Username == loginVm.Username && user.Password == passwordEncrypt);
            return user;
        }
    }
}
