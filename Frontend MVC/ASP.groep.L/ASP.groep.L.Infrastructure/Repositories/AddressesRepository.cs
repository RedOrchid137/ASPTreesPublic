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
    public class AddressesRepository : IAddressesRepository
    {
        private readonly ApplicationDbContext context;

        public AddressesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Address>> GetAll(int pageNr, int pageSize)
        {
            return await context.Addresses.Skip((pageNr - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Address> GetById(int id)
        {
            return await context.Addresses.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Address Create(Address newAddress)
        {
            context.ChangeTracker.Clear();
            context.Addresses.Add(newAddress);
            context.SaveChanges();
            return newAddress;
        }

        public void Delete(Address person)
        {
            context.ChangeTracker.Clear();
            context.Addresses.Remove(person);
            context.SaveChanges();
        }

        public Address Update(Address modifiedAddress)
        {
            context.ChangeTracker.Clear();
            context.Addresses.Update(modifiedAddress);
            context.SaveChanges();
            return modifiedAddress;
        }

        public void Delete(IEnumerable<Address> addresses)
        {
            context.ChangeTracker.Clear();
            context.Addresses.RemoveRange(addresses);
            context.SaveChanges();
        }
    }
}
