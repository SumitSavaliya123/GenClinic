using AutoMapper;
using GenClinic_Common.Utils.Models;
using GenClinic_Entities.DataModels;
using GenClinic_Entities.DTOs.Request;

namespace GenClinic_Service.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserRequestDto, User>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordUtil.HashPassword(src.Password)));
        }
    }
}