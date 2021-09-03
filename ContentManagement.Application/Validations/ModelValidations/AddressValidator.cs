using ContentManagement.Application.DataTransfer;
using FluentValidation;

namespace MainMarket.Application.Validations.ModelValidations
{
    public class AddressValidator : AbstractValidator<AddAddressDTO>
    {
        public AddressValidator()
        {
            RuleFor(model => model.City)
                .Must(x => x != null)
                .NotEmpty()
                .WithMessage("City Field is required");

            RuleFor(model => model.Country)
                .Must(x => x != null)
                .NotEmpty()
                .WithMessage("Country field is required");

            RuleFor(model => model.StreetInfo)
               .Must(x => x != null)
               .NotEmpty()
               .WithMessage("HouseNumber field is required");

            RuleFor(model => model.State)
                .Must(x => x != null)
               .NotEmpty()
               .WithMessage("State field is required");
        }
    }
}
