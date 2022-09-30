using SL.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using BLL.Services;
using BE;
using SL.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SL.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService _peopleService)
        {
            this._peopleService = _peopleService;
        }


        [HttpPost]
        [MapToApiVersion("2.0"), ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePerson(PersonViewModelInput personViewModelInput)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                Persona person = new Persona()
                {
                    IdDocumento = personViewModelInput.PersonIdDocument,
                    IdGenero = personViewModelInput.PersonIdGender,
                    Nombre = personViewModelInput.PersonName,
                    Apellido = personViewModelInput.PersonSurname,
                    NumeroDocumento = personViewModelInput.PersonDocumentNumber,
                    FechaNacimiento = personViewModelInput.PersonBirthday,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };
                await _peopleService.Insert(person);
                resultStructure.Message = $"La persona con numero de documento: {personViewModelInput.PersonDocumentNumber} ha sido creada satisfactoriamente.";
                resultStructure.State = 200;
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"Ha ocurrido un error al crear la persona con id: .  Excepción: {exception.Message}";
                resultStructure.State = 500;
            }
            return Ok(resultStructure);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("2.0"), ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetPersonById([Required] int id)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                Persona person = await _peopleService.Get(id);

                resultStructure.Result = new PersonViewModelOutput(person);
                resultStructure.State = 200;
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"Se produjo un error al obtener la información de la persona con numero de documento: {id}. Excepción: {exception.Message}";
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
        public async Task<IActionResult> GetPeople()
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                IQueryable<Persona> queryPersonSql = await _peopleService.GetAll();
                List<PersonViewModelOutput> listPersons = queryPersonSql.Select(person => new PersonViewModelOutput(person)).ToList();
                resultStructure.Result = listPersons;
                resultStructure.State = 200;
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
        public async Task<IActionResult> UpdatePerson([Required] int personId, PersonViewModelInput personViewModelInput)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                Persona person = new Persona(personId, personViewModelInput.PersonIdDocument, personViewModelInput.PersonIdGender,
                    personViewModelInput.PersonName, personViewModelInput.PersonSurname, personViewModelInput.PersonDocumentNumber
                    , personViewModelInput.PersonBirthday, DateTime.Now);
                await _peopleService.Update(person);
                resultStructure.Message = $"La persona con numero de documento: {personViewModelInput.PersonDocumentNumber} ha sido modificado satisfactoriamente.";
                resultStructure.State = 200;
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error al actualizar a la persona con numero de identificación: {personViewModelInput.PersonDocumentNumber}. Excepción: {exception.Message}";
                resultStructure.State = 500;
            }
            return Ok(resultStructure);
        }


        [MapToApiVersion("2.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([Required] int id)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                await _peopleService.Delete(id);
                resultStructure.Message = $"La persona con numero de documento: {id} ha sido eliminada satisfactoriamente.";
                resultStructure.State = 200;
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error al eliminar la persona con numero de documento: {id}. Excepción: {exception.Message}";
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

    }
}
