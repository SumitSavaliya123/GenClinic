using GenClinic_Api.Helpers;
using GenClinic_Common.Constants;
using GenClinic_Common.Exceptions;
using GenClinic_Entities.DTOs.Request;
using GenClinic_Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GenClinic_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
                throw new ModelStateException(ModelState);

            return ResponseHelper.SuccessResponse(await _authenticationService.ValidateLogin(loginRequest));
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(LoginOtpRequestDto otpData)
        {
            return ResponseHelper.SuccessResponse(await _authenticationService.VerifyOtp(null, otpData), MessageConstants.LOGIN_SUCCESS);
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp(LoginEmailRequestDto loginEmailRequestDto)
        {
            await _authenticationService.SendOtp(null, loginEmailRequestDto.Email, SystemConstants.AuthenticationOtp);
            return ResponseHelper.SuccessResponse(MessageConstants.MAIL_SENT);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(LoginEmailRequestDto loginEmailRequestDto)
        {
            await _authenticationService.ForgotPassword(loginEmailRequestDto);
            return ResponseHelper.SuccessResponse(MessageConstants.MAIL_SENT);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto, string token)
        {
            await _authenticationService.ResetPassword(resetPasswordRequestDto, token);
            return ResponseHelper.SuccessResponse(MessageConstants.PASSWORD_RESET);
        }
    }
}