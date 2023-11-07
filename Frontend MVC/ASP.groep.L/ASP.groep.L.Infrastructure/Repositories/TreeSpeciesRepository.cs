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
    public class TreeSpeciesRepository : ITreeSpeciesRepository
    {
        private readonly ApplicationDbContext context;

        public TreeSpeciesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TreeSpecies>> GetAll(int pageNr, int pageSize)
        {
            return await context.TreeSpecies
                .Include(m => m.MaintenancePlan)
                .Include(z => z.Zones)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TreeSpecies> GetById(int id)
        {
            return await context.TreeSpecies
                .Include(m => m.MaintenancePlan)
                .Include(z => z.Zones)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public TreeSpecies Create(TreeSpecies newTreeSpecies)
        {
            context.ChangeTracker.Clear();
            context.TreeSpecies.Add(newTreeSpecies);
            context.SaveChanges();
            return newTreeSpecies;
        }

        public void Delete(TreeSpecies TreeSpecies)
        {
            context.ChangeTracker.Clear();
            context.TreeSpecies.Remove(TreeSpecies);
            context.SaveChanges();
        }

        public TreeSpecies Update(TreeSpecies modifiedTreeSpecies)
        {
            context.ChangeTracker.Clear();
            context.TreeSpecies.Update(modifiedTreeSpecies);
            context.SaveChanges();
            return modifiedTreeSpecies;
        }

        public void Delete(IEnumerable<TreeSpecies> TreeSpecies)
        {
            context.ChangeTracker.Clear();
            context.TreeSpecies.RemoveRange(TreeSpecies);
            context.SaveChanges();
        }

        
    }
}
