using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions
{
    public interface ILibroRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAntesde2000Async();
    }
}
