using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class ProductValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductValidator()
        {
            RuleFor(product => product.NameAr)
                .NotEmpty().WithMessage("NameAr is required.")
                .MaximumLength(100).WithMessage("NameAr must not exceed 50 characters.");

            RuleFor(product => product.NameEn)
                .NotEmpty().WithMessage("NameEn is required.")
                .MaximumLength(100).WithMessage("NameEn must not exceed 50 characters.");

            RuleFor(product => product.ParentProductId)
                .GreaterThan(0).When(product => product.ParentProductId.HasValue)
                .WithMessage("ParentProductId must be greater than 0 if provided.");

            RuleFor(product => product.StoreId)
                .GreaterThan(0).WithMessage("StoreId must be greater than 0.");

            RuleFor(product => product.SalePrice)
                .GreaterThan(0).WithMessage("SalePrice must be greater than 0.");

            RuleFor(product => product.PurchasePrice)
                .GreaterThan(0).WithMessage("PurchasePrice must be greater than 0.");

            RuleFor(product => product.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(product => product.Barcode)
                .NotEmpty().WithMessage("Barcode is required.");

            RuleFor(product => product.ExtraBarcode)
                .MaximumLength(50).WithMessage("ExtraBarcode must not exceed 50 characters.")
                .When(product => !string.IsNullOrEmpty(product.ExtraBarcode));

            RuleFor(product => product.TypeId)
                .GreaterThan(0).WithMessage("TypeId must be greater than 0.");

            RuleFor(product => product.Photo)
                .NotEmpty().WithMessage("Photo is required.");
        }
    }
}
