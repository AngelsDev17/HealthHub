using Angels.Packages.MongoDb.Enums;
using AutoMapper;
using HealthHub.Application.Dtos.Commons;
using HealthHub.Application.Dtos.UserManagement;
using HealthHub.Domain.Models.UserManagement;

namespace HealthHub.BusinessLogic.Mappers.UserManagement;

public class UserManagementProfile : Profile
{
    public UserManagementProfile()
    {
        AllowNullCollections = true;

        CreateMap<IdentificationDto, Identification>().ReverseMap();

        CreateMap<UserInformationDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Status.Active))
            .ForPath(dest => dest.Activation.IsActivated, opt => opt.MapFrom(src => false))
            .ReverseMap();

        CreateMap<UserToUpdateDto, User>()
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Identification, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.Activation, opt => opt.Ignore());

        CreateMap<User, FlatUserInformationDto>();
    }
}
