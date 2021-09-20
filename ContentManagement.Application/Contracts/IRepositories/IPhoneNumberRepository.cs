using ContactManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Application.Persistence.Repositories.Interfaces
{
    public interface IPhoneNumberRepository : IGenericRepository<PhoneNumber>
    {
        public int TotalNumberOfItems { get; set; }
        public int TotalNumberOfPages { get; set; }

        List<PhoneNumber> FilterName(string search);
        Task<IReadOnlyList<PhoneNumber>> GetAllAvailablePhoneNumbers();
        Task<ICollection<PhoneNumber>> GetPaginated(int pageNumber, int itemsPerPage, IQueryable<PhoneNumber> items);
        Task<int> DeleteRange(string[] phoneNumberIds);
        Task<bool> DeleteByName(string fullName);
        PhoneNumber GetBySpecificName(string fullName);
        Task<bool> DeleteByPhoneNumber(string phoneNumber);
        PhoneNumber GetByPhoneNumber(string phoneNumber);
    }
}
