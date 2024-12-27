using AutoMapper;
using GenClinic_Common.Constants;
using GenClinic_Common.Exceptions;
using GenClinic_Entities.DataModels;
using GenClinic_Entities.DTOs.Request;
using GenClinic_Entities.DTOs.Response;
using GenClinic_Repository.IRepositories;
using GenClinic_Service.IServices;
using Microsoft.AspNetCore.Http;

namespace GenClinic_Service.Services
{
    public class ProfileService : BaseService<User>, IProfileService
    {
        #region Properties
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;

        #endregion Properties

        #region Constructors

        public ProfileService(IMapper mapper, IProfileRepository profileRepository) : base(profileRepository)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
        }

        #endregion

        public async Task<UserDetailsInfoDto> GetProfileDetails(long id)
        {
            User? user = await GetFirstOrDefaultAsync(user => user.Id == id);

            UserDetailsInfoDto userDetailsInfoDTO = _mapper.Map<UserDetailsInfoDto>(user);

            if (user.Avatar != null)
            {
                byte[]? byteData = user.Avatar;

                string imgBase64Data = Convert.ToBase64String(byteData);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imgBase64Data);

                userDetailsInfoDTO.Avatar = imgDataURL;
            }
            return userDetailsInfoDTO;
        }

        public async Task UpdateUserProfile(long id, ProfileDetailsRequestDto profileDetailsDto)
        {
            User? user = await GetFirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception(ErrorMessages.USER_NOT_FOUND);
            bool isEmailExist = await _profileRepository.IsDuplicateEmail(profileDetailsDto.Email, id);

            if (isEmailExist)
                throw new CustomException(StatusCodes.Status403Forbidden, ErrorMessages.EMAIL_ALREADY_EXIST);

            _mapper.Map(profileDetailsDto, user);

            if (profileDetailsDto.Avatar != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await profileDetailsDto.Avatar.CopyToAsync(memoryStream);
                    user.Avatar = memoryStream.ToArray();
                }
            }

            await UpdateAsync(user);
        }
    }
}