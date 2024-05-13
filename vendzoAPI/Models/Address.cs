using System;
using System.Collections.Generic;

namespace vendzoAPI.Models;

public partial class Address
{
    public string Id { get; set; } = null!;

    public string? UserId { get; set; }

    public string? ContactNo { get; set; }

    public string? Address1 { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
