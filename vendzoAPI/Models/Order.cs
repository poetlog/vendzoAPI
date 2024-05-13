using System;
using System.Collections.Generic;

namespace vendzoAPI.Models;

public partial class Order
{
    public string Id { get; set; } = null!;

    public string? UserId { get; set; }

    public DateTimeOffset? OrderDate { get; set; }

    public DateTimeOffset? ShipDate { get; set; }

    public DateTimeOffset? DeliverDate { get; set; }

    public string? ShipAddress { get; set; }

    public string? TrackingNo { get; set; }

    public string? Status { get; set; }

    public decimal? Total { get; set; }

    public virtual User? User { get; set; }
}
