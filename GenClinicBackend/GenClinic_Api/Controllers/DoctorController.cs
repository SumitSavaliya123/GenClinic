using GenClinic_Api.Helpers;
using GenClinic_Common.Constants;
using GenClinic_Common.Utils;
using GenClinic_Entities.DTOs.Request;
using GenClinic_Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GenClinic_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtManageService _jwtManageService;
        public DoctorController(IUserService userService, IJwtManageService jwtManageService)
        {
            _userService = userService;
            _jwtManageService = jwtManageService;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequestDto registerUserRequestDto)
        {
            LoggedUser loggedUser = _jwtManageService.GetLoggedUser();

            long userId = await _userService.RegisterAsync(registerUserRequestDto, loggedUser);

            return ResponseHelper.CreatedResponse(userId, MessageConstants.ACCOUNT_CREATED);
        }
    }
}