using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Stock.Adapter.Repositories
{
    internal sealed class Repository<TEntity, TContext> : IRepository<TEntity>, IDisposable
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private bool _disposed;

        /// <summary>
        /// 
        /// </summary>
        public Repository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public int Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                // Resolve the concurrency conflict by refreshing the 
                // object context before re-saving changes. 
                //_dbContext.Refresh(RefreshMode.ClientWins, entity);

                // Save changes.
                int result = _dbContext.SaveChanges();
                return result;
            }
        }

        /// <summary>
        /// Method checks if any record is returned based on given predicate.
        /// </summary>
        /// <param name="predicate">Expression predicate</param>
        /// <returns>true or false</returns>
        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Any(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}