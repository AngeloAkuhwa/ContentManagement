using ContentManagement.Application.Persistence.Repositories.Interfaces;
using ContentManagement.Domain.Entities;
using ContentManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContentManagement.Infrastructure.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly ContactDbContext _context;
        private readonly ILogger<AddressRepository> _logger;
        private readonly DbSet<Address> _dbSet;

        public AddressRepository(ContactDbContext context, ILogger<AddressRepository> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
            _dbSet = _context.Set<Address>();
        }
    }
}
