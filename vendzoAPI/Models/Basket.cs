using System;
using System.Collections.Generic;

namespace vendzoAPI.Models;

public partial class Basket
{
    public string Id { get; set; } = null!;

    public int? Quantity { get; set; }

    public string? UserId { get; set; }

    public string? ItemId { get; set; }

    public virtual Item? Item { get; set; }

    public virtual User? User { get; set; }
}
