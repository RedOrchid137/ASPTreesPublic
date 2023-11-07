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
    public class ZonesRepository : IZonesRepository
    {
        private readonly ApplicationDbContext context;

        public ZonesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Zone>> GetAll(int pageNr, int pageSize)
        {
            return await context.Zones.Include(z=>z.EmployeeTasks).Include(z => z.Site).Include(z => z.TreeSpecies).Skip((pageNr - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Zone> GetById(int id)
        {
            return await context.Zones.Include(z => z.EmployeeTasks).Include(z => z.Site).ThenInclude(z => z.Zones).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Zone Create(Zone newZone)
        {
            context.ChangeTracker.Clear();
            context.Zones.Add(newZone);
            context.SaveChanges();
            return newZone;
        }

        public void Delete(Zone zone)
        {
            context.ChangeTracker.Clear();
            context.Zones.Remove(zone);
            context.SaveChanges();
        }

        public Zone Update(Zone modifiedZone)
        {
            context.ChangeTracker.Clear();
            context.Zones.Update(modifiedZone);
            context.SaveChanges();
            return modifiedZone;
        }

        public void Delete(IEnumerable<Zone> zones)
        {
            context.ChangeTracker.Clear();
            context.Zones.RemoveRange(zones);
            context.SaveChanges();
        }
    }
}
