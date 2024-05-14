using AutoMapper;
using vendzoAPI.DTO;

namespace vendzoAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
        }
    }
}
