using System.Linq.Expressions;

namespace BookStore.DAL.Repositories.IRepositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null);
    Task<IQueryable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null,
                                           List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> includes = null);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<int> DeleteAsync(TEntity entity);
}
