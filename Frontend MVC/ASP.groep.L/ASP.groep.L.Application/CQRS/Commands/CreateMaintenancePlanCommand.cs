using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Commands
{
    public class CreateMaintenancePlanCommand : IRequest<MaintenancePlan>
    {
        public MaintenancePlan MaintenancePlan { get; set; }

        public class CreateMaintenancePlanCommandHandler : IRequestHandler<CreateMaintenancePlanCommand, MaintenancePlan>
        {
            private readonly IUnitofWork _uow;
            public CreateMaintenancePlanCommandHandler(IUnitofWork uow)
            {
                _uow = uow;
            }
            public async Task<MaintenancePlan> Handle(CreateMaintenancePlanCommand command, CancellationToken cancellationToken)
            {            
                _uow.MaintenancePlansRepository.Create(command.MaintenancePlan);
                return command.MaintenancePlan;
            }
        }
    }
}
