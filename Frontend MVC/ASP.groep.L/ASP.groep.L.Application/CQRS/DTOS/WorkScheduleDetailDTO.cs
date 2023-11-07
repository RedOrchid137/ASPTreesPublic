using ASP.groep.L.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.DTOS
{
    public class WorkScheduleDetailDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public UserDetailDTO Employee { get; set; }
        public int PlannerId { get; set; }
        public UserDetailDTO PlannerDetailDTO { get; set; }
        public string? Description { get; set; }
    }
}
