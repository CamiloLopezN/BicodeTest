namespace BicodeTest.Models.ViewModels
{
    public class PersonViewModelInput
    {
        public int? PersonIdDocument { get; set; }
        public int? PersonIdGender { get; set; }
        public string? PersonName { get; set; }
        public string? PersonSurname { get; set; }
        public long? PersonDocumentNumber { get; set; }
        public DateTime PersonBirthday { get; set; }
    }
}
