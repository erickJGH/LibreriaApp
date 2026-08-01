using Application.Dtos.Libros;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Libros
{
    public class ObtenerLibrosAntesde2000UseCase
    {
        private readonly ILibroRepository<LibroEntity> _repository;

        public ObtenerLibrosAntesde2000UseCase(ILibroRepository<LibroEntity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetLibrosAntesdel2000Dto>> ExecuteAsync()
        {

            var libros = await _repository.GetAntesde2000Async();

            return libros.Select(a => new GetLibrosAntesdel2000Dto
            {
                libro_id = a.libro_id,
                titulo = a.titulo,
                ano_publicacion = a.ano_publicacion
            });
            
            
        }

    }
}
