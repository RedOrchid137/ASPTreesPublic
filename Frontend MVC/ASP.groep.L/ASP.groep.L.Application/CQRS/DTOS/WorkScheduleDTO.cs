using ASP.groep.L.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.DTOS
{
    public class WorkScheduleDTO
    {
        public int? Id { get; set; }
        public String Description { get; set; }
        public int EmployeeId { get; set; }

        public int PlannerId { get; set; }
        public IEnumerable<EmployeeTaskDTO> EmployeeTasks { get; set; }
    }
}
