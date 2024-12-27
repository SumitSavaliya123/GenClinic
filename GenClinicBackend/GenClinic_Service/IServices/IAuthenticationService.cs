using GenClinic_Entities.DTOs.Request;

namespace GenClinic_Service.IServices
{
    public interface IAuthenticationService
    {
        Task<string> ValidateLogin(LoginRequestDto loginRequest);

        Task SendOtp(long? id, string email, string typeOfOtp);

        Task<string> VerifyOtp(long? id, LoginOtpRequestDto otpData);

        Task ForgotPassword(LoginEmailRequestDto loginEmailRequestDto);
        Task ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto, string token);
    }
}