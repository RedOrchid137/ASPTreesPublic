using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Infrastructure.Contexts;
using ASP.groep.L.Infrastructure.Repositories;
using ASP.groep.L.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.Extensions
{
    public static class Registrator
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.RegisterDbContext();
            services.RegisterRepositories();
            return services;
        }
        public static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer("ASPGroepL"));

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkSchedulesRepository, WorkSchedulesRepository>();
            services.AddScoped<IAddressesRepository, AddressesRepository>();
            services.AddScoped<IEmployeeTasksRepository, EmployeeTasksRepository>();
            services.AddScoped<IMaintenancePlansRepository, MaintenancePlansRepository>();
            services.AddScoped<ITreeFarmsRepository, TreeFarmsRepository>();
            services.AddScoped<ITreeSpeciesRepository, TreeSpeciesRepository>();
            services.AddScoped<ISitesRepository, SitesRepository>();
            services.AddScoped<IZonesRepository, ZonesRepository>();
            services.AddScoped<IUnitofWork, UnitofWork>();
            return services;
        }
    }
}
