using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.groep.L.Domain;
namespace ASP.groep.L.Application.CQRS.Queries
{
    public class GetAllSitesQuery : IRequest<IEnumerable<Site>>
    {
        public int pageNr { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
    public class GetAllSitesQueryHandler : IRequestHandler<GetAllSitesQuery, IEnumerable<Site>>
    {
        private readonly IUnitofWork _uow;
        private readonly IMapper _mapper;

        public GetAllSitesQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Site>> Handle(GetAllSitesQuery request, CancellationToken cancellationToken)
        {
            var list = await _uow.SitesRepository.GetAll(request.pageNr, request.pageSize);
            return list;
        }
    }
}
