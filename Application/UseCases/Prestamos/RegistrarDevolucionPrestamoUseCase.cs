using Application.Dtos.Prestamos;
using Domain;
using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Prestamos
{
    public class RegistrarDevolucionPrestamoUseCase
    {

        private readonly IRepository<PrestamoEntity, int> _repository;


        public RegistrarDevolucionPrestamoUseCase(IRepository<PrestamoEntity, int> repository)
        {
            _repository = repository;
        }

        public async Task<PrestamoEntity> ExecuteAsync(int id,RegistrarTerminoPrestamoDto dto)
        {
            PrestamoEntity? prestamo;

            prestamo = await _repository.GetByIdAsync(id);

            if (prestamo == null)
                throw new InvalidOperationException($"No existe ningun prestamo con el id: {id}");

            prestamo.RegistrarTerminoPrestamo(dto.fecha_devolucion);

            await _repository.UpdateAsync(prestamo);
            await _repository.SaveChangesAsync();

            return prestamo;

        }
    }
}
