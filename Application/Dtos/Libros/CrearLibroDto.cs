using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Libros
{
    public class CrearLibroDto
    {
        public string titulo { get; set; } = string.Empty;
        public int autor_id { get;set; }
        public DateTime ano_publicacion { get; set; }
        public string genero { get; set; } = string.Empty;
    }
}
