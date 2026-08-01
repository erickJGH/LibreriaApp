using Application.Dtos.Libros;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Libros
{
    public class ObtenerTodosLosLibrosUseCase
    {
        private readonly IRepository<LibroEntity, int> _repository;

        public ObtenerTodosLosLibrosUseCase(IRepository<LibroEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LibroEntity>> ExecuteAsync()
        {

            var libros = await _repository.GetAllAsync();

            return libros;
        }
    }
}
