using System.Linq.Expressions;
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
                await AdditionalNewPatientDetails(loggedUser, mappedUser);
            }
            else
            {
                mappedUser.Role = UserRole.Lab;
            }

            await AddAsync(mappedUser);
            await SaveAsync();
            return mappedUser.Id;
        }

        private async Task AdditionalNewPatientDetails(LoggedUser loggedUser,
       User user)
        {
            user.LabId = await GetLabIdForDoctor(loggedUser.UserId);

            if (user.LabId is null)
                throw new CustomException(StatusCodes.Status404NotFound, ErrorMessages.LAB_USER_NOT_FOUND);

            user.ConsultationStatus = PatientConsultationStatus.New;
        }

        private async Task<long?> GetLabIdForDoctor(long doctorId)
        {
            User? model = await GetFirstOrDefaultAsync(DoctorLabUserFilter(doctorId));
            return model?.Id;
        }

        private static Expression<Func<User, bool>> DoctorLabUserFilter(long doctorId)
       => user => user.Role == UserRole.Lab && user.DoctorId == doctorId;
    }
}