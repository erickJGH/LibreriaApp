using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain
{
    public class LibroEntity
    {
        public int libro_id { get; private set; }
        public string titulo { get; private set; } = string.Empty;
        public int autor_id { get; private set; }
        public AutorEntity? autor { get; private set; }
        public DateTime ano_publicacion { get; private set; }
        public string genero { get; private set;  } = string.Empty;


        public LibroEntity() { }

        public LibroEntity(string titulo,int autor_id,DateTime ano_publicacion,string genero)
        {
            validarTitulo(titulo);
            validarAno_Publicacion(ano_publicacion);
            validarGenero(genero);

            if ( autor_id <= 0)
                throw new ArgumentException("El id de el actor no puede estar vacio", nameof(autor_id));

            this.titulo = titulo;
            this.autor_id = autor_id;
            this.ano_publicacion = ano_publicacion;
            this.genero = genero;

        }

        public void ActualizarLibro(string titulo, int autor_id, DateTime ano_publicacion, string genero)
        {
            validarTitulo(titulo);
            validarAno_Publicacion(ano_publicacion);
            validarGenero(genero);

            if (autor_id <= 0)
                throw new ArgumentException("El id de el actor no puede estar vacio", nameof(autor_id));

            this.titulo = titulo;
            this.autor_id = autor_id;
            this.ano_publicacion = ano_publicacion;
            this.genero = genero;

        }



        public void validarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El titulo no puede estar vacio", nameof(titulo));

            if (titulo.Trim().Length < 2)
                throw new ArgumentException("El titulo debe tener al menos 2 caracteres", nameof(titulo));

            if (titulo.Trim().Length > 80)
                throw new ArgumentException("El titulo no puede tener mas de 80 caracteres", nameof(titulo));
        }

        public void validarAno_Publicacion(DateTime ano_publicacion)
        { 
            if (ano_publicacion > DateTime.Now)
                throw new ArgumentException("La fecha de publicacion no puede ser posterior a la fecha actual");
          
        }

        public void validarGenero(string genero) 
        {
            if (string.IsNullOrWhiteSpace(genero))
                throw new ArgumentException("El genero no puede estar vacio", nameof(genero));

            if (genero.Trim().Length < 2)
                throw new ArgumentException("El genero debe tener al menos 2 caracteres", nameof(genero));

            if (genero.Trim().Length > 15)
                throw new ArgumentException("El genero no puede tener mas de 15 caracteres", nameof(genero));
        }



    }
}
