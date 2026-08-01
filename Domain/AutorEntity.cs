using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AutorEntity
    {
        public int autor_id { get; private set; }
        public string nombre { get; private set; } = string.Empty;
        public string nacionalidad { get; private set; } = string.Empty;


        public AutorEntity(string nombre, string nacionalidad)
        {
            ValidarNombre(nombre);
            ValidarNacionalidad(nacionalidad);

            this.nombre = nombre.Trim();
            this.nacionalidad = nacionalidad.Trim();
        }

        public void ActualizarAutorInfo(string nombre,string nacionalidad)
        {
            ValidarNombre(nombre);
            ValidarNacionalidad(nacionalidad);

            this.nombre = nombre.Trim();
            this.nacionalidad = nacionalidad.Trim();

        }


        private void ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacio", nameof(nombre));

            if (nombre.Trim().Length < 2)
                throw new ArgumentException("El nombre debe tener al menos 2 caracteres", nameof(nombre));

            if (nombre.Trim().Length > 50)
                throw new ArgumentException("El nombre no puede tener mas de 50 caracteres", nameof(nombre));
        }

        private void ValidarNacionalidad(string nacionalidad)
        {
            if (string.IsNullOrWhiteSpace(nacionalidad))
                throw new ArgumentException("La nacionalidad no puede estar vacio", nameof(nacionalidad));

            if (nacionalidad.Trim().Length < 2)
                throw new ArgumentException("La Nacionalidad debe tener al menos 2 caracteres", nameof(nacionalidad));

            if (nacionalidad.Trim().Length > 50)
                throw new ArgumentException("la Nacionalidad no puede tener mas de 50 caracteres", nameof(nacionalidad));
        }

        

    }
}
