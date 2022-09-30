using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IPeopleService
    {
        Task<bool> Insert(Persona person);
        Task<bool> Update(Persona person);
        Task<bool> Delete(int id);
        Task<Persona> Get(int id);
        Task<IQueryable<Persona>> GetAll();
    }
}
