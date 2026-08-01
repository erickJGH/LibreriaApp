using Application.Dtos.Autores;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Autores
{
    public class ActualizarActorUseCase
    {
        private readonly IRepository<AutorEntity, int> _repository;


        public ActualizarActorUseCase(IRepository<AutorEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task<AutorEntity> ExecuteAsync(int id,ActualizarAutorDto dto)
        {
            var autor = await _repository.GetByIdAsync(id);
            if (autor == null)
                throw new InvalidOperationException($"No se encontro un actor con el Id: {id}");

            autor.ActualizarAutorInfo(dto.nombre, dto.nacionalidad);

            await _repository.UpdateAsync(autor);
            await _repository.SaveChangesAsync();

            return autor;
        }


    }
}
