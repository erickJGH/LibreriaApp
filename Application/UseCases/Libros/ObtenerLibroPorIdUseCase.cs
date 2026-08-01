using Application.Dtos.Libros;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Application.UseCases.Libros
{
    public class ObtenerLibroPorIdUseCase
    {
        private readonly IRepository<LibroEntity, int> _repository;

        public ObtenerLibroPorIdUseCase(IRepository<LibroEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task<LibroEntity> ExecuteAsync(int id)
        {
            var libro = await _repository.GetByIdAsync(id);

            if (libro == null)
                throw new InvalidOperationException($"No se encontro un libro con el Id: {id}");

            return libro;
            //return new GetLibrosDto
            //{
            //    libro_id = libro.libro_id,
            //    titulo = libro.titulo,
            //    autor_id = libro.autor_id,
            //    ano_publicacion = libro.ano_publicacion,
            //    genero = libro.genero
            //};
        }
    }
}
