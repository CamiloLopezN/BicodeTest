using System;
using System.Collections.Generic;

namespace BicodeTest.Models
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
