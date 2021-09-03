using ContactManagement.Domain.Entities;
using ContactManagement.Persistence.DataContext;
using ContentManagement.Application.Contracts.IRepositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagement.Persistence.Repositories
{
    public class PhoneNumberRepository : GenericRepository<PhoneNumber> , IPhoneNumberRepository
    {
        private readonly ContactDbContext _context;
        private readonly DbSet<PhoneNumber> _dbSet;
        private readonly IDapperRepository _dapperRepository;

        public PhoneNumberRepository(ContactDbContext context, IDapperRepository dapperRepository) :base(context)
        {
            _context = context;
            _dbSet = _context.Set<PhoneNumber>();
            _dapperRepository = dapperRepository;
        }





        public int TotalNumberOfItems { get; set; }
        public int TotalNumberOfPages { get; set; }

        public async Task<IReadOnlyList<PhoneNumber>> GetAllAvailablePhoneNumbers()
        {
            return await _context.PhoneNumbersTbl.Where(PhoneNumber => PhoneNumber.IsAvailable).ToListAsync();
        }

        public async Task<ICollection<PhoneNumber>> GetPaginated(int pageNumber, int itemsPerPage, IQueryable<PhoneNumber> items)
        {
            TotalNumberOfItems = await items.CountAsync();

            TotalNumberOfPages = (int)Math.Ceiling(TotalNumberOfItems / (double)itemsPerPage);

            if (pageNumber > TotalNumberOfPages || pageNumber < 1)
            {
                return null;
            }
            var pagedItems = await items.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();
            return pagedItems;
        }

        public  List<PhoneNumber> FilterName(string search)
        {
            return  _dapperRepository
                                .GetConnectionString()
                                .Query<PhoneNumber>("SELECT * FROM PhoneNumbersTbl WHERE FirstName like '%' + @nameSearch + '%' ", new { nameSearch = search })
                                .ToList();
        }

        public PhoneNumber GetBySpecificName(string fullName)
        {
            return  _dapperRepository
                                .GetConnectionString()
                                .Query<PhoneNumber>("SELECT * FROM PhoneNumbersTbl WHERE FullName = @nameSearch", new { nameSearch = fullName })
                                .FirstOrDefault();
                                
        }
        public async Task<int> DeleteRange(string[] phoneNumberIds)
        {
          var result = await  _dapperRepository
                            .GetConnectionString()
                            .QueryAsync<PhoneNumber>("DELETE FROM PhoneNumbersTbl WHERE Id IN @phoneIds ", new { @phoneIds = phoneNumberIds });

            return result.Select(x => x.Id).Count();
        }

        public async Task<bool> DeleteByName(string fullName)
        {
            var sql = "DELETE FROM PhoneNumbersTbl WHERE FullName = @storedName";
            var result = await _dapperRepository
                                            .GetConnectionString()
                                            .ExecuteAsync(sql, new { storedName = fullName });
            return result > 0;
        }

        public async Task<bool> DeleteByPhoneNumber(string phoneNumber)
        {
            var sql = "DELETE FROM PhoneNumbersTbl WHERE Number = @storedNunmber";
            var result = await _dapperRepository
                                            .GetConnectionString()
                                            .ExecuteAsync(sql, new { storedNunmber = phoneNumber });
            return result > 0;
        }

        public  PhoneNumber GetByPhoneNumber(string phoneNumber)
        {
            return _dapperRepository
                                .GetConnectionString()
                                .Query<PhoneNumber>("SELECT * FROM PhoneNumbersTbl WHERE Number = @storedNumber ", new { storedNumber = phoneNumber })
                                .FirstOrDefault();
                                
        }
    }
}
