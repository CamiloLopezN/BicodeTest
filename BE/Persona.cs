using System;
using System.Collections.Generic;

namespace BE
{
    public partial class Persona
    {
        public int Id { get; set; }
        public int? IdDocumento { get; set; }
        public int? IdGenero { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public long? NumeroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public virtual Documento? IdDocumentoNavigation { get; set; }
        public virtual Genero? IdGeneroNavigation { get; set; }

        public Persona(int id, int? idDocumento, int? idGenero, string? nombre, string? apellido,
            long? numeroDeDocumento, DateTime fechaNacimiento, DateTime? fechaActualizacion)
        {
            Id = id;
            IdDocumento = idDocumento;
            IdGenero = idGenero;
            Nombre = nombre;
            Apellido = apellido;
            NumeroDocumento = numeroDeDocumento;
            FechaNacimiento = fechaNacimiento;
            FechaActualizacion = fechaActualizacion;
        }

        public Persona()
        {

        }

    }
}
