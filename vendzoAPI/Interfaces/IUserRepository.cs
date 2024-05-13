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
        bool Save();
        
    }
}
