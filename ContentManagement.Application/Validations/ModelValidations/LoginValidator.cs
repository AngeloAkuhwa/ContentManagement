using CloudinaryImageCrudHandler.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentManagement.Application.Validations.ModelValidations
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(model => model.Email)
                .NotEmpty()
                .WithMessage("email field is required");

            RuleFor(model => model.Password)
                .NotEmpty()
                .WithMessage("password field is required");
        }
    }
}
