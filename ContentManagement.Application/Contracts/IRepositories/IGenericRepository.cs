using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(string id);
        IQueryable<T> GetAll();

        Task<bool> Insert(T entity);

        Task<bool> Update(T enity);

        Task<bool> Delete(T entity);
    }
}
