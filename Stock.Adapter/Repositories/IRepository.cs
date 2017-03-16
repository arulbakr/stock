using System;
using System.Linq;
using System.Linq.Expressions;


namespace Stock.Adapter.Repositories
{
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        int Add(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        int Remove(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(TEntity entity);

        /// <summary>
        /// Method checks if any record is returned based on given predicate.
        /// </summary>
        /// <param name="predicate">Expression predicate</param>
        /// <returns>true or false</returns>
        bool IsExist(Expression<Func<TEntity, bool>> predicate);
    }
}