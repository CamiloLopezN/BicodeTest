using BE;

namespace SL.Models.ViewModels
{
    public class PersonViewModelOutput
    {
        private Persona _persona;
        public int PersonId { get; set; }
        public string PersonDocumentType { get; set; }
        public string PersonGender { get; set; }
        public string? PersonName { get; set; }
        public string? PersonSurname { get; set; }
        public long? PersonDocumentNumber { get; set; }
        public int PersonAge { get; set; }
        public string PersonClassification { get; set; }

        public PersonViewModelOutput(Persona persona)
        {
            _persona = persona;
            this.InitializerValues();
        }

        private void InitializerValues()
        {
            PersonId = _persona.Id;
            PersonDocumentType = GetDocumentType();
            PersonGender = GetGender();
            PersonName = _persona.Nombre;
            PersonSurname = _persona.Apellido;
            PersonDocumentNumber = _persona.NumeroDocumento;
            PersonAge = CalculateAge();
            PersonClassification = GetClassification(PersonAge);
        }
        public string GetDocumentType()
        {
            return _persona.IdDocumento == 1 ? "CC" : _persona.IdDocumento == 2 ? "TI" : "CE";
        }

        public string GetGender()
        {
            return _persona.IdGenero == 1 ? "Masculino" : _persona.IdGenero == 2 ? "Femenino" : "Otro";
        }

        public int CalculateAge()
        {
            return DateTime.Today.AddTicks(-_persona.FechaNacimiento.Ticks).Year - 1;
        }

        public string GetClassification(int age)
        {
            return age >= 0 && age <= 14 ? "Niño" : age >= 15 && age <= 20 ? "Adolescente"
                : age >= 21 && age <= 60 ? "Mayor de edad" : "Tercera edad";
        }


    }
}
