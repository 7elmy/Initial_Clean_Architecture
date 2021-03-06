﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Initial_Clean_Architecture.Data.Repositories;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Data.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork
         where TContext : DbContext, IDisposable
    {
        private Dictionary<Type, object> _repositories;
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepositoryAsync<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
                _repositories[type] = new RepositoryAsync<TEntity>(_context);
            return (IRepositoryAsync<TEntity>)_repositories[type];
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
