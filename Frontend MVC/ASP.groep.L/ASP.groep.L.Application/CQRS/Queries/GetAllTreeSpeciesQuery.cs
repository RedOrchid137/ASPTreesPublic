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
    public class GetAllTreeSpeciesQuery : IRequest<IEnumerable<TreeSpecies>>
    {
        public int pageNr { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
    public class GetAllTreeSpeciesQueryHandler : IRequestHandler<GetAllTreeSpeciesQuery, IEnumerable<TreeSpecies>>
    {
        private readonly IUnitofWork _uow;
        private readonly IMapper _mapper;

        public GetAllTreeSpeciesQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TreeSpecies>> Handle(GetAllTreeSpeciesQuery request, CancellationToken cancellationToken)
        {
            var list = await _uow.TreeSpeciesRepository.GetAll(request.pageNr, request.pageSize);
            return list;
        }
    }
}
