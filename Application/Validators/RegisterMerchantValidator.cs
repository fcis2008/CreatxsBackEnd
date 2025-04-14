using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class RegisterMerchantValidator : AbstractValidator<RegisterMerchantDto>
    {
        public RegisterMerchantValidator()
        {
            RuleFor(x => x.StoreName).NotEmpty().WithMessage("Store name is required.").Length(5, 50).WithMessage("Store name must be between 5 and 50 characters.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(x => x.Password).WithMessage("Phone number is required.");
        }
    }
}
