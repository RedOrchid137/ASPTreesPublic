using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using ASP.groep.L.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Repositories
{
    public class MaintenancePlansRepository : IMaintenancePlansRepository
    {
        private readonly ApplicationDbContext context;

        public MaintenancePlansRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<MaintenancePlan>> GetAll(int pageNr, int pageSize)
        {
            return await context.MaintenancePlans.Skip((pageNr - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<MaintenancePlan> GetById(int id)
        {
            return await context.MaintenancePlans.FirstOrDefaultAsync(p => p.Id == id);
        }

        public MaintenancePlan Create(MaintenancePlan newMaintenancePlan)
        {
            context.ChangeTracker.Clear();
            context.MaintenancePlans.Add(newMaintenancePlan);
            context.SaveChanges();
            return newMaintenancePlan;
        }

        public void Delete(MaintenancePlan maintenancePlan)
        {
            context.ChangeTracker.Clear();
            context.MaintenancePlans.Remove(maintenancePlan);
            context.SaveChanges();
        }

        public MaintenancePlan Update(MaintenancePlan modifiedMaintenancePlan)
        {
            context.ChangeTracker.Clear();
            context.MaintenancePlans.Update(modifiedMaintenancePlan);
            context.SaveChanges();
            return modifiedMaintenancePlan;
        }

        public void Delete(IEnumerable<MaintenancePlan> maintenancePlans)
        {
            context.ChangeTracker.Clear();
            context.MaintenancePlans.RemoveRange(maintenancePlans);
            context.SaveChanges();
        }
    }
}
