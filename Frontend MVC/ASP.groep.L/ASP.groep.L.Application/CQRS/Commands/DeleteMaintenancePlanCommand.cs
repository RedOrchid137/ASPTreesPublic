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
    public class DeleteMaintenancePlanCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteMaintenancePlanCommandHandler : IRequestHandler<DeleteMaintenancePlanCommand, int>
        {
            private readonly IUnitofWork uow;
            public DeleteMaintenancePlanCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<int> Handle(DeleteMaintenancePlanCommand request, CancellationToken cancellationToken)
            {
                var MaintenancePlan = await uow.MaintenancePlansRepository.GetById(request.Id);

                if (MaintenancePlan == null)
                {
                    return default;
                }
                else
                {
                    uow.MaintenancePlansRepository.Delete(MaintenancePlan);
                    return request.Id;
                }
            }
        }
    }
}
