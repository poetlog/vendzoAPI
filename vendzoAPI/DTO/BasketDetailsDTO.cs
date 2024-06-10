namespace vendzoAPI.DTO
{
    public class BasketDetailsDTO
    {
        public string? Id { get; set; }

        public int? Quantity { get; set; }

        public string? UserId { get; set; }

        public string? ItemId { get; set; }

        public string? ItemName { get; set; }

        public int? ItemStock { get; set; }

        public decimal? ItemPrice { get; set; }

    }
}
