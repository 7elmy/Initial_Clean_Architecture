using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Data.Repositories
{
    internal class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #region DQL
        public async Task<TEntity> GetByIdAsync(object id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;
                if (disableTracking) query = query.AsNoTracking();

                if (include != null) query = include(query);

                if (predicate != null) query = query.Where(predicate);

                if (orderBy != null)
                    return await orderBy(query).FirstOrDefaultAsync();
                return await query.FirstOrDefaultAsync();
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TEntity> GetListPaginate(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int index = 0, int size = 10, bool disableTracking = true)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;
                if (disableTracking) query = query.AsNoTracking();

                if (include != null) query = include(query);

                if (predicate != null) query = query.Where(predicate);

                if (orderBy != null)
                    return orderBy(query).Skip(index).Take(size);
                return query.Skip(index).Take(size);
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;
                if (disableTracking) query = query.AsNoTracking();

                if (include != null) query = include(query);

                if (predicate != null) query = query.Where(predicate);

                if (orderBy != null)
                    return orderBy(query).ToList();
                return query.ToList();
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion


        #region DML
        public async Task<bool> AddAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);

                return true;
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> AddAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);

                return true;
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);

                return true;
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.UpdateRange(entities);

                return true;
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool Remove(object id)
        {
            try
            {
                var entity = _dbSet.Find(id);
                _dbSet.Remove(entity);

                return true;
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool Remove(IEnumerable<object> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    Remove(id);
                }
                return true;
            }
            //TODO: log ex
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #endregion
    }
}
