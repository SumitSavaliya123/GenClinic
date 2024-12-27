using GenClinic_Entities.DTOs.Request;
using GenClinic_Entities.DTOs.Response;

namespace GenClinic_Service.IServices
{
    public interface IProfileService
    {
        Task<UserDetailsInfoDto> GetProfileDetails(long id);
        Task UpdateUserProfile(long id, ProfileDetailsRequestDto profileDetailsDto);
    }
}