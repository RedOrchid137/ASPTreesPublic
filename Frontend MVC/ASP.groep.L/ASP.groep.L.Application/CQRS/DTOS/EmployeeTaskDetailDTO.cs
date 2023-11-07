using ASP.groep.L.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.DTOS
{
    public class EmployeeTaskDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int WorkScheduleId { get; set; }
        public int ZoneId { get; set; }
        public DateTime scheduledStart { get; set; }
        public DateTime scheduledStop { get; set; }

        public DateTime actualStart { get; set; }
        public DateTime actualStop { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; } 
    }
}
