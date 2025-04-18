using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Store
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? OwnerName { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? WhatsAppNumber { get; set; }

    public string? Link { get; set; }

    public string? AccountPhoneNumber { get; set; }

    public string? Password { get; set; }

    public string? Photo { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
