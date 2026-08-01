using Application.Dtos.Prestamos;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Prestamos
{
    public class ObtenerPrestamosNoDevueltosUseCase
    {
        private readonly IPrestamoRepository<PrestamoEntity> _repository;


        public ObtenerPrestamosNoDevueltosUseCase(IPrestamoRepository<PrestamoEntity> repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<GetPrestamosNoDevueltosDto>> ExecuteAsync() 
        {
            var prestamos = await _repository.GetNoDevueltoAsync();

            return prestamos.Select( p => new GetPrestamosNoDevueltosDto
            {
                nombre = p.libro.autor.nombre,
                titulo = p.libro.titulo
            });
        }
    }
}
