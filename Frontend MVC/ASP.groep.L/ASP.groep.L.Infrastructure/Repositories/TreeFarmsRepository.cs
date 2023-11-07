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
    public class TreeFarmsRepository : ITreeFarmsRepository
    {
        private readonly ApplicationDbContext context;

        public TreeFarmsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TreeFarm>> GetAll(int pageNr, int pageSize)
        {
            return await context.TreeFarms.Skip((pageNr - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<TreeFarm> GetById(int id)
        {
            return await context.TreeFarms.FirstOrDefaultAsync(p => p.Id == id);
        }

        public TreeFarm Create(TreeFarm newTreeFarm)
        {
            context.ChangeTracker.Clear();
            context.TreeFarms.Add(newTreeFarm);
            context.SaveChanges();
            return newTreeFarm;
        }

        public void Delete(TreeFarm treeFarm)
        {
            context.ChangeTracker.Clear();
            context.TreeFarms.Remove(treeFarm);
            context.SaveChanges();
        }

        public TreeFarm Update(TreeFarm modifiedTreeFarm)
        {
            context.ChangeTracker.Clear();
            context.TreeFarms.Update(modifiedTreeFarm);
            context.SaveChanges();
            return modifiedTreeFarm;
        }

        public void Delete(IEnumerable<TreeFarm> treeFarms)
        {
            context.ChangeTracker.Clear();
            context.TreeFarms.RemoveRange(treeFarms);
            context.SaveChanges();
        }
    }
}
