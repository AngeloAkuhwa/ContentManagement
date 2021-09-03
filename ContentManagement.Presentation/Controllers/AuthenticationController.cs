using CloudinaryImageCrudHandler.DTO;
using ContentManagement.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ContentManagement.Presentation.Controllers
{
    public class AuthenticationController:BaseController
    {
        private readonly IUserService _userService;

        public AuthenticationController(IServiceProvider provider)
        {
            _userService = provider.GetRequiredService<IUserService>();
        }

        [HttpPost("create-user")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateUserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateUserDTO), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateUserDTO>> CreadUser([FromBody] CreateUserDTO model)
        {
            var result = await _userService.RegisterUserAsyn(model);

            if (result.Success) return Created("CreadUser", result);

            return BadRequest(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ReturnLoggedInUserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnLoggedInUserDTO), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReturnLoggedInUserDTO>> Login([FromBody] LoginDTO model)
        {
            var result = await _userService.LoginAsync(model);

            if (result.Success) return Ok(result.Data);

            return BadRequest(result);
        }
    }
}
