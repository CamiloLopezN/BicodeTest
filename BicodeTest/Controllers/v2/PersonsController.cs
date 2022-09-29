using BicodeTest.models;
using BicodeTest.Models;
using BicodeTest.Models.ViewModels;
using BicodeTest.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace BicodeTest.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonsController : ControllerBase
    {

        public PersonsController() { }

        [HttpPost]
        [MapToApiVersion("2.0"), ServiceFilter(typeof(ValidationFilterAttribute))]
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
                    resultStructure.State = 200;
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"Ha ocurrido un error al crear la persona con id: .  Excepción: {exception.Message}";
                resultStructure.State = 500;
            }
            return Ok(resultStructure);
        }

        [HttpGet("{idDocument}/{documentNumber}")]
        [MapToApiVersion("2.0"), ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult GetPersonByDocumentNumber([Required] int idDocument, [Required] long documentNumber)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                using (BI_TESTGENContext dataBase = new BI_TESTGENContext())
                {
                    Persona person = dataBase.Personas.Single(itemPerson => itemPerson.IdDocumento == idDocument && itemPerson.NumeroDocumento == documentNumber);
                    resultStructure.Result = new PersonViewModelOutput(person);
                    resultStructure.State = 200;
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"Se produjo un error al obtener la información de la persona con numero de documento: {documentNumber}. Excepción: {exception.Message}";
                resultStructure.State = 500;
            }
            return Ok(resultStructure);
        }


        [HttpGet]
        [MapToApiVersion("2.0")]
        /*
         * La versión 2.0 Lista las personas y lo devuelve con la estructura de resultados teniendo en cuenta el estatus de la petición, 
         * la estructura definida en el viewmodel de salida.. 
         * 
         */
        public IActionResult GetPersons()
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                using (BI_TESTGENContext dataBase = new BI_TESTGENContext())
                {
                    var personsOutput = new List<PersonViewModelOutput>();
                    foreach (Persona person in dataBase.Personas.ToList())
                    {
                        personsOutput.Add(new PersonViewModelOutput(person));
                    }
                    resultStructure.Result = personsOutput;
                    resultStructure.State = 200;
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error durante la obtención de personas! Excepción: {exception.Message}";
                resultStructure.State = 500;
            }
            return Ok(resultStructure);

        }

        [HttpPut]
        [MapToApiVersion("2.0"), ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult EditPerson(PersonViewModelInput personViewModel)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                using (BI_TESTGENContext dataBase = new BI_TESTGENContext())
                {
                    Persona person = dataBase.Personas.Single(itemPerson => itemPerson.IdDocumento == personViewModel.PersonIdDocument && itemPerson.NumeroDocumento == personViewModel.PersonDocumentNumber);
                    EditPersonAssignValue(personViewModel, person);
                    dataBase.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dataBase.SaveChanges();
                    resultStructure.Message = $"La persona con numero de documento: {personViewModel.PersonDocumentNumber} ha sido modificado satisfactoriamente.";
                    resultStructure.State = 200;
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error al actualizar a la persona con numero de identificación: {personViewModel.PersonDocumentNumber}. Excepción: {exception.Message}";
                resultStructure.State = 500;
            }
            return Ok(resultStructure);
        }


        [MapToApiVersion("2.0")]
        [HttpDelete("{idDocument}/{documentNumber}")]
        public IActionResult DeletePerson([Required] int idDocument, [Required] long documentNumber)
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
                    resultStructure.State = 200;
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error al eliminar la persona con numero de documento: {documentNumber}. Excepción: {exception.Message}";
                resultStructure.State = 500;
            }
            return Ok(resultStructure);
        }

        [MapToApiVersion("2.0")]
        [HttpGet, Route("alive")]
        public IActionResult Alive()
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                resultStructure.Message = "¡La versión 2.0 esta funcionando correctamente!";
                resultStructure.State = 200;
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"Ha ocurrido un error al realizar la petición. Excepción: {exception.Message}";
            }
            return Ok(resultStructure);
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

        private static void EditPersonAssignValue(PersonViewModelInput personViewModel, Persona person)
        {
            person.IdGenero = personViewModel.PersonIdGender;
            person.Nombre = personViewModel.PersonName;
            person.Apellido = personViewModel.PersonSurname;
            person.FechaNacimiento = personViewModel.PersonBirthday;
            person.FechaActualizacion = DateTime.Now;
        }

    }
}
