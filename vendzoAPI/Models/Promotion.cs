using System;
using System.Collections.Generic;

namespace vendzoAPI.Models;

public partial class Promotion
{
    public string Id { get; set; } = null!;

    public string? PromoCode { get; set; }

    public decimal? Amount { get; set; }

    public DateTimeOffset? Expires { get; set; }

    public string? Type { get; set; }
}
