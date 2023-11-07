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
    public class WorkSchedulesRepository : IWorkSchedulesRepository
    {
        private readonly ApplicationDbContext context;

        public WorkSchedulesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<WorkSchedule>> GetAll(int pageNr, int pageSize)
        {
            return await context.WorkSchedules.Include(e => e.EmployeeTasks).ThenInclude(e=>e.Zone).ThenInclude(e => e.Site).Include(w => w.Employee).Include(w => w.Planner).Skip((pageNr - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<WorkSchedule> GetById(int id)
        {
            return await context.WorkSchedules.Include(e => e.EmployeeTasks).ThenInclude(e => e.Zone).ThenInclude(e => e.Site).Include(w=>w.Employee).Include(w => w.Planner).FirstOrDefaultAsync(p => p.Id == id);
        }

        public WorkSchedule Create(WorkSchedule newPerson)
        {
            newPerson.EmployeeTasks = new List<EmployeeTask>();
            context.ChangeTracker.Clear();
            context.WorkSchedules.Add(newPerson);
            context.SaveChanges();
            return newPerson;
        }

        public void Delete(WorkSchedule person)
        {
            context.ChangeTracker.Clear();
            context.WorkSchedules.Remove(person);
            context.SaveChanges();
        }

        public WorkSchedule Update(WorkSchedule modifiedPerson)
        {
            context.ChangeTracker.Clear();
            context.WorkSchedules.Update(modifiedPerson);
            context.SaveChanges();
            return modifiedPerson;
        }

        public void Delete(IEnumerable<WorkSchedule> workSchedules)
        {
            context.ChangeTracker.Clear();
            context.WorkSchedules.RemoveRange(workSchedules);
            context.SaveChanges();
        }
    }
}
