using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Mappings
{
    public class Mappings:Profile
    {
        public Mappings()
        {
            //Mappings
            CreateMap<Site, SiteDTO>();
            CreateMap<SiteDTO, Site>();
            CreateMap<SiteDetailDTO, Site>();
            CreateMap<Site, SiteDetailDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDetailDTO>();
            CreateMap<UserDetailDTO, User>();
            CreateMap<WorkSchedule, WorkScheduleDTO>();
            CreateMap<WorkScheduleDTO,WorkSchedule>();
            CreateMap<WorkSchedule, WorkScheduleDetailDTO>();
            CreateMap<WorkScheduleDetailDTO,WorkSchedule>();
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
            CreateMap<Address, AddressDetailDTO>();
            CreateMap<AddressDetailDTO,Address>();
            CreateMap<EmployeeTask, EmployeeTaskDTO>();
            CreateMap<EmployeeTaskDTO,EmployeeTask> ();
            CreateMap<EmployeeTask, EmployeeTaskDetailDTO>();
            CreateMap<EmployeeTaskDetailDTO,EmployeeTask>();
            CreateMap<MaintenancePlan, MaintenancePlanDTO>();
            CreateMap<MaintenancePlan, MaintenancePlanDetailDTO>();
            CreateMap<MaintenancePlanDTO,MaintenancePlan> ();
            CreateMap<MaintenancePlanDetailDTO,MaintenancePlan>();
            CreateMap<TreeFarm, TreeFarmDTO>();
            CreateMap<TreeFarm, TreeFarmDetailDTO>();
            CreateMap<TreeFarmDTO, TreeFarm>();
            CreateMap<TreeFarmDetailDTO,TreeFarm>();
            CreateMap<TreeSpecies,TreeSpeciesDTO> ();
            CreateMap<TreeSpecies, TreeSpeciesDetailDTO>();
            CreateMap<TreeSpeciesDTO, TreeSpecies>();
            CreateMap<TreeSpeciesDetailDTO, TreeSpecies>();
            CreateMap<Site, SiteDTO>();
            CreateMap<Site, SiteDetailDTO>();
            CreateMap<SiteDTO, Site>();
            CreateMap<SiteDetailDTO, Site>();
            CreateMap<Zone, ZoneDTO>();
            CreateMap<Zone, ZoneDetailDTO>();
            CreateMap<ZoneDTO, Zone>();
            CreateMap<ZoneDetailDTO, Zone>();
        }
    }
}
