using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ContentManagement.Application.Contracts
{
    public interface IIMageService
    {
        Task<UploadResult> UploadImage(IFormFile model);
        Task<DelResResult> DeleteImage(string publicId);
    }
}
