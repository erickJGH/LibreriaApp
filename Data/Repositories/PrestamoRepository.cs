using Data.Persistence;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class PrestamoRepository : IRepository<PrestamoEntity, int>, IPrestamoRepository<PrestamoEntity>
    {

        private readonly ApplicationDbContext _context;

        public PrestamoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PrestamoEntity?> GetByIdAsync(int id)
        {
            return await _context.Prestamos
                .Include(p => p.libro)
                .ThenInclude(p => p.autor)
                .FirstOrDefaultAsync(p => p.prestamo_id == id);
        }

        public async Task<IEnumerable<PrestamoEntity>> GetAllAsync()
        {
            return await _context.Prestamos
                .AsNoTracking()
                .Include(p => p.libro)
                .ThenInclude(p => p.autor)
                .OrderBy(p => p.fecha_prestamo)
                .ToListAsync();
        }

        public async Task AddAsync(PrestamoEntity prestamo)
        {
            if (prestamo == null)
                throw new ArgumentNullException(nameof(prestamo));

            await _context.Prestamos.AddAsync(prestamo);
        }

        public Task UpdateAsync(PrestamoEntity prestamo)
        {
            if (prestamo == null)
                throw new ArgumentNullException(nameof(prestamo));

            _context.Prestamos.Update(prestamo);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(PrestamoEntity prestamo)
        {
            if (prestamo == null)
                throw new ArgumentNullException(nameof(prestamo));

            _context.Prestamos.Remove(prestamo);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //IPrestamoRepository

        public async Task<IEnumerable<PrestamoEntity>> GetNoDevueltoAsync()
        {
            return await _context.Prestamos
                .AsNoTracking()
                .Include(p => p.libro)
                .ThenInclude(l => l.autor)
                .Where(l => l.fecha_devolucion == null)
                .OrderBy(p => p.fecha_prestamo)
                .ToListAsync();
        }

        public async Task<bool> ExistePrestamoActivoAsync(int libroId)
        {
            return await _context.Prestamos
                .AsNoTracking()
                .AnyAsync(p =>
                    p.libro_id == libroId &&
                    !p.fecha_devolucion.HasValue);
        }


    }
}
