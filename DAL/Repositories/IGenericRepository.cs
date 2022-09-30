using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<bool> Insert(TEntityModel entityModel);
        Task<bool> Update(TEntityModel entityModel);
        Task<bool> Delete(int id);
        Task<TEntityModel> Get(int id);
        Task<IQueryable<TEntityModel>> GetAll();
    }
}
