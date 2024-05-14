using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace vendzoAPI.Models;

public partial class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string? UserId { get; set; }

    public string? ContactNo { get; set; }

    public string? Address1 { get; set; }

    public virtual User? User { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
