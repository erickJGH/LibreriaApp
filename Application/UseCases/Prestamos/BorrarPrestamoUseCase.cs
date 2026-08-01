using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Prestamos
{
    public class BorrarPrestamoUseCase
    {

        private readonly IRepository<PrestamoEntity, int> _repository;

        public BorrarPrestamoUseCase(IRepository<PrestamoEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int id)
        {
            var prestamo = await _repository.GetByIdAsync(id);

            if (prestamo == null)
                throw new InvalidOperationException($"No se encontro un prestamo con el id: {id}");

            if (!prestamo.fecha_devolucion.HasValue)
                throw new InvalidOperationException("No se puede borrar un prestamo activo o sin devolver el libro");

            await _repository.DeleteAsync(prestamo);
            await _repository.SaveChangesAsync();
        }

    }
}
