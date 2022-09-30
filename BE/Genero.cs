using System;
using System.Collections.Generic;

namespace BE
{
    public partial class Genero
    {
        public Genero()
        {
            Personas = new HashSet<Persona>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
