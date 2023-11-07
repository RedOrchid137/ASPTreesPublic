using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.Interfaces
{
    public interface IUnitofWork
    {
        public IUserRepository UserRepository { get;  }
        public IWorkSchedulesRepository WorkSchedulesRepository { get;  }
        public IAddressesRepository AddressesRepository { get;  }
        public IEmployeeTasksRepository EmployeeTasksRepository { get;  }
        public IMaintenancePlansRepository MaintenancePlansRepository { get;  }
        public ITreeSpeciesRepository TreeSpeciesRepository { get;  }
        public ITreeFarmsRepository TreeFarmsRepository { get;  }
        public ISitesRepository SitesRepository { get;  }
        public IZonesRepository ZonesRepository { get;  }
        Task Commit();
    }
}
