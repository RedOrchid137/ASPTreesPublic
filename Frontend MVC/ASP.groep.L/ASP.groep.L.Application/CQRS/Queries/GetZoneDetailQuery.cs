using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Queries
{
    public class GetZoneDetailQuery : IRequest<ZoneDetailDTO>
    {
        public int Id { get; set; }
    }
    public class GetZoneDetailQueryHandler : IRequestHandler<GetZoneDetailQuery, ZoneDetailDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetZoneDetailQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<ZoneDetailDTO> Handle(GetZoneDetailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ZoneDetailDTO>(await uow.ZonesRepository.GetById(request.Id));
        }
    }
}
