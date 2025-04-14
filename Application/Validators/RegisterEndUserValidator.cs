using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class RegisterEndUserValidator : AbstractValidator<RegisterEndUserDto>
    {
        public RegisterEndUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(x => x.Password).WithMessage("Phone number is required.");
        }
    }
}
