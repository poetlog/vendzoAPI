using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace vendzoAPI.DTO
{
    public class OrderEntryDTO
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? BuyerId { get; set; }
        public string? ItemId { get; set; }
        public string? SellerId { get; set; }
        public string? Photo { get; set; }
        public string? ItemTitle { get; set; }
        public string? SellerName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
