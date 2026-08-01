using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Autores
{
    public class ObtenerTodosAutoresUseCase
    {
        private readonly IRepository<AutorEntity, int> _repository;

        public ObtenerTodosAutoresUseCase(IRepository<AutorEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AutorEntity>> ExecuteAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
