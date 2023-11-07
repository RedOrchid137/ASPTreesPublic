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
    public class GetSiteDetailQuery : IRequest<SiteDetailDTO>
    {
        public int Id { get; set; }
    }
    public class GetSiteDetailQueryHandler : IRequestHandler<GetSiteDetailQuery, SiteDetailDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetSiteDetailQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<SiteDetailDTO> Handle(GetSiteDetailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<SiteDetailDTO>(await uow.SitesRepository.GetById(request.Id));
        }
    }
}
