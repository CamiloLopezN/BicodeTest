using System.ComponentModel.DataAnnotations;

namespace SL.Models.ViewModels
{
    public class PersonViewModelInput
    {

        [Required, Range(1, 3)]
        public int? PersonIdDocument { get; set; }

        [Required, Range(1, 3)]
        public int? PersonIdGender { get; set; }

        [Required, StringLength(200)]
        public string? PersonName { get; set; }

        [Required, StringLength(200)]
        public string? PersonSurname { get; set; }

        [Required]
        public long? PersonDocumentNumber { get; set; }

        [Required]
        public DateTime PersonBirthday { get; set; }
    }
}
