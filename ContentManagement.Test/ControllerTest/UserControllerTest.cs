using ContentManagement.Application.Contracts;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Domain.Commons;
using ContentManagement.Domain.Entities;
using ContentManagement.Identity;
using ContentManagement.Presentation.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace ContentManagement.Test.ControllerTest
{
    public class UserControllerTest
    {
        #region Depencies Mock up
        private Mock<IUserService> _userService;
        private Mock<IPhoneNumberService> _phoneNumberService;
        private Mock<IAddressService> _adddressService;
        private Mock<ILogger<UserController>> _logger;
        Mock<IUserStore<AppUser>> store;
        Mock<UserManager<AppUser>> mockUserManager;
        UserController userController;

        #endregion ends depndencies Mock up


        #region Constructor 

        public UserControllerTest()
        {
            _userService = new Mock<IUserService>();
            _phoneNumberService = new Mock<IPhoneNumberService>();  
            _adddressService = new Mock<IAddressService>(); 
            _logger = new Mock<ILogger<UserController>>();  
             store = new Mock<IUserStore<AppUser>>();
            var mockUser = new Mock<UserManager<IdentityUser>>();
            mockUserManager = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new AppUser());   
            userController = new UserController(_userService.Object, mockUserManager.Object, _phoneNumberService.Object, _adddressService.Object, _logger.Object);
        }

        #endregion ends Constructor


        #region Testing section

        [Fact]
        private async Task AddImage_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_AddImageReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            AddImageDTO model = new AddImageDTO();

            //act
            var actual = await userController.AddImage(model) as OkObjectResult;    
            var actualSecond = await userController.AddImage(model) as BadRequestObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        [Fact]
        private async Task DeleteImageById_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_DeleteImageByIdReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var actual = await userController.DeleteImageById() as OkObjectResult;
            var actualSecond = await userController.DeleteImageById() as BadRequestObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        [Fact]
        private async Task StorePhoneNumber_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_StorePhoneNumberReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            AddPhoneNumberDTO model = new AddPhoneNumberDTO();  
            //act
            var actual = await userController.StorePhoneNumber(model) as CreatedResult;
            var actualSecond = await userController.StorePhoneNumber(model) as BadRequestObjectResult;

            //assert
            Assert.IsType<CreatedResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        [Fact]
        private void GetPhoneNumberByName_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_GetPhoneNumberByNameReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var actual =  userController.GetPhoneNumberByName("") as OkObjectResult;
            var actualSecond =  userController.GetPhoneNumberByName("") as BadRequestObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        [Fact]
        private async Task UpdatePhoneNumber_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_UpdatePhoneNumberReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var actual =await userController.UpdatePhoneNumber("", new AddPhoneNumberDTO()) as OkObjectResult;
            var actualSecond =await userController.UpdatePhoneNumber("", new AddPhoneNumberDTO()) as BadRequestObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }


        [Fact]
        private async Task DeletePhoneNumberByFullName_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_DeletePhoneNumberByFullNameReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var actual = await userController.DeletePhoneNumberByFullName("") as OkObjectResult;
            var actualSecond = await userController.DeletePhoneNumberByFullName("") as BadRequestObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        [Fact]
        private async Task DeleteMultiplePhoneNumbersByIds_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_DeleteMultiplePhoneNumbersByIdsReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var actual = await userController.DeleteMultiplePhoneNumbersByIds(new string[] { }) as OkObjectResult;
            var actualSecond = await userController.DeleteMultiplePhoneNumbersByIds(new string[] { }) as BadRequestObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        [Fact]
        private async Task CreateAddress_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_CreateAddressReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var actual = await userController.CreateAddress(new AddAddressDTO()) as CreatedResult;
            var actualSecond = await userController.CreateAddress(new AddAddressDTO()) as BadRequestObjectResult;

            //assert
            Assert.IsType<CreatedResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        [Fact]
        private async Task GetAddress_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_GetAddressReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var actual = await userController.GetAddress("") as OkObjectResult;
            var actualSecond = await userController.GetAddress("") as BadRequestObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        [Fact]
        private async Task ModifyUserAddress_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_ModifyUserAddressReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var actual = await userController.ModifyUserAddress("", new AddAddressDTO()) as OkObjectResult;
            var actualSecond = await userController.ModifyUserAddress("", new AddAddressDTO()) as BadRequestObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        [Fact]
        private async Task DeleteAddress_Should_Return_OkObjectResultAnd_BadRequestObjectResult()
        {
            //arrange
            Mock_DeleteAddressReturns_ObjectResults();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            //act
            var actual = await userController.DeleteAddress("") as OkObjectResult;
            var actualSecond = await userController.DeleteAddress("") as BadRequestObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.IsType<BadRequestObjectResult>(actualSecond);
        }

        #endregion ends Testing section


        #region Test Methods setup 
        private void Mock_AddImageReturns_ObjectResults()
        {
            _userService.SetupSequence(x => x.AddImagAsync(It.IsAny<AppUser>(), It.IsAny<AddImageDTO>()))
                .ReturnsAsync(new Response<AppUser>() { Success = true})
                .ReturnsAsync(new Response<AppUser>() { Success = false });  
        }

        private void Mock_DeleteImageByIdReturns_ObjectResults()
        {
            _userService.SetupSequence(x => x.DeleteImageByPublicIdAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(new Response<AppUser>() { Success = true })
                .ReturnsAsync(new Response<AppUser>() { Success = false });
        }

        private void Mock_StorePhoneNumberReturns_ObjectResults()
        {
            _phoneNumberService.SetupSequence(x => x.StorePhoneNumber(It.IsAny<AppUser>(), It.IsAny<AddPhoneNumberDTO>()))
                .ReturnsAsync(new Response<AddPhoneNumberDTO>() { Success = true })
                .ReturnsAsync(new Response<AddPhoneNumberDTO>() { Success = false });
        }

        private void Mock_GetPhoneNumberByNameReturns_ObjectResults()
        {
            _phoneNumberService.SetupSequence(x => x.RetrievePhoneNumberByName(It.IsAny<string>()))
                .Returns(new Response<List<AddPhoneNumberDTO>>() { Success = true })
                .Returns(new Response<List<AddPhoneNumberDTO>>() { Success = false });
        }

        private void Mock_UpdatePhoneNumberReturns_ObjectResults()
        {
            _phoneNumberService.SetupSequence(x => x.UpdatePhoneNumber(It.IsAny<AppUser>(),It.IsAny<string>(), It.IsAny<AddPhoneNumberDTO>()))
                .ReturnsAsync(new Response<bool>() { Success = true })
                .ReturnsAsync(new Response<bool>() { Success = false });
        }

        private void Mock_DeletePhoneNumberByFullNameReturns_ObjectResults()
        {
            _phoneNumberService.SetupSequence(x => x.DeletePhoneNumberByName(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(new Response<bool>() { Success = true })
                .ReturnsAsync(new Response<bool>() { Success = false });
        }

        private void Mock_DeleteMultiplePhoneNumbersByIdsReturns_ObjectResults()
        {
            _phoneNumberService.SetupSequence(x => x.DeleteMultiplePhoneNumbers(It.IsAny<AppUser>(), It.IsAny<string[]>()))
                .ReturnsAsync(new Response<bool>() { Success = true })
                .ReturnsAsync(new Response<bool>() { Success = false });
        }
        private void Mock_CreateAddressReturns_ObjectResults()
        {
            _adddressService.SetupSequence(x => x.CreateAddress(It.IsAny<AppUser>(), It.IsAny<AddAddressDTO>()))
                .ReturnsAsync(new Response<Address>() { Success = true })
                .ReturnsAsync(new Response<Address>() { Success = false });
        }

        private void Mock_GetAddressReturns_ObjectResults()
        {
            _adddressService.SetupSequence(x => x.GetAddress(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(new Response<Address>() { Success = true })
                .ReturnsAsync(new Response<Address>() { Success = false });
        }

        private void Mock_ModifyUserAddressReturns_ObjectResults()
        {
            _adddressService.SetupSequence(x => x.ModifyAddress(It.IsAny<AppUser>(),It.IsAny<AddAddressDTO>() ,It.IsAny<string>()))
                .ReturnsAsync(new Response<Address>() { Success = true })
                .ReturnsAsync(new Response<Address>() { Success = false });
        }

        private void Mock_DeleteAddressReturns_ObjectResults()
        {
            _adddressService.SetupSequence(x => x.RemoveAddress(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(new Response<bool>() { Success = true })
                .ReturnsAsync(new Response<bool>() { Success = false });
        }

        #endregion ends Test Methods setup
    }
}
