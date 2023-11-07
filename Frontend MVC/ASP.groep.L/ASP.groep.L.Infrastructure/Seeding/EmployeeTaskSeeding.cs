using ASP.groep.L.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Seeding
{
    public static class EmployeeTaskSeeding
    {
        public static void Seed(this EntityTypeBuilder<EmployeeTask> modelBuilder)
        {
            modelBuilder.HasData(
                new EmployeeTask()
                {
                    Id = 1,
                    Name = "Task: Adding New Mangrove TreeSpecies",
                    scheduledStart = DateTime.Now,
                    scheduledStop = new DateTime(DateTime.Now.Ticks+10000),
                    WorkScheduleId = 1,
                    WorkSchedule = null,
                    ZoneId = 1,
                    Zone = null,
                    Description = "",
                    Status = Status.Todo,
                    Priority = Priority.Low
                }
            );
        }
    }
}
