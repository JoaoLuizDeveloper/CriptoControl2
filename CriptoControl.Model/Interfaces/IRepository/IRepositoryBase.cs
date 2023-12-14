using System.Linq.Expressions;

namespace CriptoControl.Model.Interfaces.IRepository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null);
        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null);
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Remove(int id);
        Task<bool> Exists(int id);
        Task<bool> Save();
    }
}