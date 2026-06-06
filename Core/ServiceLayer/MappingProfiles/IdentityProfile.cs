using AutoMapper;
using DomainLayer.Models.IdetityModule;
using Shared.DTOs.Identity;

namespace ServiceImplementation.MappingProfiles;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}
