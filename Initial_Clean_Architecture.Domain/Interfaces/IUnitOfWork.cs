using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
