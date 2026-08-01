using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Autores
{
    public class BorrarAutorUseCase
    {
        private readonly IRepository<AutorEntity,int> _repository;

        public BorrarAutorUseCase(IRepository<AutorEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int id)
        {
            var autor = await _repository.GetByIdAsync(id);

            if (autor == null)
                throw new InvalidOperationException($"No se encontro un autor con el Id: {id}");

            await _repository.DeleteAsync(autor);
            await _repository.SaveChangesAsync();

        }
    }
}
