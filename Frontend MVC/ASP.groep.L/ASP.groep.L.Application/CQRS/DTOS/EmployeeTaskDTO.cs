using ASP.groep.L.Domain;
using ASP.groep.L.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.DTOS
{
    public class EmployeeTaskDTO
    {
        public int? WorkScheduleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime scheduledStart { get; set; }
        public DateTime scheduledStop { get; set; }
        public int ZoneId { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
