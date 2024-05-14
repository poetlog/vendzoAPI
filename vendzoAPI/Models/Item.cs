using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace vendzoAPI.Models;

public partial class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

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
