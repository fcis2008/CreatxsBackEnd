using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Branch
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ManagerName { get; set; }

    public string? PhoneNumber { get; set; }

    public int CityId { get; set; }

    public int DistrictId { get; set; }

    public string? Address { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int StoreId { get; set; }

    public bool IsPublish { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
