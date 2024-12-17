using GenClinic_Entities.DataModels;
using GenClinic_Repository.Data;
using GenClinic_Repository.IRepositories;

namespace GenClinic_Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public new readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}