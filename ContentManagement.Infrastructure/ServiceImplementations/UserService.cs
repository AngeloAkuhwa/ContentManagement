using AutoMapper;
using CloudinaryImageCrudHandler.DTO;
using ContentManagement.Application.Contracts;
using ContentManagement.Application.CustomExceptions;
using ContentManagement.Domain.Commons;
using ContentManagement.Identity;
using FacilityManagement.Services.API.Policy;
using MainMarket.Application.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagement.Infrastructure.ServiceImplementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userMgr;
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;
        private readonly IIMageService _imageService;

        public UserService(IServiceProvider provider)
        {
            _userMgr = provider.GetRequiredService<UserManager<AppUser>>();
            _mapper = provider.GetRequiredService<IMapper>();
            _jwtService = provider.GetRequiredService<IJWTService>();
            _imageService = provider.GetRequiredService<IIMageService>();
        }

        public async Task<Response<CreateUserDTO>> RegisterUserAsyn(CreateUserDTO model)
        {
            var response = new Response<CreateUserDTO>();
            model.IsProfileCompleted = false;
            model.PublicId = null;
            model.AvatarUrl = null;

            var validity = await ValidateHelper.CreateUserValidator(model);
            var hasValidationErrors = validity.FirstOrDefault(e => e.HasValidationErrors);

            if (hasValidationErrors != null)
            {
                throw new BadRequestException(validity, "Request object does not contain the required data");
            }


            var result = await IsUserAlreadyExistAsync(model.Email);

            if (!result.Success)
            {
                var createdUserDetails = await CreateUserAsync(model);

                return createdUserDetails;
            }


            response.Success = false;
            response.Data = null;
            response.StatusCode = 404;
            response.Message = "ooops! something went wrong, Registration failed";
            response.Success = false;

            return response;
        }


        public async Task<Response<CreateUserDTO>> CreateUserAsync(CreateUserDTO model)
        {
            var response = new Response<CreateUserDTO>();

            var userToCreate = _mapper.Map<AppUser>(model);
            IdentityResult result = await _userMgr.CreateAsync(userToCreate, model.Password);
            List<IdentityError> errorList = result.Errors.ToList();

            if (result.Succeeded)
            {
                var userToReturn = await _userMgr.FindByEmailAsync(model.Email);
                var createRole = await _userMgr.AddToRoleAsync(userToReturn, Policies.GeneralUser);

                response.Data = _mapper.Map<CreateUserDTO>(userToReturn);
                response.Success = true;
                response.Message = "Registration succesfull";
                response.StatusCode = 201;

                return response;
            }

            response.Data = null;
            response.StatusCode = 404;
            response.Message = string.Join(", ", errorList.Select(x => x.Description));
            response.Success = false;

            return response;
        }


        public async Task<Response<AppUser>> IsUserAlreadyExistAsync(string email)
        {
            var response = new Response<AppUser>();
            if (email == null)
            {
                response.Success = false;
                response.Message = "email field is required";

                return response;
            }
            var user = await _userMgr.FindByEmailAsync(email);

            if (user != null)
            {
                response.Success = true;
                response.Message = "user already exist";
                response.Data = user;
                response.StatusCode = 409;

                return response;
            }

            response.Success = false;
            response.Message = "user does not exist";
            response.StatusCode = 404;

            return response;
        }



        public async Task<Response<ReturnLoggedInUserDTO>> LoginAsync(LoginDTO model)
        {
            var response = new Response<ReturnLoggedInUserDTO>();

            var validity = await ValidateHelper.LoginValidator(model);

            var hasValidationErrors = validity.FirstOrDefault(e => e.HasValidationErrors);

            if (hasValidationErrors != null)
            {
                throw new BadRequestException(validity, "Request object does not contain the required data");
            }

            var user = await _userMgr.FindByEmailAsync(model.Email);
            ReturnLoggedInUserDTO returnUser = new ReturnLoggedInUserDTO();

            if (user != null && await _userMgr.CheckPasswordAsync(user, model.Password))
            {
                returnUser.Token = await _jwtService.GenerateToken(user);
                returnUser.Email = user.Email;
                returnUser.Id = user.Id;
                response.Message = "Login successful";
                response.Success = true;
                response.Data = returnUser;
                response.StatusCode = 200;


                return response;
            }

            response.Message = "Invalid login details";
            return response; throw new NotImplementedException();

        }



        public async Task<Response<AppUser>> AddImagAsync(AppUser user, AddImageDTO model)
        {
            var response = new Response<AppUser>();

            var validity = await ValidateHelper.ImageValidator(model);
            var hasValidationErrors = validity.FirstOrDefault(e => e.HasValidationErrors);

            if (hasValidationErrors != null)
            {
                throw new BadRequestException(validity, "Request object does not contain the required data");
            }


            var result = await _imageService.UploadImage(model.Image);

            if (result.Url == null)
            {
                response.Success = false;
                response.Message = "something went wrong, Image upload failed";
                response.StatusCode = 400;
                return response;
            }

            var userToUpdate = await _userMgr.FindByEmailAsync(user.Email);

            userToUpdate.AvatarUrl = result.Url.ToString();
            userToUpdate.PublicId = result.PublicId;

            var userUpdate = await _userMgr.UpdateAsync(userToUpdate);

            if (!userUpdate.Succeeded)
            {
                List<IdentityError> errors = userUpdate.Errors.ToList();
                response.Data = null;
                response.IdentityError = errors;
                response.Message = string.Join(", ", errors.Select(x => x.Description));
                response.StatusCode = 400;
                response.Success = false;
                return response;
            }

            var updatedUser = await _userMgr.FindByEmailAsync(user.Email);

            response.Data = updatedUser;
            response.Errors = null;
            response.Message = "user update successful";
            response.StatusCode = 200;
            response.Success = true;
            return response;

        }

        public async Task<Response<AppUser>> DeleteImageByPublicIdAsync(AppUser user)
        {
            Response<AppUser> response = new Response<AppUser>();

            if (user != null)
            {
                var userToDelte = await _userMgr.FindByIdAsync(user.Id);

                if (userToDelte != null)
                {
                    var result = await _imageService.DeleteImage(userToDelte.PublicId);

                    if (result.Error != null)
                    {
                        response.Data = null;
                        response.Message = result.Error.ToString();
                        response.StatusCode = 400;
                        response.Success = false;
                        return response;
                    }

                    //update user photo details
                    userToDelte.PublicId = null;
                    userToDelte.AvatarUrl = null;
                    await _userMgr.UpdateAsync(userToDelte);

                    var updatedUser = await _userMgr.FindByIdAsync(user.Id);
                    //response to return
                    response.Data = updatedUser;
                    response.Message = "photo removed successfully";
                    response.StatusCode = 200;
                    response.Success = true;
                    return response;
                }
            }
            throw new Exception("user can not be null");
        }

    }
}
