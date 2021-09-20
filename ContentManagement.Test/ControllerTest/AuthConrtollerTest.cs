using ContentManagement.Application.Contracts;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Domain.Commons;
using ContentManagement.Presentation.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ContentManagement.Test
{
    public class AuthConrtollerTest
    {
        #region Dependencies Mock up
        private Mock<IUserService> _userService;
         private  AuthenticationController _authenticationController;
         private readonly Mock<ILogger<AuthenticationController>> _logger;

        #endregion ends depencies Mock up

        #region Constructor

        public AuthConrtollerTest()
        {
            _userService = new Mock<IUserService>();
            _logger = new Mock<ILogger<AuthenticationController>>();
            _authenticationController = new AuthenticationController(_userService.Object, _logger.Object);
        }

        #endregion ends Constructor


        #region Testing section

        [Fact]
        private async Task CreateUser_Should_Return_CreatedResultAnd_BadRequestObjectresult()
        {
            //arrange
            Mock_CreatedUser_Returns_ObjectResults();
            _authenticationController.ControllerContext.HttpContext = new DefaultHttpContext();
            CreateUserDTO model =new CreateUserDTO{};

            //act
            var actual = await _authenticationController.CreadUser(model) as CreatedResult;
            var actualSecond = await _authenticationController.CreadUser(model) as BadRequestObjectResult;

            //assert
            Assert.IsType<CreatedResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }


        [Fact]
        private async Task LoginUser_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_LoginUser_Returns_ObjectResults();
            _authenticationController.ControllerContext.HttpContext = new DefaultHttpContext();
            LoginDTO model = new LoginDTO { };

            //act
            var actual = await _authenticationController.Login(model) as OkObjectResult;
            var actualSecond = await _authenticationController.Login(model) as BadRequestObjectResult;
            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        #endregion  ends Testing section




        #region Test methods setup

        private void Mock_CreatedUser_Returns_ObjectResults()
        {
            _userService.SetupSequence(x => x.RegisterUserAsyn(It.IsAny<CreateUserDTO>()))
                 .ReturnsAsync(new Response<CreateUserDTO>() { Success = true })
                 .ReturnsAsync(new Response<CreateUserDTO> { Success = false }) ;
        }

        private void Mock_LoginUser_Returns_ObjectResults()
        {
            _userService.SetupSequence(x => x.LoginAsync(It.IsAny<LoginDTO>()))
                 .ReturnsAsync(new Response<ReturnLoggedInUserDTO>() { Success = true })
                 .ReturnsAsync(new Response<ReturnLoggedInUserDTO> { Success = false });
        }

        #endregion  
    }
}
