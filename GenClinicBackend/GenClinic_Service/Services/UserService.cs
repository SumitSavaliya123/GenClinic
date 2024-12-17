using AutoMapper;
using GenClinic_Common.Constants;
using GenClinic_Common.Enums;
using GenClinic_Common.Exceptions;
using GenClinic_Common.Utils;
using GenClinic_Entities.DataModels;
using GenClinic_Entities.DTOs.Request;
using GenClinic_Repository.IRepositories;
using GenClinic_Service.IServices;
using Microsoft.AspNetCore.Http;

namespace GenClinic_Service.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;


        public UserService(IUserRepository userRepository, IMapper mapper, IMailService mailService) : base(userRepository)
        {
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<long> RegisterAsync(RegisterUserRequestDto registerUserRequestDto,
               LoggedUser loggedUser)
        {
            User? user = await GetFirstOrDefaultAsync(user => user.Email == registerUserRequestDto.Email);
            if (user != null)
                throw new CustomException(StatusCodes.Status403Forbidden, ErrorMessages.USER_ALREADY_EXISTS);

            User mappedUser = _mapper.Map<RegisterUserRequestDto, User>(registerUserRequestDto);
            mappedUser.DoctorId = loggedUser.UserId;
            if (registerUserRequestDto.IsPatient == true)
            {
                mappedUser.Role = UserRole.Patient;
            }
            else
            {
                mappedUser.Role = UserRole.Lab;
            }

            await AddAsync(mappedUser);
            await SaveAsync();
            return mappedUser.Id;
        }
    }
}