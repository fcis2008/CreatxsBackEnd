using Core.Models;
using FluentValidation;

namespace Application.Validators
{
    public class BranchValidator : AbstractValidator<Branch>
    {
        public BranchValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Branch name is required.");
            RuleFor(b => b.CityId).GreaterThan(0).WithMessage("CityId must be greater than 0.");
            RuleFor(b => b.DistrictId).GreaterThan(0).WithMessage("DistrictId must be greater than 0.");
            RuleFor(b => b.StoreId).GreaterThan(0).WithMessage("StoreId must be greater than 0.");
            RuleFor(b => b.Latitude).InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");
            RuleFor(b => b.Longitude).InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
        }
    }
}
