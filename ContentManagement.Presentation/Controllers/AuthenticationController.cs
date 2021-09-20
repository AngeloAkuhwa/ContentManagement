using ContentManagement.Application.Contracts;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagement.Presentation.Controllers
{
    public class AuthenticationController:BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUserService userService, ILogger<AuthenticationController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("create-user")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateUserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateUserDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreadUser([FromBody] CreateUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = JsonConvert.SerializeObject(ModelState.Values.Select(err => err.Errors).ToList());
                LoggingHelper.LogError(_logger, Request, $"receives an invalid request in {nameof(AuthenticationController)} with {errors}");
                return BadRequest(errors);
            }
            var result = await _userService.RegisterUserAsyn(model);

            if (result.Success) return Created("CreadUser", result);

            return BadRequest(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ReturnLoggedInUserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnLoggedInUserDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = JsonConvert.SerializeObject(ModelState.Values.Select(err => err.Errors).ToList());
                LoggingHelper.LogError(_logger, Request, $"receives an invalid request in {nameof(AuthenticationController)} with {errors}");
                return BadRequest(errors);
            }
            var result = await _userService.LoginAsync(model);

            if (result.Success) return Ok(result.Data);

            return BadRequest(result);
        }
    }
}
