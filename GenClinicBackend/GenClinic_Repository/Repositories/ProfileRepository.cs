using System.Linq.Expressions;
using GenClinic_Entities.DataModels;
using GenClinic_Repository.Data;
using GenClinic_Repository.IRepositories;

namespace GenClinic_Repository.Repositories
{
    public class ProfileRepository : BaseRepository<User>, IProfileRepository
    {
        public new readonly AppDbContext _dbContext;
        public ProfileRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsDuplicateEmail(string email, long? userId)
       => userId is null ? await AnyAsync(EmailFilter(email)) : await AnyAsync(EmailFilter(email, userId));


        #region helper method
        private static Expression<Func<User, bool>> EmailFilter(string email,
        long? userId = null)
        => userId is null ? user => user.Email == email
                            : user => user.Email == email && user.Id != userId;

        #endregion
    }
}