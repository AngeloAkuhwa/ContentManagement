using ContentManagement.Application.DataTransfer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainMarket.Application.Validations.ModelValidations
{
    public class PhoneNumberValidator : AbstractValidator<AddPhoneNumberDTO>
    {
        public PhoneNumberValidator()
        {
            RuleFor(model => model.CountryCode)
                  .Must(val => val != null && val.Contains('+'))
                  .WithMessage("Value can not be empty and must contain a +");

            RuleFor(model => model.FirstName)
                .NotEmpty()
                .WithMessage("Name field is required");

            RuleFor(model => model.LastName)
                .Must(val => val != null)
                .NotEmpty()
                .WithMessage("Name field is required");

            RuleFor(model => model.Number)
                .Must(val => val != null)
                .NotEmpty()
                .WithMessage("Phone nUmber field is required");
        }
    }
}