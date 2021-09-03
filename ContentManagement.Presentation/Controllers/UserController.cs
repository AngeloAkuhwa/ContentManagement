using CloudinaryImageCrudHandler.DTO;
using ContactManagement.Domain.Entities;
using ContentManagement.Application.Contracts;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Domain.Commons;
using ContentManagement.Identity;
using ContentManagement.Presentation.Controllers;
using FacilityManagement.Services.API.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CloudinaryImageCrudHandler.Controllers
{
    public class UserController:BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userMgr;
        private readonly IPhoneNumberService _phoneNumberService;
        private readonly IAddressService _addressService;


        public UserController(IServiceProvider serviceProvider)
        {
            _userService = serviceProvider.GetRequiredService<IUserService>();
            _userMgr = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            _phoneNumberService = serviceProvider.GetRequiredService<IPhoneNumberService>();
            _addressService = serviceProvider.GetRequiredService<IAddressService>();
        }


        [Authorize]
        [HttpPost("add-image")]
        [ProducesResponseType(typeof(Response<AppUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AppUser>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddImageDTO>> AddImage([FromForm]AddImageDTO model)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result =await _userService.AddImagAsync(loggedInUser,model);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Policy = Policies.GeneralUser)]
        [HttpDelete("delete-user-image")]
        [ProducesResponseType(typeof(Response<AppUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AppUser>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddImageDTO>> DeleteImageById()
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result = await _userService.DeleteImageByPublicIdAsync(loggedInUser);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Policy = Policies.GeneralUser)]
        [HttpPost("store-phone-number")]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddImageDTO>> StorePhoneNumber([FromBody]AddPhoneNumberDTO dto)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result = await _phoneNumberService.StorePhoneNumber(loggedInUser, dto);

            if (result.Success) return Created("StorePhoneNumber", result);

            return BadRequest(result);
        }

        [Authorize(Policy = Policies.GeneralUser)]
        [HttpPost("get-phoneNumber-byName")]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status400BadRequest)]
        public ActionResult<AddPhoneNumberDTO> GetPhoneNumberByName([FromBody] string search)
        {
            var result =  _phoneNumberService.RetrievePhoneNumberByName(search);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Policy = Policies.GeneralUser)]
        [HttpPatch("update-phoneNumber/{phoneNumberId}")]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddPhoneNumberDTO>> UpdatePhoneNumber([FromRoute]string phoneNumberId,[FromBody] AddPhoneNumberDTO dto)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result =await _phoneNumberService.UpdatePhoneNumber(loggedInUser, phoneNumberId, dto);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Policy = Policies.GeneralUser)]
        [HttpDelete("delete-phoneNumberby-fullname")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> DeletePhoneNumberByFullName([FromForm] string fullName)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result = await _phoneNumberService.DeletePhoneNumberByName(loggedInUser,fullName);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }


        [Authorize(Policy = Policies.GeneralUser)]
        [HttpDelete("delete-multiple-Phonenumbers-by-ids")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> DeleteMultiplePhoneNumbersByIds([FromForm] string[] phoneNumberIds)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result = await _phoneNumberService.DeleteMultiplePhoneNumbers(loggedInUser, phoneNumberIds);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("create-address")]
        [Authorize(Policy = Policies.GeneralUser)]
        [ProducesResponseType(typeof(Response<Address>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<Address>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<Address>>> CreateAddress([FromBody] AddAddressDTO model)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);
            var result = await _addressService.CreateAddress(loggedInUser, model);

            if (result.Success) return Created("CreadUser", result);

            return BadRequest(result);
        }

        [HttpPost("get-address/{addressId}")]
        [Authorize(Policy = Policies.GeneralUser)]
        [ProducesResponseType(typeof(Response<Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<Address>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<Address>>> GetAddress([FromRoute] string addressId)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);
            var result = await _addressService.GetAddress(loggedInUser, addressId);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }


        [HttpPatch("modify-address/{addressId}")]
        [Authorize(Policy = Policies.GeneralUser)]
        [ProducesResponseType(typeof(Response<Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<Address>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<Address>>> ModifyUserAddress([FromRoute] string addressId, [FromBody] AddAddressDTO model)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);
            var result = await _addressService.ModifyAddress(loggedInUser, model, addressId);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("delete-address/{addressId}")]
        [Authorize(Policy = Policies.GeneralUser)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> DeleteAddress([FromRoute] string addressId)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);
            var result = await _addressService.RemoveAddress(loggedInUser, addressId);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
    }
}
