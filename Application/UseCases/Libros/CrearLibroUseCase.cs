using Application.Dtos.Libros;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Libros
{
    public class CrearLibroUseCase
    {

        private readonly IRepository<LibroEntity, int> _libroRepository;
        private readonly IRepository<AutorEntity,int> _autorRepository;

        public CrearLibroUseCase(IRepository<LibroEntity, int> libroRepository, IRepository<AutorEntity, int> autorRepository)
        {
            _libroRepository = libroRepository;
            _autorRepository = autorRepository;
        }

        public async Task<LibroEntity> ExecuteAsync(CrearLibroDto dto)
        {
            int autor_id = 0;

            if (dto.autor_id > 0)
            {
                var autor = await _autorRepository.GetByIdAsync(dto.autor_id);

                if (autor == null)
                    throw new InvalidOperationException($"No se encontro el autor con el id: {dto.autor_id}");

                autor_id = autor.autor_id;
            }
            else
            {
                throw new InvalidOperationException("Debe proporcionar un autor_id valido para crear un libro.");
            }

            var libro = new LibroEntity(dto.titulo, autor_id, dto.ano_publicacion, dto.genero);

            await _libroRepository.AddAsync(libro);
            await _libroRepository.SaveChangesAsync();

            return libro;

            
        }
    }
}
