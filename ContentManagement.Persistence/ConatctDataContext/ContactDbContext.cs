using ContactManagement.Domain.Entities;
using ContentManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactManagement.Persistence.DataContext
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }

        public DbSet<Address> AddressTbl{ get; set; }
        public DbSet<PhoneNumber> PhoneNumbersTbl{ get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)

        {
            var entries = ChangeTracker
                .Entries()
                .Where(entry => entry.Entity is BaseEntity && (
                        entry.State == EntityState.Added
                        || entry.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
