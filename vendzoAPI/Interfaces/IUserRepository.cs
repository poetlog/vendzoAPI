using Microsoft.AspNetCore.Mvc;

namespace vendzoAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUserById(string id);
        User GetUserByEmail(string email);
        User GetUserByUsername(string username);
        bool UserExists(string id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        public bool AddAddressToUser(User user, Address address, bool saveFlag);
        public bool SetDefaultAddressOfUser(User user, Address address, bool saveFlag);
        bool Save();
        bool UserExistsByEmail(string email);
    }
}
