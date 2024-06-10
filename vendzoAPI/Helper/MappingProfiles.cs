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

            CreateMap<Item, ItemDTO>();
            CreateMap<ItemDTO, Item>();

            CreateMap<Basket, BasketDTO>();
            CreateMap<BasketDTO, Basket>();
            CreateMap<Basket, BasketDetailsDTO>();

            CreateMap<Promotion, PromotionDTO>();
            CreateMap<PromotionDTO, Promotion>();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();

            CreateMap<OrderEntry, OrderEntryDTO>();
            CreateMap<OrderEntryDTO, OrderEntry>();

            CreateMap<RegisterDTO, User>();

        }
    }
}
