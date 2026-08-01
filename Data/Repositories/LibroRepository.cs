using Data.Persistence;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class LibroRepository: IRepository<LibroEntity, int>, ILibroRepository<LibroEntity>
    {
        private readonly ApplicationDbContext _context;

        public LibroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LibroEntity?> GetByIdAsync(int id)
        {
            return await _context.Libros
                .Include(l => l.autor)
                .FirstOrDefaultAsync(l => l.libro_id == id);
        }

        public async Task<IEnumerable<LibroEntity>> GetAllAsync()
        {
            return await _context.Libros
                .AsNoTracking()
                .Include(l => l.autor)
                .OrderBy(l => l.libro_id)
                .ToListAsync();
        }

        public async Task AddAsync(LibroEntity libro)
        {
            if (libro == null)
                throw new ArgumentNullException(nameof(libro));

           await _context.Libros.AddAsync(libro);
        }

        public Task UpdateAsync(LibroEntity libro)
        {
            if(libro == null)
                throw new ArgumentNullException(nameof(libro));

            _context.Libros.Update(libro);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(LibroEntity libro)
        {
            if(libro == null)
                throw new ArgumentNullException(nameof(libro));

            _context.Libros.Remove(libro);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //ILibroRepository

        public async Task<IEnumerable<LibroEntity>> GetAntesde2000Async()
        {
            return await _context.Libros
                .AsNoTracking()
                .Where(l => l.ano_publicacion.Year < 2000  )
                .OrderByDescending(l => l.ano_publicacion)
                .ToListAsync();
        }

    }
}
