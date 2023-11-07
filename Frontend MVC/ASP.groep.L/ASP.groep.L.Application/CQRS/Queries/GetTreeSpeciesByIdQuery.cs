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
    public class GetTreeSpeciesByIdQuery : IRequest<TreeSpecies>
    {
        public int Id { get; set; }
    }
    public class GetTreeSpecieByIdQueryHandler : IRequestHandler<GetTreeSpeciesByIdQuery, TreeSpecies>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetTreeSpecieByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<TreeSpecies> Handle(GetTreeSpeciesByIdQuery request, CancellationToken cancellationToken)
        {
            return await uow.TreeSpeciesRepository.GetById(request.Id);
        }
    }
}
