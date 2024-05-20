using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace vendzoAPI.Models;

public partial class Basket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public int? Quantity { get; set; }

    public string? UserId { get; set; }

    public string? ItemId { get; set; }

    public virtual Item? Item { get; set; }

    public virtual User? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }
}
