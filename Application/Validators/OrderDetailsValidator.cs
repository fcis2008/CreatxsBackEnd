using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class OrderDetailsValidator : AbstractValidator<OrderDetailsCreateDto>
    {
        public OrderDetailsValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");
        }
    }
}
