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
    public class GetTreeFarmDetailQuery : IRequest<TreeFarmDetailDTO>
    {
        public int Id { get; set; }
    }
    public class GetTreeFarmDetailQueryHandler : IRequestHandler<GetTreeFarmDetailQuery, TreeFarmDetailDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetTreeFarmDetailQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<TreeFarmDetailDTO> Handle(GetTreeFarmDetailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<TreeFarmDetailDTO>(await uow.TreeFarmsRepository.GetById(request.Id));
        }
    }
}
