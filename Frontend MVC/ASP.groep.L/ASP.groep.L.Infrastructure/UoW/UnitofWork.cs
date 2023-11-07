using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Infrastructure.UoW
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext ctxt;
        private readonly IUserRepository userRepo;
        private readonly IWorkSchedulesRepository workSchedulesRepo;
        private readonly IAddressesRepository addressesRepo;
        private readonly IEmployeeTasksRepository employeeTasksRepo;
        private readonly IMaintenancePlansRepository maintenancePlansRepo;
        private readonly ITreeSpeciesRepository TreeSpeciesRepo;
        private readonly ITreeFarmsRepository treeFarmsRepo;
        private readonly ISitesRepository sitesRepo;
        private readonly IZonesRepository zonesRepo;

        public UnitofWork(
            ApplicationDbContext ctxt,
            IWorkSchedulesRepository workSchedulesRepo,
            IAddressesRepository addressesRepo,
            IEmployeeTasksRepository employeeTasksRepo,
            IMaintenancePlansRepository maintenancePlansRepo,
            ITreeSpeciesRepository TreeSpeciesRepo,
            ITreeFarmsRepository treeFarmsRepo,
            ISitesRepository sitesRepo,
            IZonesRepository zonesRepo,
            IUserRepository userRepo
            )
        {
            this.ctxt = ctxt;
            this.workSchedulesRepo = workSchedulesRepo;
            this.addressesRepo = addressesRepo;
            this.employeeTasksRepo = employeeTasksRepo;
            this.maintenancePlansRepo = maintenancePlansRepo;
            this.TreeSpeciesRepo = TreeSpeciesRepo;
            this.treeFarmsRepo = treeFarmsRepo;
            this.sitesRepo = sitesRepo;
            this.zonesRepo = zonesRepo;
            this.userRepo = userRepo;
    }
        public IWorkSchedulesRepository WorkSchedulesRepository => workSchedulesRepo;
        public IAddressesRepository AddressesRepository => addressesRepo;
        public IEmployeeTasksRepository EmployeeTasksRepository => employeeTasksRepo;
        public IMaintenancePlansRepository MaintenancePlansRepository => maintenancePlansRepo;
        public ITreeSpeciesRepository TreeSpeciesRepository => TreeSpeciesRepo;
        public ITreeFarmsRepository TreeFarmsRepository => treeFarmsRepo;
        public ISitesRepository SitesRepository => sitesRepo;
        public IZonesRepository ZonesRepository => zonesRepo;
        public IUserRepository UserRepository => userRepo;
        public async Task Commit()
        {
            await ctxt.SaveChangesAsync();
        }
    }
}
