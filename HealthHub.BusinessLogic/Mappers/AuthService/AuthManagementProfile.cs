using Angels.Packages.MongoDb.Enums;
using AutoMapper;
using HealthHub.Application.Dtos.AuthService;
using HealthHub.Application.Dtos.UserManagement;
using HealthHub.Domain.Models.AuthService;

namespace HealthHub.BusinessLogic.Mappers.AuthService;

public class AuthManagementProfile : Profile
{
    public AuthManagementProfile()
    {
        AllowNullCollections = true;

        var random = new Random();

        CreateMap<UserToRegisterDto, AuthUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            .ForMember(dest => dest.SecretId, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Status.Inactive))
            .ForPath(dest => dest.Activation.ActivationId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForPath(dest => dest.Activation.ActivationCode, opt => opt.MapFrom(src => random.Next(100000, 999999)));

        CreateMap<UserToRegisterDto, UserInformationDto>();
    }
}
