using CloudinaryImageCrudHandler.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentManagement.Application.Validations.ModelValidations
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidator()
        {
            RuleFor(model => model.Email)
                .NotEmpty()
                .WithMessage("email field is required");

            RuleFor(model => model.FirstName)
               .NotEmpty()
               .WithMessage("Firstname field is required");

            RuleFor(model => model.LastName)
               .NotEmpty()
               .WithMessage("LastName field is required");

            RuleFor(model => model.Password)
               .NotEmpty()
               .WithMessage("password field is required");

            RuleFor(model => model.PhoneNumber)
               .NotEmpty()
               .WithMessage("Phone Number field is required");

            RuleFor(model => model.Gender)
               .NotEmpty()
               .WithMessage("Gender field is required");
        }
    }
}
