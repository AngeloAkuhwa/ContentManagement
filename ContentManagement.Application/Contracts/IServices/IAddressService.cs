using ContentManagement.Application.DataTransfer;
using ContentManagement.Domain.Commons;
using ContentManagement.Domain.Entities;
using ContentManagement.Identity;
using System.Threading.Tasks;

namespace ContentManagement.Application.Contracts
{
    public interface IAddressService
    {
        Task<Response<Address>> CreateAddress(AppUser user,AddAddressDTO model);
        Task<Response<Address>> ModifyAddress(AppUser user, AddAddressDTO model, string addressId);
        Task<Response<bool>> RemoveAddress(AppUser user,string addressId);
        Task<Response<Address>> IsAddressAlreadyExist(string addressId);
        Task<Response<Address>> GetAddress(AppUser user,string sddressId);
    }
}
