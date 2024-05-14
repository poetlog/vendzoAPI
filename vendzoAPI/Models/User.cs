using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace vendzoAPI.Models;

public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string? Username { get; set; }

    public string? Pass { get; set; }

    public string? Email { get; set; }

    public string? CurrentAddress { get; set; }

    public string? ContactNo { get; set; }

    public bool IsClient { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual Address? CurrentAddressNavigation { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    

}
