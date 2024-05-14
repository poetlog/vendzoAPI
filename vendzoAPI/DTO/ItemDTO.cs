namespace vendzoAPI.DTO
{
    public class ItemDTO
    {
        public string? Id { get; set; }

        public string? SellerId { get; set; }

        public string? Description { get; set; }

        public string? Title { get; set; }

        public string? Category { get; set; }

        public decimal? Price { get; set; }

        public string? Photo { get; set; }

        public int? Stock { get; set; }
    }
}
