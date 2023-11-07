using ASP.groep.L.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Seeding
{
    public static class WorkScheduleSeeding
    {
        public static void Seed(this EntityTypeBuilder<WorkSchedule> modelBuilder)
        {
            modelBuilder.HasData(
                new WorkSchedule()
                {
                    Id = 1,
                    EmployeeId = 1,
                    Employee = null,
                    PlannerId = 2,
                    Planner = null,
                    EmployeeTasks = new List<EmployeeTask>(),
                    Description = "First Workschedule from planner to worker about the tasks."
                }
            ); ;
        }
    }
}
