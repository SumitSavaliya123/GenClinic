using GenClinic_Common.Constants;
using GenClinic_Common.Exceptions;
using GenClinic_Common.Utils.Models;
using GenClinic_Entities.DataModels;
using GenClinic_Entities.DTOs.Request;
using GenClinic_Repository.IRepositories;
using GenClinic_Service.IServices;
using Microsoft.AspNetCore.Http;

namespace GenClinic_Service.Services
{
    public class AuthenticationService : BaseService<User>, IAuthenticationService
    {
        private readonly IMailService _mailService;
        private readonly IJwtManageService _jwtManageService;

        public AuthenticationService(IUserRepository userRepository, IMailService mailService, IJwtManageService jwtManageService) : base(userRepository)
        {
            _mailService = mailService;
            _jwtManageService = jwtManageService;
        }

        public async Task<string> ValidateLogin(LoginRequestDto loginRequest)
        {
            User? user = await GetFirstOrDefaultAsync(x => x.Email == loginRequest.Email);

            if (user == null)
                throw new CustomException(StatusCodes.Status404NotFound, ErrorMessages.USER_NOT_FOUND);

            bool isUserValid = PasswordUtil.VerifyPassword(loginRequest.Password, user.Password);

            if (!isUserValid)
                throw new CustomException(StatusCodes.Status404NotFound, ErrorMessages.INVALID_USER);

            await SendOtp(null, user.Email, SystemConstants.AuthenticationOtp);
            return user.FirstName + ' ' + user.LastName;
        }

        public async Task SendOtp(long? id, string email, string typeOfOtp)
        {
            User user = id.HasValue
                ? await GetFirstOrDefaultAsync(x => x.Id == id)
                : await GetFirstOrDefaultAsync(x => x.Email == email);

            Random generator = new();
            user.OTP = generator.Next(100000, 999999).ToString();
            user.ExpiryTime = DateTime.Now.AddMinutes(10);

            await UpdateAsync(user);

            string emailBody = MailBodyUtil.SendOtpForAuthenticationBody(user.OTP);

            await _mailService.SendMailAsync(new MailDto
            {
                ToEmail = email,
                Subject = MailConstants.OtpSubject,
                Body = emailBody
            });
        }

        public async Task<string> VerifyOtp(long? id, LoginOtpRequestDto otpData)
        {
            User user = id.HasValue
                ? await GetFirstOrDefaultAsync(x => x.Id == id)
                : await GetFirstOrDefaultAsync(x => x.Email == otpData.Email) ?? throw new CustomException(StatusCodes.Status404NotFound, ErrorMessages.USER_NOT_FOUND);

            if (user.OTP != otpData.Otp || user.ExpiryTime < DateTime.Now) throw new CustomException(StatusCodes.Status400BadRequest, MessageConstants.INVALID_OTP);

            user.OTP = null;
            user.ExpiryTime = null;
            await UpdateAsync(user);

            return _jwtManageService.GenerateToken(user) ?? throw new CustomException(StatusCodes.Status400BadRequest, MessageConstants.INVALID_ATTEMPT);

        }

        public async Task ForgotPassword(string email)
        {
            User user = await GetFirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                throw new CustomException(StatusCodes.Status404NotFound, ErrorMessages.USER_NOT_FOUND);
            }
            MailDto mailDto = new()
            {
                ToEmail = email,
                Subject = MailConstants.ResetPasswordSubject,
                Body = MailBodyUtil.SendResetPasswordLink("http://localhost:4200/reset-password?token=" + EncodingMailToken(email))
            };
            await _mailService.SendMailAsync(mailDto);

        }

        public async Task ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto)
        {
            if (String.IsNullOrEmpty(resetPasswordRequestDto.token)) throw new CustomException(StatusCodes.Status400BadRequest, MessageConstants.INVALID_TOKEN);
            DateTime dateTime = Convert.ToDateTime(DecodingMailToken(resetPasswordRequestDto.token).Split("&")[1]);
            if (dateTime < DateTime.UtcNow) throw new CustomException(StatusCodes.Status410Gone, MessageConstants.TOKEN_EXPIRE);

            string email = DecodingMailToken(resetPasswordRequestDto.token).Split("&")[0];
            User user = await GetFirstOrDefaultAsync(x => x.Email == email);
            user.Password = PasswordUtil.HashPassword(resetPasswordRequestDto.password);
            await UpdateAsync(user);
        }

        public static string EncodingMailToken(string email) => System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(email + "&" + DateTime.UtcNow.AddMinutes(10)));
        public static string DecodingMailToken(string token) => System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(token));
    }
}