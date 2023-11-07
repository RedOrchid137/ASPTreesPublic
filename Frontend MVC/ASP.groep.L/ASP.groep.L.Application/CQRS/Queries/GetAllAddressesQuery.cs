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
    public class GetAllAddressesQuery : IRequest<IEnumerable<Address>>
    {
        public int pageNr { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }

    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, IEnumerable<Address>>
    {
        private readonly IUnitofWork _uow;
        private readonly IMapper _mapper;

        public GetAllAddressesQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Address>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            var list = await _uow.AddressesRepository.GetAll(request.pageNr, request.pageSize);
            return list;
        }
    }
}
