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
    public class SitesRepository : ISitesRepository
    {
        private readonly ApplicationDbContext context;

        public SitesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Site>> GetAll(int pageNr, int pageSize)
        {
            return await context.Sites.Include(s => s.Address).Include(s => s.TreeFarm).Include(z => z.Zones).Skip((pageNr - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Site> GetById(int id)
        {
            return await context.Sites.Include(s => s.Address).Include(s=>s.TreeFarm).Include(z => z.Zones).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Site Create(Site newSite)
        {
            context.ChangeTracker.Clear();
            context.Sites.Add(newSite);
            context.SaveChanges();
            return newSite;
        }

        public void Delete(Site site)
        {
            context.ChangeTracker.Clear();
            context.Sites.Remove(site);
            context.SaveChanges();
        }

        public Site Update(Site modifiedSite)
        {
            context.ChangeTracker.Clear();
            context.Sites.Update(modifiedSite);
            context.SaveChanges();
            return modifiedSite;
        }

        public void Delete(IEnumerable<Site> sites)
        {
            context.ChangeTracker.Clear();
            context.Sites.RemoveRange(sites);
            context.SaveChanges();
        }
    }
}
