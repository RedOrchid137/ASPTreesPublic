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
    public class GetTreeSpeciesDetailQuery : IRequest<TreeSpeciesDetailDTO>
    {
        public int Id { get; set; }
    }
    public class GetTreeSpeciesDetailQueryHandler : IRequestHandler<GetTreeSpeciesDetailQuery, TreeSpeciesDetailDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetTreeSpeciesDetailQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<TreeSpeciesDetailDTO> Handle(GetTreeSpeciesDetailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<TreeSpeciesDetailDTO>(await uow.TreeSpeciesRepository.GetById(request.Id));
        }
    }
}
