using ASP.groep.L.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Seeding
{
    public static class ZoneSeeding
    {
        public static void Seed(this EntityTypeBuilder<Zone> modelBuilder)
        {
            modelBuilder.HasData(
                new Zone()
                {
                    Id = 1,
                    Name = "Zone01",
                    Description = "First Zone",
                    SurfaceArea = 500,
                    SiteId = 1,
                    Site = null,
                    TreeSpeciesId = 1,
                    TreeSpecies = null,
                    EmployeeTasks = new List<EmployeeTask>()
                }
            );
        }
    }
}
