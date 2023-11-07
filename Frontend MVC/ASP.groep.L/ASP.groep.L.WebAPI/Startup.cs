//using ASP.groep.L.Application.CQRS.Behaviours;
using ASP.groep.L.Application.CQRS.Mappings;
using ASP.groep.L.Application.Extensions;
using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Infrastructure.Contexts;
using ASP.groep.L.Infrastructure.Extensions;
using ASP.groep.L.Infrastructure.Repositories;
using ASP.groep.L.Infrastructure.UoW;
using ASP.groep.L.WebAPI.Authentication;
using ASP.groep.L.WebAPI.MiddleWare;
using Auth0.AspNetCore.Authentication;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity.UI;
using System.Reflection;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

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
            //SQL
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ASPGroepL"),
                    b => b.MigrationsAssembly("ASP.groep.L.Infrastructure"));
                //options.UseNpgsql(Configuration.GetConnectionString("PostGres"),
                //    b => b.MigrationsAssembly("ASP.groep.L.Infrastructure"));
                //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role",
                    ValidateAudience = false,
                };
            });

            services
              .AddAuthorization(options =>
              {
                  options.AddPolicy("ReadZones", policy => policy.Requirements.Add(new HasScopeRequirement("read:zones", $"https://{Configuration["Auth0:Domain"]}/")));
                  options.AddPolicy("EditZones", policy => policy.Requirements.Add(new HasScopeRequirement("edit:zones", $"https://{Configuration["Auth0:Domain"]}/")));
                  
                  options.AddPolicy("ReadTasks", policy => policy.Requirements.Add(new HasScopeRequirement("read:tasks", $"https://{Configuration["Auth0:Domain"]}/")));
                  options.AddPolicy("EditTasks", policy => policy.Requirements.Add(new HasScopeRequirement("edit:tasks", $"https://{Configuration["Auth0:Domain"]}/")));

                  options.AddPolicy("ReadSchedules", policy => policy.Requirements.Add(new HasScopeRequirement("read:workschedules", $"https://{Configuration["Auth0:Domain"]}/")));
                  options.AddPolicy("EditSchedules", policy => policy.Requirements.Add(new HasScopeRequirement("edit:workschedules", $"https://{Configuration["Auth0:Domain"]}/")));

                  options.AddPolicy("ReadTreeSpecies", policy => policy.Requirements.Add(new HasScopeRequirement("read:treespecies", $"https://{Configuration["Auth0:Domain"]}/")));
                  options.AddPolicy("EditTreeSpecies", policy => policy.Requirements.Add(new HasScopeRequirement("edit:treespecies", $"https://{Configuration["Auth0:Domain"]}/")));

                  options.AddPolicy("AddUser", policy => policy.Requirements.Add(new HasScopeRequirement("add:user", $"https://{Configuration["Auth0:Domain"]}/")));
              }
              );
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            //Swagger
            services.RegisterInfrastructure();

            services.RegisterApplication();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });

            //Repositories
            services.AddTransient<IAddressesRepository, AddressesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmployeeTasksRepository, EmployeeTasksRepository>();
            services.AddTransient<IMaintenancePlansRepository, MaintenancePlansRepository>();
            services.AddTransient<ISitesRepository, SitesRepository>();
            services.AddTransient<ITreeFarmsRepository, TreeFarmsRepository>();
            services.AddTransient<ITreeSpeciesRepository, TreeSpeciesRepository>();
            services.AddTransient<IWorkSchedulesRepository, WorkSchedulesRepository>();
            services.AddTransient<IZonesRepository, ZonesRepository>();
            services.AddTransient<IUnitofWork, UnitofWork>();
            services.AddControllers();
            services.AddAutoMapper(typeof(Mappings));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }
            app.UseMiddleware<ExceptionHandlingMiddleWare>();

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseCors(b =>
                b.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }

}
