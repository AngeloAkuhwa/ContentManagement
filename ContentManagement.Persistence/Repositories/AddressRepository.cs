using ContactManagement.Domain.Entities;
using ContactManagement.Persistence.DataContext;
using ContentManagement.Application.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentManagement.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly ContactDbContext _context;
        private readonly DbSet<Address> _dbSet;

        public AddressRepository(ContactDbContext context) :base(context)
        {
            _context = context;
            _dbSet = _context.Set<Address>();
        }
    }
}
