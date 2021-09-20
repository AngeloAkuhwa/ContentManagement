using ContentManagement.Application.DataTransfer;
using ContentManagement.Domain.Commons;
using ContentManagement.Identity;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ContentManagement.Application.Contracts
{
    public interface IUserService
    {
        Task<Response<CreateUserDTO>> RegisterUserAsyn(CreateUserDTO model);
        Task<Response<CreateUserDTO>> CreateUserAsync(CreateUserDTO model);
        Task<Response<ReturnLoggedInUserDTO>> LoginAsync(LoginDTO model);
        Task<Response<AppUser>> IsUserAlreadyExistAsync(string email);
        Task<Response<AppUser>> AddImagAsync(AppUser user, AddImageDTO model);
        Task<Response<AppUser>> DeleteImageByPublicIdAsync(AppUser user);
    }
}
