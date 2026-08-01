using Data.Persistence;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class AutorRepository : IRepository<AutorEntity,int>
    {
        private readonly ApplicationDbContext _context;

        public AutorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AutorEntity?> GetByIdAsync(int id)
        {
            return await _context.Autores
                .FirstOrDefaultAsync(a => a.autor_id == id);
        }

        public async Task<IEnumerable<AutorEntity>> GetAllAsync()
        {
            return await _context.Autores
                .AsNoTracking()
                .OrderBy(a => a.autor_id)
                .ThenBy(a => a.nombre)
                .ToListAsync();
        }
        public async Task AddAsync(AutorEntity autor)
        {
            if(autor== null)
                throw new ArgumentNullException(nameof(autor));

            await _context.Autores.AddAsync(autor);
        }

        public Task UpdateAsync(AutorEntity autor)
        {
            if(autor== null)
                throw new ArgumentNullException(nameof(autor));

            _context.Autores.Update(autor);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(AutorEntity autor)
        {
            if(autor == null)
                throw new ArgumentNullException(nameof(autor));

            _context.Autores.Remove(autor);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
