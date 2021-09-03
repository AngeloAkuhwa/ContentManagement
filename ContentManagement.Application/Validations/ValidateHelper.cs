using CloudinaryImageCrudHandler.DTO;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Application.Validations.ModelValidations;
using ContentManagement.Domain.Commons;
using FluentValidation.Results;
using MainMarket.Application.Validations.ModelValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMarket.Application.Validations
{
   public class ValidateHelper
    {
        public static async Task<List<Error>> LoginValidator(LoginDTO model)
        {
            var validator = new LoginValidator();
            var result = await validator.ValidateAsync(model);
            return ErrorsValidated(result);
        }
       

        public static async Task<List<Error>> CreateUserValidator(CreateUserDTO model)
        {
            var validator = new CreateUserValidator();
            var result = await validator.ValidateAsync(model);

            return ErrorsValidated(result);
        }

        public static async Task<List<Error>> AddressValidator(AddAddressDTO model)
        {
            var validator = new AddressValidator();
            var result = await validator.ValidateAsync(model);

            return ErrorsValidated(result);
        }

        public static async Task<List<Error>> PhoneNumberValidator(AddPhoneNumberDTO model)
        {
            var validator = new PhoneNumberValidator();
            var result = await validator.ValidateAsync(model);

            return ErrorsValidated(result);
        }

        public static async Task<List<Error>> ImageValidator(AddImageDTO model)
        {
            var validator = new ImageValidator();
            var result = await validator.ValidateAsync(model);

            return ErrorsValidated(result);
        }


        private static List<Error> ErrorsValidated(ValidationResult results)
        {
            List<Error> validationErrors = new List<Error>();

            if (!results.IsValid)
            {
                validationErrors.AddRange(results.Errors.Select(error => new Error
                {
                    PropertyName = error.PropertyName,
                    PropertyValue = error.ErrorMessage,
                    HasValidationErrors = true
                }));
            }

            return validationErrors;
        }
    }
}
