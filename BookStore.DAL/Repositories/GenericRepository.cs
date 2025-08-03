using System.Linq.Expressions;
using BookStore.DAL.DataContext;
using BookStore.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
{
    private readonly BookStoreDbContext _dbContext;

    public GenericRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<int> DeleteAsync(TEntity entity)
    {
        _ = _dbContext.Remove(entity);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
    }

    public async Task<IQueryable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null,
                                                        List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> includes = null)
    {
        var query = filter == null
                    ? _dbContext.Set<TEntity>()
                    : _dbContext.Set<TEntity>().Where(filter);
        includes?.ForEach(include => query = include(query));
        return query;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _ = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
