using ASP.groep.L.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.DTOS
{
    public class SiteDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BluePrint { get; set; }
        public TreeFarmDetailDTO TreeFarm { get; set; }
        public AddressDetailDTO Address { get; set; }

    }
}
