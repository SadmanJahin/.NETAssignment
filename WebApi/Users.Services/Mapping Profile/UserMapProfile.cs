using AutoMapper;
using Users.Core.Entities;
using WebApi.Shared.Models;


namespace Users.Application.Mapping_Profile
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
