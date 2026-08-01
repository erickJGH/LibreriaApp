using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Prestamos
{
    public class ObtenerPrestamoPorIdUseCase
    {
        private readonly IRepository<PrestamoEntity, int> _repository;

        public ObtenerPrestamoPorIdUseCase(IRepository<PrestamoEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task<PrestamoEntity> ExecuteAsync(int id)
        {
            var prestamo = await _repository.GetByIdAsync(id);

            if (prestamo == null)
                throw new InvalidOperationException($"No se encontro un prestamo con el Id: {id}");

            return prestamo;
        }

    }
}
