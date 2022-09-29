using BicodeTest.models;
using BicodeTest.Models;
using BicodeTest.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BicodeTest.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonsController : ControllerBase
    {

        public PersonsController()
        {

        }


        [MapToApiVersion("2.0")]
        [HttpPost]
        public IActionResult CreatePerson(PersonViewModelInput personViewModel)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                using (BI_TESTGENContext dataBase = new BI_TESTGENContext())
                {
                    Persona persona = new Persona();
                    NewPersonAssignValues(personViewModel, persona);
                    dataBase.Personas.Add(persona);
                    dataBase.SaveChanges();
                    resultStructure.Message = $"La persona con numero de documento: {personViewModel.PersonDocumentNumber} ha sido creada satisfactoriamente.";
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"Ha ocurrido un error al crear la persona con id: .  Excepción: {exception}";
            }
            return Ok(resultStructure);
        }

        [MapToApiVersion("2.0")]
        [HttpGet("{idDocument}/{documentNumber}")]
        public IActionResult GetPersonByDocumentNumber(int idDocument, long documentNumber)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                using (BI_TESTGENContext dataBase = new BI_TESTGENContext())
                {
                    Persona person = dataBase.Personas.Single(itemPerson => itemPerson.IdDocumento == idDocument && itemPerson.NumeroDocumento == documentNumber);
                    resultStructure.Result = person;
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"Se produjo un error al obtener la información de la persona con numero de documento: {documentNumber}. Excepción: {exception}";
            }
            return Ok(resultStructure);
        }


        [MapToApiVersion("2.0")]
        [HttpGet]
        /*
         * La versión 1.0 Lista las personas y lo devuelve con la estructura de resultados sin tener en cuenta el estatus. 
         * 
         */
        public IActionResult GetPersons()
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                using (BI_TESTGENContext dataBase = new BI_TESTGENContext())
                {
                    var personList = dataBase.Personas.ToList();
                    resultStructure.Result = personList;
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error durante la obtención de personas! Excepción: {exception}";
            }
            return Ok(resultStructure);

        }

        [MapToApiVersion("2.0")]
        [HttpPut]
        public IActionResult EditPerson(PersonViewModelInput personViewModel)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                using (BI_TESTGENContext dataBase = new BI_TESTGENContext())
                {
                    Persona person = dataBase.Personas.Single(itemPerson => itemPerson.IdDocumento == personViewModel.PersonIdDocument && itemPerson.NumeroDocumento == personViewModel.PersonDocumentNumber);
                    person.IdGenero = personViewModel.PersonIdGender;
                    person.Nombre = personViewModel.PersonName;
                    person.Apellido = personViewModel.PersonSurname;
                    person.FechaNacimiento = personViewModel.PersonBirthday;
                    person.FechaActualizacion = DateTime.Now;
                    dataBase.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dataBase.SaveChanges();
                    resultStructure.Message = $"La persona con numero de documento: {personViewModel.PersonDocumentNumber} ha sido modificado satisfactoriamente.";
                }

            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error al actualizar a la persona con numero de identificación: {personViewModel.PersonDocumentNumber}. Excepción: {exception}";
            }
            return Ok(resultStructure);
        }

        [MapToApiVersion("2.0")]
        [HttpDelete("{idDocument}/{documentNumber}")]
        public IActionResult DeletePerson(int idDocument, long documentNumber)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                using (BI_TESTGENContext dataBase = new BI_TESTGENContext())
                {
                    Persona person = dataBase.Personas.Single(itemPerson => itemPerson.IdDocumento == idDocument && itemPerson.NumeroDocumento == documentNumber);
                    dataBase.Personas.Remove(person);
                    dataBase.SaveChanges();
                    resultStructure.Message = $"La persona con numero de documento: {documentNumber} ha sido eliminada satisfactoriamente.";
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error al eliminar la persona con numero de documento: {documentNumber}. Excepción: {exception}";
            }
            return Ok(resultStructure);
        }

        [MapToApiVersion("2.0")]
        [HttpGet, Route("alive")]
        public string Alive()
        {
            return "Captain, 2.0 Here. I'm Alive and Kicking!";
        }

        private static void NewPersonAssignValues(PersonViewModelInput personViewModel, Persona persona)
        {
            persona.IdDocumento = personViewModel.PersonIdDocument;
            persona.IdGenero = personViewModel.PersonIdGender;
            persona.Nombre = personViewModel.PersonName;
            persona.Apellido = personViewModel.PersonSurname;
            persona.NumeroDocumento = personViewModel.PersonDocumentNumber;
            persona.FechaNacimiento = personViewModel.PersonBirthday;
            persona.FechaCreacion = DateTime.Now;
            persona.FechaActualizacion = DateTime.Now;
        }

    }
}
