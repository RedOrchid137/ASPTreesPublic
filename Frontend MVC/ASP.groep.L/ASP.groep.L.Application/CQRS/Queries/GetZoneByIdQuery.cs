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
    public class GetZoneByIdQuery : IRequest<Zone>
    {
        public int Id { get; set; }
    }
    public class GetZoneByIdQueryHandler : IRequestHandler<GetZoneByIdQuery, Zone>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetZoneByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<Zone> Handle(GetZoneByIdQuery request, CancellationToken cancellationToken)
        {
            return await uow.ZonesRepository.GetById(request.Id);
        }
    }
}
