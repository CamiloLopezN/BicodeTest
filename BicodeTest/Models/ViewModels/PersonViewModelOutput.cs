namespace BicodeTest.Models.ViewModels
{
    public class PersonViewModelOutput
    {

        private Persona _persona;
        public string PersonDocumentType { get; set; }
        public string PersonGender { get; set; }
        public string? PersonName { get; set; }
        public string? PersonSurname { get; set; }
        public long? PersonDocumentNumber { get; set; }
        public int PersonAge { get; set; }
        public string PersonClassification { get; set; }

        public PersonViewModelOutput(Persona persona)
        {
            this._persona = persona;
            this.PersonDocumentType = GetDocumentType();
            this.PersonGender = GetGender();
            this.PersonName = persona.Nombre;
            this.PersonSurname = persona.Apellido;
            this.PersonDocumentNumber = persona.NumeroDocumento;
            this.PersonAge = CalculateAge();
            this.PersonClassification = GetClassification(this.PersonAge);
        }

        private string GetDocumentType()
        {
            return _persona.IdDocumento == 1 ? "CC" : _persona.IdDocumento == 2 ? "TI" : "CE";
        }

        private string GetGender()
        {
            return _persona.IdGenero == 1 ? "Masculino" : _persona.IdGenero == 2 ? "Femenino" : "Otro";
        }

        private int CalculateAge()
        {
            return DateTime.Today.AddTicks(-_persona.FechaNacimiento.Ticks).Year - 1;
        }

        private string GetClassification(int age)
        {
            return age >= 0 && age <= 14 ? "Niño" : age >= 15 && age <= 20 ? "Adolescente"
                : age >= 21 && age <= 60 ? "Mayor de edad" : "Tercera edad";
        }
    }


}
