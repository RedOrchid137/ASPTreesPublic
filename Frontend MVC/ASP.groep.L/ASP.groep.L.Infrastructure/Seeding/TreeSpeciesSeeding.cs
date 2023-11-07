using ASP.groep.L.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Seeding
{
    public static class TreeSpeciesSeeding
    {
        public static void Seed(this EntityTypeBuilder<TreeSpecies> modelBuilder)
        {
            modelBuilder.HasData(
                new TreeSpecies()
                {
                    Id = 1,
                    Name = "Mangrove Tree",
                    Description = "A specific tree originated from Swamp biomes",
                    MaintenancePlanID = 1,
                    MaintenancePlan = null,
                    PicturePath = "/images/MangroveTree.png",
                    Zones = new List<Zone>()
                }
                //},
                //new TreeSpecies()
                //{
                //    Id = 2,
                //    Name = "Oak Tree",
                //    Description = "An oak is a tree or shrub in the genus Quercus of the beech family, Fagaceae.",
                //    MaintenancePlanID = 1,
                //    EmployeeTasks = null,
                //    MaintenancePlan = null,
                //    PicturePath = "/images/OakTree.png",
                //    Zone = null
                //}
            );
        }
    }
}
