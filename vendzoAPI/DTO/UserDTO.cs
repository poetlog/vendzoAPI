namespace vendzoAPI.DTO
{
    public class UserDTO
    {
        public string? Id { get; set; }

        public string? Username { get; set; }

        public string? Pass { get; set; }

        public string? Email { get; set; }

        public string? CurrentAddress { get; set; }

        public string? ContactNo { get; set; }

        public bool IsClient { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
