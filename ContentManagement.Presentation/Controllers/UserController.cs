using ContentManagement.Application.Contracts;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Domain.Commons;
using ContentManagement.Domain.Entities;
using ContentManagement.Identity;
using ContentManagement.Infrastructure.Helpers;
using FacilityManagement.Services.API.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagement.Presentation.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userMgr;
        private readonly IPhoneNumberService _phoneNumberService;
        private readonly IAddressService _addressService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserService userService, UserManager<AppUser> userMgr,
            IPhoneNumberService phoneNumberService, IAddressService addressService,
            ILogger<UserController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userMgr = userMgr ?? throw new ArgumentNullException(nameof(userMgr));
            _phoneNumberService = phoneNumberService ?? throw new ArgumentNullException(nameof(phoneNumberService));
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize]
        [HttpPost("add-image")]
        [ProducesResponseType(typeof(Response<AppUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AppUser>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddImage([FromForm] AddImageDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = JsonConvert.SerializeObject(ModelState.Values.Select(err => err.Errors).ToList());
                LoggingHelper.LogError(_logger, Request, $"receives an invalid request in {nameof(AuthenticationController)} with {errors}");
                return BadRequest(errors);
            }
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result = await _userService.AddImagAsync(loggedInUser, model);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Policy = Policies.GeneralUser)]
        [HttpDelete("delete-user-image")]
        [ProducesResponseType(typeof(Response<AppUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AppUser>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteImageById()
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
        public async Task<IActionResult> StorePhoneNumber([FromBody] AddPhoneNumberDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = JsonConvert.SerializeObject(ModelState.Values.Select(err => err.Errors).ToList());
                LoggingHelper.LogError(_logger, Request, $"receives an invalid request in {nameof(AuthenticationController)} with {errors}");
                return BadRequest(errors);
            }
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result = await _phoneNumberService.StorePhoneNumber(loggedInUser, dto);

            if (result.Success) return Created("StorePhoneNumber", result);

            return BadRequest(result);
        }

        [Authorize(Policy = Policies.GeneralUser)]
        [HttpPost("get-phoneNumber-byName")]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status400BadRequest)]
        public IActionResult GetPhoneNumberByName([FromBody] string search)
        {
            var result = _phoneNumberService.RetrievePhoneNumberByName(search);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Policy = Policies.GeneralUser)]
        [HttpPatch("update-phoneNumber/{phoneNumberId}")]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AddPhoneNumberDTO>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute] string phoneNumberId, [FromBody] AddPhoneNumberDTO dto)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result = await _phoneNumberService.UpdatePhoneNumber(loggedInUser, phoneNumberId, dto);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Policy = Policies.GeneralUser)]
        [HttpDelete("delete-phoneNumberby-fullname")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePhoneNumberByFullName([FromForm] string fullName)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);

            var result = await _phoneNumberService.DeletePhoneNumberByName(loggedInUser, fullName);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }


        [Authorize(Policy = Policies.GeneralUser)]
        [HttpDelete("delete-multiple-Phonenumbers-by-ids")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMultiplePhoneNumbersByIds([FromForm] string[] phoneNumberIds)
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
        public async Task<IActionResult> CreateAddress([FromBody] AddAddressDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = JsonConvert.SerializeObject(ModelState.Values.Select(err => err.Errors).ToList());
                LoggingHelper.LogError(_logger, Request, $"receives an invalid request in {nameof(AuthenticationController)} with {errors}");
                return BadRequest(errors);
            }
            var loggedInUser = await _userMgr.GetUserAsync(User);
            var result = await _addressService.CreateAddress(loggedInUser, model);

            if (result.Success) return Created("CreadUser", result);

            return BadRequest(result);
        }

        [HttpPost("get-address/{addressId}")]
        [Authorize(Policy = Policies.GeneralUser)]
        [ProducesResponseType(typeof(Response<Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<Address>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAddress([FromRoute] string addressId)
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
        public async Task<IActionResult> ModifyUserAddress([FromRoute] string addressId, [FromBody] AddAddressDTO model)
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
        public async Task<IActionResult> DeleteAddress([FromRoute] string addressId)
        {
            var loggedInUser = await _userMgr.GetUserAsync(User);
            var result = await _addressService.RemoveAddress(loggedInUser, addressId);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
    }
}
