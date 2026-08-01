using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Libros
{
    public class GetLibrosAntesdel2000Dto
    {
        public int libro_id { get; set; }
        public string titulo { get; set; } = string.Empty;
        public DateTime ano_publicacion { get; set; }
        
    }
}
