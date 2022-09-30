using BE;
using DAL.DataBaseConnection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PersonRepository : IGenericRepository<Persona>
    {
        private readonly BI_TESTGENContext _BI_TESTGENContext;

        public PersonRepository(BI_TESTGENContext bI_TESTGENContext)
        {
            this._BI_TESTGENContext = bI_TESTGENContext;
        }

        public async Task<bool> Delete(int id)
        {
            Persona person = _BI_TESTGENContext.Personas.First(itemPerson => itemPerson.Id == id);
            _BI_TESTGENContext.Personas.Remove(person);
            await _BI_TESTGENContext.SaveChangesAsync();
            return true;
        }

        public async Task<Persona> Get(int id)
        {
            return await _BI_TESTGENContext.Personas.SingleAsync(itemPerson => itemPerson.Id == id);
        }

        public async Task<IQueryable<Persona>> GetAll()
        {
            IQueryable<Persona> queryPersonsSql = _BI_TESTGENContext.Personas;
            return queryPersonsSql;
        }

        public async Task<bool> Insert(Persona entityModel)
        {
            _BI_TESTGENContext.Personas.Add(entityModel);
            await _BI_TESTGENContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Persona entityModel)
        {
            _BI_TESTGENContext.Entry(entityModel).State = EntityState.Modified; // exception when trying to change the state
            _BI_TESTGENContext.Entry(entityModel).Property(person => person.FechaCreacion).IsModified = false;
            await _BI_TESTGENContext.SaveChangesAsync();
            return true;
        }
    }
}
