using ASP.groep.L.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Seeding
{
    public static class SiteSeeding
    {
        public static void Seed(this EntityTypeBuilder<Site> modelBuilder)
        {
            modelBuilder.HasData(
                new Site()
                {
                    Id = 1,
                    Name = "Site01",
                    BluePrint = "",
                    BluePrintName = "",
                    Description = "First Site",
                    AddressId = 1,
                    Address = null,
                    TreeFarmId = 1,
                    TreeFarm = null,
                }
            );
        }
    }
}
