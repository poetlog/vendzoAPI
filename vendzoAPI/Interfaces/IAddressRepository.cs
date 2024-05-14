namespace vendzoAPI.Interfaces
{
    public interface IAddressRepository
    {
        ICollection<Address> GetAll();

        Address Get(string id);

        ICollection<Address> GetByUser(string userId);

        bool AddressExists(string id);

        int UserAddressCount(string userId);

        bool UpdateAddress(Address address);

        bool DeleteAddress(Address address);

        bool CreateAddress(Address address);

        bool Save();

    }
}
