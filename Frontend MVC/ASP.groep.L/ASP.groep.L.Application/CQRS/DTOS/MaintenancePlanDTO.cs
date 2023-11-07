using ASP.groep.L.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.DTOS
{
    public class MaintenancePlanDTO
    {
        public int Id { get; set; }
        public Season Season { get; set; }
        public TreeSpeciesDTO TreeSpecies { get; set; }
        public String Link { get; set; }
    }
}
