using ASP.groep.L.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ASP.groep.L.Infrastructure.Seeding
{
    public static class UserSeeding
    {
        public static void Seed(this EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Bart",
                    LastName = "Bartmans",
                    Email= "employee@trees.com",
                    Role = Role.Ionic_Employee
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Kurt",
                    LastName = "Kurtmans",
                    Email = "planner@trees.com",
                    Role = Role.MVC_Planner
                }
            );
        }
    }
}
