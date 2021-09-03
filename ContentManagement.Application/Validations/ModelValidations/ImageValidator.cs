using CloudinaryImageCrudHandler.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentManagement.Application.Validations.ModelValidations
{
    public class ImageValidator: AbstractValidator<AddImageDTO>
    {
        public ImageValidator()
        {
            RuleFor(model => model.Image)
                .Must(x => x != null)
                .NotEmpty()
                .WithMessage("image field can is required");
        }
    }
}
