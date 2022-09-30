using System;
using System.Collections.Generic;

namespace BE
{
    public partial class Documento
    {
        public Documento()
        {
            Personas = new HashSet<Persona>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Abreviatura { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
