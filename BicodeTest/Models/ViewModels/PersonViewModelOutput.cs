namespace BicodeTest.Models.ViewModels
{
    public class PersonViewModelOutput : Persona
    {

        public string PersonClassification { get; set; }


        public PersonViewModelOutput()
        {
            this.PersonClassification = GetClassification(CalculateAge());
        }

        private int CalculateAge()
        {
            return DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1;
        }

        private string GetClassification(int age)
        {
            if (age >= 0 && age <= 14)
            {
                return "Niño";
            }
            if (age >= 15 && age <= 20)
            {
                return "Adolescente";
            }
            if (age >= 21 && age <= 60)
            {
                return "Mayor de edad";
            }
            return "Tercera edad";
        }

    }


}
