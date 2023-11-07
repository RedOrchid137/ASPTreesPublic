using ASP.groep.L.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Seeding
{
    public static class MaintenancePlanSeeding
    {
        public static void Seed(this EntityTypeBuilder<MaintenancePlan> modelBuilder)
        {
            modelBuilder.HasData(
                new MaintenancePlan()
                {
                    Id = 1,
                    Title = "MaintenancePlan01",
                    Link = "/test/link",
                    Season = Season.Winter,
                    QRCodeName = null,
                    QRCode = "testURL",
                    FileName = null,
                    Description = "First Maintenance Plan",
                    TreeSpecies = null,
                }
            );
        }
    }
}
