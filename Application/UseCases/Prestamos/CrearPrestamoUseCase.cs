using Application.Dtos.Prestamos;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Prestamos
{
    public class CrearPrestamoUseCase
    {
        private readonly IRepository<PrestamoEntity, int> _prestamoRepository;
        private readonly IRepository<LibroEntity, int> _libroRepository;
        private readonly IPrestamoRepository<PrestamoEntity> _prestamoRepository2;

        public CrearPrestamoUseCase(IRepository<PrestamoEntity, int> prestamoRepository, IRepository<LibroEntity, int> libroRepository,
            IPrestamoRepository<PrestamoEntity> prestamoRepository2)
        {
            _prestamoRepository = prestamoRepository;
            _libroRepository = libroRepository;
            _prestamoRepository2 = prestamoRepository2;
        }

        public async Task<PrestamoEntity> ExecuteAsync(CrearPrestamoDto dto)
        {
            int libro_id = 0;

           

            if (dto.libro_id > 0)
            {
               var  libro = await _libroRepository.GetByIdAsync(dto.libro_id);

                if (libro == null)
                    throw new InvalidOperationException($"No se encontro un libro con el codigo: {dto.libro_id}");

                libro_id =  libro.libro_id;
            }
            else
            {
                throw new InvalidOperationException("Debe proporcionar un libro_id valido para registrar el prestamo.");
            }

            if (await _prestamoRepository2.ExistePrestamoActivoAsync(libro_id))
                throw new InvalidOperationException("El libro ya se encuentra prestado.");

            var prestamo = new PrestamoEntity(libro_id, dto.fecha_prestamo);

       

            await _prestamoRepository.AddAsync(prestamo);
            await _prestamoRepository.SaveChangesAsync();

            return prestamo;

        }


    }
}
