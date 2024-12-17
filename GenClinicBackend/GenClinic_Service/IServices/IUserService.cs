using GenClinic_Common.Utils;
using GenClinic_Entities.DTOs.Request;

namespace GenClinic_Service.IServices
{
    public interface IUserService
    {
        Task<long> RegisterAsync(RegisterUserRequestDto registerUserRequestDto, LoggedUser loggedUser);
    }
}