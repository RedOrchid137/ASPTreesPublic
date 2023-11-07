using ASP.groep.L.Domain;
using ASP.groep.L.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace ASP.groep.L.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<MaintenancePlan> MaintenancePlans { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<EmployeeTask> EmployeeTasks { get; set; }
        public virtual DbSet<TreeFarm> TreeFarms { get; set; }
        public virtual DbSet<TreeSpecies> TreeSpecies { get; set; }
        public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<WorkSchedule>()
                .HasOne<User>(p => p.Employee)
                .WithMany(e => e.EmployeeWorkSchedules)
                .HasForeignKey(i => i.EmployeeId)
                .OnDelete(DeleteBehavior.ClientNoAction);
            modelBuilder.Entity<WorkSchedule>()
                .HasOne<User>(p => p.Planner)
                .WithMany(e => e.PlannerWorkSchedules)
                .HasForeignKey(i => i.PlannerId)
                .OnDelete(DeleteBehavior.ClientNoAction);


            //Seeding
            /*modelBuilder.Entity<User>().Seed();
            modelBuilder.Entity<WorkSchedule>().Seed();
            modelBuilder.Entity<Address>().Seed();
            modelBuilder.Entity<EmployeeTask>().Seed();
            modelBuilder.Entity<MaintenancePlan>().Seed();
            modelBuilder.Entity<TreeSpecies>().Seed();*/
            modelBuilder.Entity<TreeFarm>().Seed();

            /*modelBuilder.Entity<Site>().Seed();
             modelBuilder.Entity<Zone>().Seed();*/


        }
    }
}
