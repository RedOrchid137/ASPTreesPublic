using ASP.groep.L.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.DTOS
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string StreetName { get; set; }

        public string Commune { get; set; }
        public SiteDTO Site { get; set; }
    }
}
