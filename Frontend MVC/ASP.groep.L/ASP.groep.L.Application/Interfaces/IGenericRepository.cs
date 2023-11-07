using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll(int pageNr, int pageSize);
        Task<T> GetById(int id);
        T Create(T newEntity);
        T Update(T modifiedEntity);
        void Delete(T entity);
    }
}
