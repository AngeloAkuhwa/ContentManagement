using ContactManagement.Domain.Entities;
using ContentManagement.Application.DataTransfer;
using ContentManagement.Domain.Commons;
using ContentManagement.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Application.Contracts
{
    public interface IPhoneNumberService
    {
        Task<Response<AddPhoneNumberDTO>> StorePhoneNumber(AppUser user, AddPhoneNumberDTO dto);
        Response<List<AddPhoneNumberDTO>> RetrievePhoneNumberByName(string fullName);
        Task<Response<PhoneNumber>> IsPhoneNumberAlreadyExist(string PhoneNumber);
        Task<Response<bool>> DeletePhoneNumberByName(AppUser user,string fullName);
        Task<Response<bool>> UpdatePhoneNumber(AppUser user, string phoneNumberId, AddPhoneNumberDTO dto);
        Task<Response<bool>> DeleteMultiplePhoneNumbers(AppUser user, string[] phoneNumberIds);
        
        //Task<Response<AddPhoneNumberDTO>> StorePhoneNumber(AddPhoneNumberDTO dto);
    }
}
