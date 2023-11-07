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
    public class GetSiteByIdQuery : IRequest<Site>
    {
        public int Id { get; set; }
    }
    public class GetSiteByIdQueryHandler : IRequestHandler<GetSiteByIdQuery, Site>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetSiteByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<Site> Handle(GetSiteByIdQuery request, CancellationToken cancellationToken)
        {
            return await uow.SitesRepository.GetById(request.Id);
        }
    }
}
