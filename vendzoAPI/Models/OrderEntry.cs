using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vendzoAPI.Models

{
    public class OrderEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string OrderId { get; set; }

        [Required]
        public string BuyerId { get; set; }

        [Required]
        public string ItemId { get; set; }

        [Required]
        public string SellerId { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public string ItemTitle { get; set; }

        [Required]
        public string SellerName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public virtual User Buyer { get; set; }

        [ForeignKey(nameof(SellerId))]
        public virtual User Seller { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual Item Item { get; set; }
    }
}
