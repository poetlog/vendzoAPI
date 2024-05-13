using System;
using System.Collections.Generic;

namespace vendzoAPI.Models;

public partial class Item
{
    public string Id { get; set; } = null!;

    public string? SellerId { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }

    public string? Category { get; set; }

    public decimal? Price { get; set; }

    public string? Photo { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual User? Seller { get; set; }
}
