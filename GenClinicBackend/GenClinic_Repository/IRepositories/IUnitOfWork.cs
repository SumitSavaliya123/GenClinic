namespace GenClinic_Repository.IRepositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        // IAuthenticationRepository AuthenticationRepository { get; }
    }
}