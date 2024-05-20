namespace vendzoAPI.DTO
{
    public class PromotionDTO
    {
        public string? Id { get; set; }

        public string? PromoCode { get; set; }

        public decimal? Amount { get; set; }

        public DateTimeOffset? Expires { get; set; }

        public string? Type { get; set; }
    }
}
