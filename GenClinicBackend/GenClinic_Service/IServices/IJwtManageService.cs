using System.Security.Claims;
using GenClinic_Common.Utils;
using GenClinic_Entities.DataModels;

namespace GenClinic_Service.IServices
{
    public interface IJwtManageService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken(User user);
        ClaimsPrincipal GetPrincipalFormToken(string token);
        LoggedUser GetLoggedUser();
    }
}