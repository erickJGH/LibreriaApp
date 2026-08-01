using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Prestamos
{
    public class ObtenerTodosLosPrestamosUseCase
    {
        private readonly IRepository<PrestamoEntity, int> _repository;

        public ObtenerTodosLosPrestamosUseCase(IRepository<PrestamoEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PrestamoEntity>> ExecuteAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
