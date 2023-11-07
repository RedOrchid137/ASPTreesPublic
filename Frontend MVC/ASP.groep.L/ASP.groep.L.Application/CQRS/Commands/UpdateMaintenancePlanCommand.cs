using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Commands
{
    public class UpdateMaintenancePlanCommand : IRequest<MaintenancePlan>
    {
        public MaintenancePlan MaintenancePlan { get; set; }
        public class UpdateMaintenancePlanCommandHandler : IRequestHandler<UpdateMaintenancePlanCommand, MaintenancePlan>
        {
            private readonly IUnitofWork uow;
            public UpdateMaintenancePlanCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<MaintenancePlan> Handle(UpdateMaintenancePlanCommand command, CancellationToken cancellationToken)
            {
                if (command.MaintenancePlan == null || command.MaintenancePlan.Id==null)
                {
                    return default;
                }
                else
                {
                    uow.MaintenancePlansRepository.Update(command.MaintenancePlan);
                    return command.MaintenancePlan;
                }
            }
        }
    }
}
