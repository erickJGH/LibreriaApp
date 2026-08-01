using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions
{
    public interface IPrestamoRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetNoDevueltoAsync();

        Task<bool> ExistePrestamoActivoAsync(int libroId);
    }
}
