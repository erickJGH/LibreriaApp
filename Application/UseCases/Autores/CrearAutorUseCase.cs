using Application.Dtos.Autores;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Autores
{
    public class CrearAutorUseCase
    {
        private readonly IRepository<AutorEntity, int> _repository;

        public CrearAutorUseCase(IRepository<AutorEntity, int> repository) 
        { 
            _repository = repository;
        }

        public async Task<AutorEntity> ExecuteAsync(CrearAutorDto dto) 
        {


            var autor = new AutorEntity(dto.nombre, dto.nacionalidad);

            await _repository.AddAsync(autor);
            await _repository.SaveChangesAsync();

            return autor;
        }
    }
}
