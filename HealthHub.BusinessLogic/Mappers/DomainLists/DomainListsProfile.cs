using Angels.Packages.MongoDb.Models;
using AutoMapper;
using HealthHub.Application.Dtos.Commons;
using HealthHub.Domain.Models.UserManagement;

namespace HealthHub.BusinessLogic.Mappers.AuthService;

public class DomainListsProfile : Profile
{
    public DomainListsProfile()
    {
        AllowNullCollections = true;

        CreateMap<ReferencedValueDto, ReferencedValue>().ReverseMap();
        CreateMap<IdentificationDto, Identification>().ReverseMap();
    }
}
