using vendzoAPI.Interfaces;

namespace vendzoAPI.Repository
{
    public class AddressRepository : IAddressRepository
    {

        private readonly VendzoContext _context;

        public AddressRepository(VendzoContext context) 
        {
            _context = context;
        }

        public bool AddressExists(string id)
        {
            return _context.Addresses.Any(a => a.Id == id);
        }

        public bool CreateAddress(Address address)
        {
            _context.Add(address);

            return Save();
        }

        public bool DeleteAddress(Address address)
        {
            _context.Remove(address);
            return Save();
        }

        public Address Get(string id)
        {
            return _context.Addresses.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Address> GetAll()
        {
            return _context.Addresses.ToList();
        }

        public ICollection<Address> GetByUser(string userId)
        {
            return _context.Addresses.Where(p => p.UserId == userId && p.IsDeleted == false ).ToList();
        }

        public bool Save()
        {
            var saved= _context.SaveChanges();

            return saved > 0; 
        }

        public bool UpdateAddress(Address address)
        {
            _context.Update(address);

            return Save();
        }

        public int UserAddressCount(string userId)
        {
            return _context.Addresses.Where(p => p.UserId == userId).Count();
        }

        
    }
}
