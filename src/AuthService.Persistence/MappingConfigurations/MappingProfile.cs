using AuthService.Application.Data.DTOs.Request;
using AuthService.Core.Models;
using AutoMapper;

namespace AuthService.Persistence.MappingConfigurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequestDTO, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Roles, opt => opt.Ignore());
    }
}
