using Application.Dtos.Libros;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Libros
{
    public class ActualizarLibroUseCase
    {
        private readonly IRepository<LibroEntity, int> _repository;

        public ActualizarLibroUseCase(IRepository<LibroEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task<LibroEntity> ExecuteAsync(int id,ActualizarLibroDto dto)
        {
            var libro = await _repository.GetByIdAsync(id);

            if (libro == null)
                throw new InvalidOperationException($"No se encontro un libro con el Id: {id}");

            libro.ActualizarLibro(dto.titulo, dto.autor_id, dto.ano_publicacion, dto.genero);

            await _repository.UpdateAsync(libro);
            await _repository.SaveChangesAsync();

            return libro;
        }
    }
}
