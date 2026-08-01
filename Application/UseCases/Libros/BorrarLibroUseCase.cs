using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Libros
{
    public class BorrarLibroUseCase
    {
        private readonly IRepository<LibroEntity, int> _repository;

        public BorrarLibroUseCase(IRepository<LibroEntity, int> repository)
        {
            _repository = repository;

        }

        public async Task ExecuteAsync(int id)
        {
            var libro = await _repository.GetByIdAsync(id);

            if (libro == null)
                throw new InvalidOperationException($"No se encontro un libro con el Id: {id}");

            await _repository.DeleteAsync(libro);
            await _repository.SaveChangesAsync();
        }
    }
}
