using GenClinic_Api.Helpers;
using GenClinic_Common.Constants;
using GenClinic_Entities.DTOs.Request;
using GenClinic_Entities.DTOs.Response;
using GenClinic_Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GenClinic_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        #region Properties

        private readonly IProfileService _profileService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtManageService _jwtManageService;

        #endregion

        #region Constructor
        public ProfileController(IProfileService profileService, IAuthenticationService authenticationService, IJwtManageService jwtManageService)
        {
            _profileService = profileService;
            _authenticationService = authenticationService;
            _jwtManageService = jwtManageService;
        }

        #endregion

        [HttpGet("getProfileDetails/{id}")]
        public async Task<IActionResult> GetProfileDetails(long id)
        {
            UserDetailsInfoDto userDetailsInfoDto = await _profileService.GetProfileDetails(id);

            return ResponseHelper.SuccessResponse(userDetailsInfoDto);
        }

        [HttpPut("updateProfileDetails/{id}")]
        public async Task<IActionResult> Update(long id, [FromForm] ProfileDetailsRequestDto profileDetailsRequestDto)
        {

            await _profileService.UpdateUserProfile(id, profileDetailsRequestDto);

            return ResponseHelper.SuccessResponse(profileDetailsRequestDto, MessageConstants.ProfileUpdated);
        }
    }


}