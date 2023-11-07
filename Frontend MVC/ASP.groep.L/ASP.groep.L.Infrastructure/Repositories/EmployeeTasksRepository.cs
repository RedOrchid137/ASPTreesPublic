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
    public class EmployeeTasksRepository : IEmployeeTasksRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeTasksRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<EmployeeTask>> GetAll(int pageNr, int pageSize)
        {
            return await context.EmployeeTasks.Include(e => e.WorkSchedule).Include(e=>e.WorkSchedule.Employee).Include(e => e.WorkSchedule.Planner).Include(e => e.Zone).Skip((pageNr - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<EmployeeTask> GetById(int id)
        {
            return await context.EmployeeTasks.Include(e=>e.WorkSchedule).ThenInclude(e=>e.EmployeeTasks).ThenInclude(e=>e.Zone).ThenInclude(e=>e.EmployeeTasks).Include(e=>e.Zone).FirstOrDefaultAsync(p => p.Id == id);
        }

        public EmployeeTask Create(EmployeeTask newEmployeeTask)
        {
            context.ChangeTracker.Clear();
            newEmployeeTask.Zone = null;
            newEmployeeTask.WorkSchedule = null;
            context.EmployeeTasks.Add(newEmployeeTask);
            context.SaveChanges();
            return newEmployeeTask;
        }

        public void Delete(EmployeeTask person)
        {
            context.ChangeTracker.Clear();
            context.EmployeeTasks.Remove(person);
            context.SaveChanges();
        }

        public EmployeeTask Update(EmployeeTask modifiedEmployeeTask)
        {
            context.ChangeTracker.Clear();
            context.EmployeeTasks.Update(modifiedEmployeeTask);
            context.SaveChanges();
            return modifiedEmployeeTask;
        }

        public void Delete(IEnumerable<EmployeeTask> employeeTasks)
        {
            context.ChangeTracker.Clear();
            context.EmployeeTasks.RemoveRange(employeeTasks);
            context.SaveChanges();
        }

        public async Task<IEnumerable<EmployeeTask>> GetAllBySchedule(WorkSchedule schedule)
        {
            return await context.EmployeeTasks.Include(e => e.WorkSchedule).Include(e => e.Zone).Where(e=>e.WorkSchedule==schedule).ToListAsync();
        }
    }
}
