using BE;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IGenericRepository<Persona> _genericRepository;

        public PeopleService(IGenericRepository<Persona> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _genericRepository.Delete(id);
        }

        public async Task<Persona> Get(int id)
        {
            return await _genericRepository.Get(id);
        }

        public async Task<IQueryable<Persona>> GetAll()
        {
            return await _genericRepository.GetAll();
        }

        public async Task<bool> Insert(Persona person)
        {
            return await _genericRepository.Insert(person);
        }

        public async Task<bool> Update(Persona person)
        {
            return await _genericRepository.Update(person);
        }
    }
}
