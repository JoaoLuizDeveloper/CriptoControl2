using CriptoControl.Model.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CriptoControl.Infrastructure.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        #region Construtor
        protected readonly ApplicationDbContext _dbContext;
        internal DbSet<TEntity> DbSet;
        public RepositoryBase(ApplicationDbContext context)
        {
            _dbContext = context;
            this.DbSet = context.Set<TEntity>();
        }
        #endregion

        #region Get Individual Object
        public async Task<TEntity> Get(int id) => await DbSet.FindAsync(id);

        public async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                Parallel.ForEach(includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), (includeProperty) =>
                {
                    query = query.Include(includeProperty);
                });
            }

            return await query.FirstOrDefaultAsync();
        }
        #endregion

        #region Get List of Objects with filters
        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                Parallel.ForEach(includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), (includeProperty) =>
                {
                    query = query.Include(includeProperty);
                });
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return await query.ToListAsync();
        }        
        #endregion

        #region Add, Update, Delete Object
        public async Task<bool> Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return await Save();
        }
        
        public async Task<bool> Update(TEntity entity)
        {
            DbSet.Update(entity);
            return await Save();
        }

        public async Task<bool> Remove(int id)
        {
            TEntity entityToRemove = await DbSet.FindAsync(id);
            DbSet.Remove(entityToRemove);

            return await Save();
        }        
        #endregion

        #region Verify if Exist
        public async Task<bool> Exists(int id)
        {
            var value = await DbSet.FindAsync(id);
            //>= 0 ? true : false
            if (value != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Save
        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() >= 0;
        }
        #endregion
    }
}