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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetAll(int pageNr, int pageSize)
        {
            return await context.Users.Skip((pageNr - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> GetByEmail(String email)
        {
            return await context.Users.FirstOrDefaultAsync(p => p.Email == email);
        }

        public User Create(User newPerson)
        {
            context.ChangeTracker.Clear();
            context.Users.Add(newPerson);
            context.SaveChanges();
            return newPerson;
        }

        public void Delete(User person)
        {
            context.ChangeTracker.Clear();
            context.Users.Remove(person);
            context.SaveChanges();
        }

        public User Update(User modifiedPerson)
        {
            context.ChangeTracker.Clear();
            context.Users.Update(modifiedPerson);
            context.SaveChanges();
            return modifiedPerson;
        }

        public void Delete(IEnumerable<User> User)
        {
            context.ChangeTracker.Clear();
            context.Users.RemoveRange(User);
            context.SaveChanges();
        }

    }
}
