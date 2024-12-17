using GenClinic_Repository.Data;
using GenClinic_Repository.IRepositories;

namespace GenClinic_Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IUserRepository _userRepository;
        // private IAuthenticationRepository _authenticationRepository;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // public IAuthenticationRepository AuthenticationRepository
        // {
        //     get
        //     {
        //         return _authenticationRepository ??= new AuthenticationRepository(_dbContext);
        //     }
        // }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_dbContext);
            }
        }
    }
}