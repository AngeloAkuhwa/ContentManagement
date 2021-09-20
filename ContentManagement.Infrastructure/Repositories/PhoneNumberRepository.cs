using ContactManagement.Domain.Entities;
using ContentManagement.Application.Persistence.Repositories.Interfaces;
using ContentManagement.Infrastructure.Helpers;
using ContentManagement.Infrastructure.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagement.Infrastructure.Repositories
{
    public class PhoneNumberRepository : GenericRepository<PhoneNumber>, IPhoneNumberRepository
    {
        private readonly ContactDbContext _context;
        private readonly DbSet<PhoneNumber> _dbSet;
        private readonly IDapperRepository _dapperRepository;
        private readonly ILogger<PhoneNumberRepository> _logger;

        public PhoneNumberRepository(ContactDbContext context, IDapperRepository dapperRepository, ILogger<PhoneNumberRepository> logger)
            : base(context,logger)
        {
            _logger = logger;
            _context = context;
            _dbSet = _context.Set<PhoneNumber>();
            _dapperRepository = dapperRepository;
        }


        public int TotalNumberOfItems { get; set; }
        public int TotalNumberOfPages { get; set; }

        public async Task<IReadOnlyList<PhoneNumber>> GetAllAvailablePhoneNumbers()
        {
            var result = await _context.PhoneNumbersTbl.Where(PhoneNumber => PhoneNumber.IsAvailable).ToListAsync();
            if(result.Count <= 0)
            {
                LoggingHelper.LogErrorAndThrowException(
                    _logger, nameof(GetAllAvailablePhoneNumbers),
                    $" error occured in {nameof(PhoneNumberRepository)}:" +
                    $" failed to retrieve {nameof(PhoneNumber)}s");
            }
            return result;
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

        public List<PhoneNumber> FilterName(string search)
        {
           var result = _dapperRepository
                                .GetConnectionString()
                                .Query<PhoneNumber>("SELECT * FROM PhoneNumbersTbl WHERE FirstName like '%' + @nameSearch + '%' ", new { nameSearch = search })
                                .ToList();
            if(result.Count <= 0)
            {
                LoggingHelper.LogErrorAndThrowException(
                    _logger, nameof(FilterName), 
                    $"error occured in {nameof(PhoneNumberRepository)}:" +
                    $" failed to filter {typeof(PhoneNumber).Name}s by name");
            }
            return result;
        }

        public PhoneNumber GetBySpecificName(string fullName)
        {
            var result = _dapperRepository
                                .GetConnectionString()
                                .Query<PhoneNumber>("SELECT * FROM PhoneNumbersTbl WHERE FullName = @nameSearch", new { nameSearch = fullName })
                                .FirstOrDefault();
            if(result.Number is null)
            {
                LoggingHelper.LogErrorAndThrowException(
                    _logger, nameof(GetBySpecificName),
                    $"error occured in {nameof(PhoneNumberRepository)}: " +
                    $"failed to retrieve {typeof(PhoneNumber).Name} by specificName");
            }
            return result;

        }
        public async Task<int> DeleteRange(string[] phoneNumberIds)
        {
            var result = await _dapperRepository
                              .GetConnectionString()
                              .QueryAsync<PhoneNumber>("DELETE FROM PhoneNumbersTbl WHERE Id IN @phoneIds ", new { @phoneIds = phoneNumberIds });
           if(!result.Any())
            {
                LoggingHelper.LogErrorAndThrowException(
                    _logger, nameof(DeleteRange), 
                    $"error occured in {nameof(PhoneNumberRepository)}:" +
                    $" failed to delete {typeof(PhoneNumber).Name}s by range");
            }
           return result.Select(x => x.Id).Count();
        }

        public async Task<bool> DeleteByName(string fullName)
        {
            var sql = "DELETE FROM PhoneNumbersTbl WHERE FullName = @storedName";
            var result = await _dapperRepository
                                            .GetConnectionString()
                                            .ExecuteAsync(sql, new { storedName = fullName });
            if(result <= 0)
            {
                LoggingHelper.LogErrorAndThrowException(
                    _logger, nameof(DeleteByName), 
                    $"error occured in {nameof(PhoneNumberRepository)}:" +
                    $" failed to delete {typeof(PhoneNumber).Name} by name");
            }
            return result > 0;
        }

        public async Task<bool> DeleteByPhoneNumber(string phoneNumber)
        {
            var sql = "DELETE FROM PhoneNumbersTbl WHERE Number = @storedNunmber";
            var result = await _dapperRepository
                                            .GetConnectionString()
                                            .ExecuteAsync(sql, new { storedNunmber = phoneNumber });
            if(result <= 0)
            {
                LoggingHelper.LogErrorAndThrowException(
                    _logger, nameof(DeleteByPhoneNumber), 
                    $"error occured in {nameof(PhoneNumberRepository)}:" +
                    $" failed to delete {typeof(PhoneNumber).Name} by PhoneNumber");
            }
            return result > 0;
        }

        public PhoneNumber GetByPhoneNumber(string phoneNumber)
        {
           var result = _dapperRepository
                                .GetConnectionString()
                                .Query<PhoneNumber>("SELECT * FROM PhoneNumbersTbl WHERE Number = @storedNumber ", new { storedNumber = phoneNumber })
                                .FirstOrDefault();
            if(result.Number is null)
            {
                LoggingHelper.LogErrorAndThrowException(
                    _logger, nameof(GetByPhoneNumber), 
                    $"error occured in {nameof(PhoneNumberRepository)}:" +
                    $" failed to retrieve {typeof(PhoneNumber).Name} by PhoneNumber");
            }
            return result;
        }
    }
}
