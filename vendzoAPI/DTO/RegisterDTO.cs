namespace vendzoAPI.DTO
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsClient { get; set; }
    }
}
