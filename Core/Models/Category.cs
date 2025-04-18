using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Order { get; set; }

    public int StoreId { get; set; }

    public string Photo { get; set; } = null!;

    public bool IsPublish { get; set; }

    public string? RedirectTo { get; set; }

    public virtual Store Store { get; set; } = null!;
}
