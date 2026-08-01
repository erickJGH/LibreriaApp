using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Prestamos
{
    public class CrearPrestamoDto
    {
        public int libro_id { get; set; }
        public DateTime? fecha_prestamo { get; set; }
        
    }
}
