using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace vendzoAPI.Models;

public partial class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string? UserId { get; set; }

    //public string? SellerId { get; set; }

    public DateTimeOffset? OrderDate { get; set; }

    public DateTimeOffset? ShipDate { get; set; }

    public DateTimeOffset? DeliverDate { get; set; }

    public string? ShipAddress { get; set; }

    public string? TrackingNo { get; set; }

    public string? Status { get; set; }

    public decimal? Total { get; set; }

    public virtual User? User { get; set; }

    //public virtual User? Seller { get; set; }

    public virtual ICollection<OrderEntry> OrderEntries { get; set; } = new List<OrderEntry>();

}
