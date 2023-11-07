using ASP.groep.L.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Seeding
{
    public static class AddressSeeding
    {
        public static void Seed(this EntityTypeBuilder<Address> modelBuilder)
        {
            modelBuilder.HasData(
                new Address()
                {
                    Id = 1,
                    StreetName = "Ellermansstraat",
                    Commune = "Antwerpen",
                    ZipCode = "2060",
                    HouseNr = "33",
                    Site = null
                }
            );
        }
    }
}
