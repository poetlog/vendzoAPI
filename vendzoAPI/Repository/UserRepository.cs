using vendzoAPI.Interfaces;

namespace vendzoAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly VendzoContext _context;

        public UserRepository(VendzoContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);

            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(p => p.Email == email).FirstOrDefault();
        }

        public User GetUserById(string id)
        {
            return _context.Users.Where(p => p.Id == id).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.Where(p => p.Username == username).FirstOrDefault();

        }

        public ICollection<User> GetUsers() {
            
            return _context.Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(string id)
        {
            return _context.Users.Any(p => p.Id == id);
        }

        public bool AddAddressToUser(User user, Address address, bool saveFlag)
        {
            user.Addresses.Add(address);

            if (user.CurrentAddress == null)
            {
                SetDefaultAddressOfUser(user, address,false);
            }

            if (saveFlag)
                return Save();
            return true;
        }

        public bool SetDefaultAddressOfUser(User user, Address address, bool saveFlag)
        {
            user.CurrentAddressNavigation = address;
            user.CurrentAddress = address.Id;
            if (saveFlag)
                return Save();
            return true;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UserExistsByEmail(string email)
        {
            return _context.Users.Any(p => p.Email == email);
        }

        public bool UserExistsByUsername(string username)
        {
            return _context.Users.Any(p => p.Username == username);
        }
    }
}
