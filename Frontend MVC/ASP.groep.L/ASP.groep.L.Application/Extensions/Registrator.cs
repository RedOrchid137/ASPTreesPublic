using ASP.groep.L.Application.CQRS.Behaviours;
using ASP.groep.L.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.Extensions
{
    public static class Registrator
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            //services.AddScoped<IEmployeesService, EmployeesService>();
            //services.AddScoped<IPlannersService, PlannersService>();
            //services.AddScoped<IWorkSchedulesService, WorkSchedulesService>();
            //services.AddScoped<IAddressesService, AddressesService>();
            //services.AddScoped<IEmployeeTasksService, EmployeeTasksService>();
            //services.AddScoped<IMaintenancePlansService, MaintenancePlansService>();
            //services.AddScoped<IQRCodesService, QRCodesService>();
            //services.AddScoped<ISitesService, SitesService>();
            //services.AddScoped<ITreeFarmsService, TreeFarmsService>();
            //services.AddScoped<ITreeSpeciesService, TreeSpeciesService>();
            //services.AddScoped<IZonesService, ZonesService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(typeof(Registrator).Assembly);
            //ervices.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }

    }
}
