using ASP.groep.L.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Seeding
{
    public static class TreeFarmSeeding
    {
        public static void Seed(this EntityTypeBuilder<TreeFarm> modelBuilder)
        {
            modelBuilder.HasData(
                new TreeFarm()
                {
                    Id = 1,
                    Name = "BTP",
                    Sites = new List<Site>(),
                }
            );
        }
    }
}
