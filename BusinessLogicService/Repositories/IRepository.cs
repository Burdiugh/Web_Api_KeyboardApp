using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task< IEnumerable<TEntity>> GetAllAsync(params string[] includes);
        Task <TEntity> GetByIdAsync(object id);
        Task DeleteByIdAsync(object id);
        Task UpdateAsync(TEntity entity);
        Task InsertAsync(TEntity entity);
        Task SaveChanges();

        IEnumerable<TEntity> GetListBySpec(ISpecification<TEntity> specification);

        TEntity? GetFirstBySpec(ISpecification<TEntity> specification);

        
    }
}
