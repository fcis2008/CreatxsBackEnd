using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Product
{
    public int Id { get; set; }

    public string NameAr { get; set; } = null!;

    public string? NameEn { get; set; }

    public int? ParentProductId { get; set; }

    public int StoreId { get; set; }

    public double SalePrice { get; set; }

    public double PurchasePrice { get; set; }

    public string ProductId { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public string? ExtraBarcode { get; set; }

    public int TypeId { get; set; }

    public string Photo { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsPublish { get; set; }
}
