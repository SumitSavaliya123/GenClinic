using GenClinic_Entities.DataModels;

namespace GenClinic_Repository.IRepositories
{
    public interface IProfileRepository : IBaseRepository<User>
    {
        Task<bool> IsDuplicateEmail(string email, long? userId);
    }
}