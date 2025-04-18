namespace Application.DTOs
{
    public class ProductCreateDto
    {
        public string NameAr { get; set; }

        public string NameEn { get; set; }

        public int? ParentProductId { get; set; }

        public int StoreId { get; set; }

        public double SalePrice { get; set; }

        public double PurchasePrice { get; set; }

        public string ProductId { get; set; } = null!;

        public string Barcode { get; set; } = null!;

        public string? ExtraBarcode { get; set; }

        public int TypeId { get; set; }

        public string Photo { get; set; } = null!;

        public bool IsPublish { get; set; }
    }

    public class ProductDto : ProductCreateDto
    {
        public int Id { get; set; }
    }
}
