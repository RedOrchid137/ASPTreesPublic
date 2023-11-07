using ASP.groep.L.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.Interfaces
{
    public interface IAddressesRepository : IGenericRepository<Address>
    {
        void Delete(IEnumerable<Address> addresses);
    }
}
