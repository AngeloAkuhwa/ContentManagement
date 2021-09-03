using AutoMapper;
using ContactManagement.Domain.Entities;
using ContentManagement.Application.Contracts;
using ContentManagement.Application.Contracts.IRepositories;
using ContentManagement.Application.CustomExceptions;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Domain.Commons;
using ContentManagement.Identity;
using MainMarket.Application.Validations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Infrastructure.ServiceImplementations
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IPhoneNumberRepository _phoneRepo;
        private readonly IMapper _mapper;
        public PhoneNumberService(IServiceProvider provider)
        {
            _phoneRepo = provider.GetRequiredService<IPhoneNumberRepository>();
            _mapper = provider.GetRequiredService<IMapper>();
        }

      
        public  Response<List<AddPhoneNumberDTO>> RetrievePhoneNumberByName(string fullName)
        {
            var response = new Response<List<AddPhoneNumberDTO>>();

            List<PhoneNumber> retrievePhone = _phoneRepo.FilterName(fullName);
            var result = _mapper.Map<List<AddPhoneNumberDTO>>(retrievePhone);

            if(retrievePhone.Count() > 0)
            {
                response.Data = result;
                response.Message = "successfully retrieved phoneNumbers";
                response.StatusCode = 200;
                response.Success = true;
            }

            return response;
        }

        public async Task<Response<AddPhoneNumberDTO>> StorePhoneNumber(AppUser loggedInUser, AddPhoneNumberDTO dto)
        {

            var validity = await ValidateHelper.PhoneNumberValidator(dto);
            var hasValidationErrors = validity.FirstOrDefault(e => e.HasValidationErrors);

            if (hasValidationErrors != null)
            {
                throw new BadRequestException(validity, "Request object does not contain the required data");
            }

            Response<AddPhoneNumberDTO> response = new Response<AddPhoneNumberDTO>();

            if (loggedInUser != null)
            {
                var phoneNumberToStore = _mapper.Map<PhoneNumber>(dto);
                phoneNumberToStore.FullName = phoneNumberToStore.FirstName + phoneNumberToStore.LastName;
                phoneNumberToStore.UserId = loggedInUser.Id;
                phoneNumberToStore.IsAvailable = true;

                var isAlreadyExist = await IsPhoneNumberAlreadyExist(phoneNumberToStore.Id);

                if (isAlreadyExist.Success)
                {
                    response.Message = "phoneNumber already exist";
                    response.Data = null;
                    response.StatusCode = 409;
                    return response;
                }

                var storageResult = await _phoneRepo.Insert(phoneNumberToStore);

                if (!storageResult)
                {
                    response.Data = null;
                    response.StatusCode = 400;
                    response.Message = "oops... something went wrong";
                    response.Success = false;
                    return response;
                }

                var updatedPhone = await _phoneRepo.Get(phoneNumberToStore.Id);
                response.Data = _mapper.Map<AddPhoneNumberDTO>(updatedPhone);
                response.StatusCode = 201;
                response.Message = "phone Number stored successfully";
                response.Success = true;
                return response;
            }

            throw new Exception("user and phone number can not be null");
        }

        public async Task<Response<PhoneNumber>> IsPhoneNumberAlreadyExist(string PhoneNumberId)
        {
            Response<PhoneNumber> response = new Response<PhoneNumber>();
            var phoneNumber = await _phoneRepo.Get(PhoneNumberId);
            if(phoneNumber != null)
            {
                response.Data = phoneNumber;
                response.Message = "phoneNumber already exist";
                response.StatusCode = 409;
                response.Success = true;
                return response;
            }

            response.Data = null;
            response.Message = "phoneNumber does not exist";
            response.StatusCode = 409;
            response.Success = false;
            return response;
        }

        public async Task<Response<bool>> DeletePhoneNumberByName(AppUser user, string fullName)
        {
            Response<bool> response = new Response<bool>();

            var phoneToDelete =  _phoneRepo.GetBySpecificName(fullName);

            if(phoneToDelete.FirstName != null && user.Id == phoneToDelete.UserId)
            {
                var deleteResult = await _phoneRepo.DeleteByName(phoneToDelete.FullName);

                if (!deleteResult)
                {
                    response.Data = false;
                    response.StatusCode = 404;
                    response.Message = "something went wrong, failed to delete phoneNumber";
                    response.Success = false;
                    return response;
                }

                response.Data = true;
                response.StatusCode = 200;
                response.Message = "phoneNumber deleted successfully";
                response.Success = true;
                return response;
            }
            throw new Exception("user does not exist");
        }

       

        public async Task<Response<bool>> UpdatePhoneNumber(AppUser user, string phoneNumberId, AddPhoneNumberDTO dto)
        {
            Response<bool> response = new Response<bool>();

            var validity = await ValidateHelper.PhoneNumberValidator(dto);
            var hasValidationErrors = validity.FirstOrDefault(e => e.HasValidationErrors);

            if (hasValidationErrors != null)
            {
                throw new BadRequestException(validity, "Request object does not contain the required data");
            }

            var phone =await  _phoneRepo.Get(phoneNumberId);

            if (phone.FirstName != null && user.Id == phone.UserId)
            {
                
                var phoneNumberToUpdate = _mapper.Map(dto,phone);
                phoneNumberToUpdate.IsAvailable = true;

                var updateResult = await _phoneRepo.Update(phoneNumberToUpdate);

                if (!updateResult)
                {
                    response.Data = false;
                    response.StatusCode = 404;
                    response.Message = "something went wrong, failed to update phoneNumber";
                    response.Success = false;
                    return response;
                }

                response.Data = true;
                response.StatusCode = 200;
                response.Message = "phoneNumber updated successfully";
                response.Success = true;
                return response;
            }
            throw new Exception("user does not exist");
        }

        public async Task<Response<bool>> DeleteMultiplePhoneNumbers(AppUser user, string[] phoneNumberIds)
        {
            Response<bool> response = new Response<bool>();

            int counter = 0;
            var availbalePhoneNumberIds = _phoneRepo.GetAll().Select(x => x.Id).ToList();
            var allUserIds = _phoneRepo.GetAll().Select(x => x.UserId).ToList();

            if (allUserIds.Contains(user.Id))
            {
                for (int i = 0; i < phoneNumberIds.Length; i++)
                {
                    if (availbalePhoneNumberIds.Contains(phoneNumberIds[i]))
                    {
                        counter++;
                    }
                }
            }

           


            if (counter == phoneNumberIds.Length)
            {
                int deleteResult = await _phoneRepo.DeleteRange(phoneNumberIds);

                if (deleteResult != phoneNumberIds.Length)
                {
                    throw new Exception("oops something went wrong");
                }

                response.Data = true;
                response.Message = "all phone numbers deleted successfully";
                response.Success = true;
                return response;
                    
            }



            throw new Exception("oops something went wrong");
        }
    }
}
