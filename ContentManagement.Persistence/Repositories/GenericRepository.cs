using ContactManagement.Persistence.DataContext;
using ContentManagement.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ContactDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ContactDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> Delete(T entity)
        {

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> Get(string id)
        {
            T Item = await _dbSet.FindAsync(id);
            return Item;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<bool> Insert(T entity)
        {
            await _dbSet.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
