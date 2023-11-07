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
    public class GetTreeFarmByIdQuery : IRequest<TreeFarm>
    {
        public int Id { get; set; }
    }
    public class GetTreeFarmByIdQueryHandler : IRequestHandler<GetTreeFarmByIdQuery, TreeFarm>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetTreeFarmByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<TreeFarm> Handle(GetTreeFarmByIdQuery request, CancellationToken cancellationToken)
        {
            return await uow.TreeFarmsRepository.GetById(request.Id);
        }
    }
}
