using ASP.groep.L.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.DTOS
{
    public class SiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TreeFarmId { get; set; }
        public int AddressId { get; set; }
    }
}
