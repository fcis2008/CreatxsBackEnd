using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CurrencyValidator : AbstractValidator<CurrencyCreateDto>
    {
        public CurrencyValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(c => c.Symbol)
                .NotEmpty().WithMessage("Symbol is required.")
                .MaximumLength(10).WithMessage("Symbol cannot exceed 10 characters.");

            RuleFor(c => c.ExchangeRate)
                .GreaterThan(0).WithMessage("Exchange rate must be greater than 0.");
        }
    }
}
