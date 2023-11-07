using ASP.groep.L.AdminMVC.MiddleWare;
using ASP.groep.L.Application.CQRS.Behaviours;
//using ASP.groep.L.Application.CQRS.Behaviours;
using ASP.groep.L.Application.CQRS.Mappings;
using ASP.groep.L.Application.CQRS.Validators.Commands.Create;
using ASP.groep.L.Application.Extensions;
using ASP.groep.L.Application.FileManagement;
using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Infrastructure.Contexts;
using ASP.groep.L.Infrastructure.Extensions;
using ASP.groep.L.Infrastructure.Repositories;
using ASP.groep.L.Infrastructure.UoW;
using Auth0.AspNetCore.Authentication;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ASP.groep.L.WebAPI
{
    public class Startup
    {

        private ConfigurationBuilder builder { get; set; }
        public IConfiguration Configuration { get; }
        public Startup()
        {
            builder = (ConfigurationBuilder?)new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }




        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // options.UseSqlServer(Configuration.GetConnectionString("ASPGroepL"),
                //     b => b.MigrationsAssembly("ASP.groep.L.Infrastructure"));
                options.UseNpgsql(Configuration.GetConnectionString("PostGres"),
                   b => b.MigrationsAssembly("ASP.groep.L.Infrastructure"));
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });
            services.RegisterInfrastructure();
            services.RegisterApplication();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeTaskCommandValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddAuth0WebAppAuthentication(options => {
                     options.Domain = Configuration["Auth0:Domain"];
                     options.ClientId = Configuration["Auth0:ClientId"];
                 });
            services.AddAuthorization(
            
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });
            //Toevoegen van de SQL server

            //repos
            services.AddTransient<IAddressesRepository, AddressesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmployeeTasksRepository, EmployeeTasksRepository>();
            services.AddTransient<IMaintenancePlansRepository, MaintenancePlansRepository>();
            services.AddTransient<ISitesRepository, SitesRepository>();
            services.AddTransient<ITreeFarmsRepository, TreeFarmsRepository>();
            services.AddTransient<ITreeSpeciesRepository, TreeSpeciesRepository>();
            services.AddTransient<IWorkSchedulesRepository, WorkSchedulesRepository>();
            services.AddTransient<IZonesRepository, ZonesRepository>();
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IUnitofWork, UnitofWork>();
            services.AddMvc();
            services.AddAutoMapper(typeof(Mappings));
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<ExceptionHandlingMiddleWare>();


            app.UseHttpsRedirection();


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }

}
