using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Queries
{
    public class GetMaintenancePlanByIdQuery : IRequest<MaintenancePlan>
    {
        public int Id { get; set; }
    }

    public class GetMaintenancePlanByIdQueryHandler : IRequestHandler<GetMaintenancePlanByIdQuery, MaintenancePlan>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetMaintenancePlanByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<MaintenancePlan> Handle(GetMaintenancePlanByIdQuery request, CancellationToken cancellationToken)
        {
            return await uow.MaintenancePlansRepository.GetById(request.Id);
        }
    }
}
