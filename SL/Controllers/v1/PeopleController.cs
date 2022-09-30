using BE;
using BLL.Services;
using DAL.DataBaseConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SL.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService _peopleService) { this._peopleService = _peopleService; }

        [MapToApiVersion("1.0")]
        [HttpPost]
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

            }
            catch (Exception exception)
            {
                resultStructure.Message = $"Ha ocurrido un error al crear la persona con id:  {personViewModelInput.PersonDocumentNumber}.  Excepción: {exception}";
            }
            return Ok(resultStructure);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById([Required] int id)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                Persona person = await _peopleService.Get(id);
                resultStructure.Result = new PersonViewModelOutput(person);
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"Se produjo un error al obtener la información de la persona con numero de documento: {id}. Excepción: {exception}";
            }
            return Ok(resultStructure);
        }


        [MapToApiVersion("1.0")]
        [HttpGet]
        /*
         * La versión 1.0 Lista las personas y lo devuelve con la estructura de resultados sin tener en cuenta el estatus. 
         * 
         */
        public async Task<IActionResult> GetPersons()
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                IQueryable<Persona> queryPersonSql = await _peopleService.GetAll();
                List<PersonViewModelOutput> listPersons = queryPersonSql.Select(person => new PersonViewModelOutput(person)).ToList();
                resultStructure.Result = listPersons;
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error durante la obtención de personas! Excepción: {exception}";
            }
            return Ok(resultStructure);

        }

        [MapToApiVersion("1.0")]
        [HttpPut]
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
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error al actualizar a la persona con numero de identificación:  {personViewModelInput.PersonDocumentNumber}. Excepción: {exception}";
            }
            return Ok(resultStructure);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([Required] int id)
        {
            ResultStructure resultStructure = new ResultStructure();
            try
            {
                using (BI_TESTGENContext dataBase = new BI_TESTGENContext())
                {
                    await _peopleService.Delete(id);
                    resultStructure.Message = $"La persona con numero de documento: {id} ha sido eliminada satisfactoriamente.";
                }
            }
            catch (Exception exception)
            {
                resultStructure.Message = $"¡Ha ocurrido un error al eliminar la persona con numero de documento: {id}. Excepción: {exception}";
            }
            return Ok(resultStructure);
        }

        [MapToApiVersion("1.0")]
        [HttpGet, Route("alive")]
        public string Alive()
        {
            return "¡La versión 1.0 esta funcionando correctamente!";
        }



    }
}
