using ContentManagement.Application.Persistence.Repositories.Interfaces;
using ContentManagement.Infrastructure.Helpers;
using ContentManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace ContentManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ContactDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _logger;

        public GenericRepository(ContactDbContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                LoggingHelper.LogErrorAndThrowException(_logger, nameof(Delete),$"error occured in {nameof(GenericRepository<T>)}: failed to delete entity" );
            }

            return result;
        }

        public async Task<T> Get(string id)
        {
            T Item = await _dbSet.FindAsync(id);
            if (id is null || Item is null)
            {
                LoggingHelper.LogErrorAndThrowException(_logger, nameof(Get), $"error occured in {nameof(GenericRepository<T>)}:" +
                    $"failed to required entity, either {nameof(id)} is null or {typeof(T).Name} was not found");
            }
            return Item;
        }

        public IQueryable<T> GetAll()
        {
           var result = _dbSet.AsNoTracking();
            if(result is null)
            {
                LoggingHelper.LogErrorAndThrowException(_logger, nameof(GetAll), $"error occured in {nameof(GenericRepository<T>)}: couldn't find entries to retrieve");
            }
            return result;
        }

        public async Task<bool> Insert(T entity)
        {
            var insertResutl = await _dbSet.AddAsync(entity);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                LoggingHelper.LogErrorAndThrowException(_logger, nameof(Insert), $"error occured in {nameof(GenericRepository<T>)}: failed to insert entity {typeof(T).Name}");
            }
            return result;
        }

        public async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result)
            {
                LoggingHelper.LogErrorAndThrowException(_logger, nameof(Update), $"error occured in {nameof(GenericRepository<T>)}: failed to update entity {typeof(T).Name}");
            }
            return result;
        }

    }
}
