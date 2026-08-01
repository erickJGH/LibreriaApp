using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class PrestamoEntity
    {
        public int prestamo_id { get; private set; }
        public int libro_id { get; private set; }
        public LibroEntity? libro { get; private set; }
        public DateTime fecha_prestamo { get; private set; }
        public DateTime? fecha_devolucion { get; private set; }

        public PrestamoEntity() { }

        public PrestamoEntity(int libro_id,DateTime? fecha_prestamo= null)
        {
            var fecha  = fecha_prestamo ?? DateTime.UtcNow.AddHours(-4);
            if (libro_id <= 0)
                throw new ArgumentException("deber introducir un id de libro valido",nameof(libro_id));

            this.libro_id = libro_id;
            this.fecha_prestamo = fecha;

        }

        public void RegistrarTerminoPrestamo(DateTime? fecha_devolucion= null )
        {
            var devolucion = fecha_devolucion ?? DateTime.UtcNow.AddHours(-4);

            if (this.fecha_devolucion.HasValue)
                throw new InvalidOperationException("Este prestamo ya tiene una fecha de devolucion");

            if (devolucion < this.fecha_prestamo)
                throw new ArgumentException("la fecha de devolucion no puede ser anterior a la fecha que el libro fue prestado",nameof(fecha_devolucion) );

            this.fecha_devolucion = devolucion;

        }
    }
}
