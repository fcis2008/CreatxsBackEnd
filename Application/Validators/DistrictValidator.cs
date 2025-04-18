using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class DistrictValidator : AbstractValidator<DistrictCreateDto>
    {
        public DistrictValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        }
    }
}
