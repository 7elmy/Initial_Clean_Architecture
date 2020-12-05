using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Data.Domain.Interfaces
{
    public interface IRepositoryAsync<TEntity> where TEntity : class
    {
        #region DQL
        Task<TEntity> GetByIdAsync(object id);

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        IEnumerable<TEntity> GetListPaginate(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int index = 0,
            int size = 10,
            bool disableTracking = true);

        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);
        #endregion



        #region DML
        Task<bool> AddAsync(TEntity entity);
        Task<bool> AddAsync(IEnumerable<TEntity> entities);
        bool Update(TEntity entity);
        bool Update(IEnumerable<TEntity> entities);
        bool Remove(object id);
        bool Remove(IEnumerable<object> ids);
        #endregion
    }
}
