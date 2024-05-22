namespace vendzoAPI.DTO
{
    public class OrderDTO
    {
        public string Id { get; set; }

        public string? UserId { get; set; }

        public string? SellerId { get; set; }

        public DateTimeOffset? OrderDate { get; set; }

        public DateTimeOffset? ShipDate { get; set; }

        public DateTimeOffset? DeliverDate { get; set; }

        public string? ShipAddress { get; set; }

        public string? TrackingNo { get; set; }

        public string? Status { get; set; }

        public decimal? Total { get; set; }
    }
}
