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
    public class GetAddressDetailQuery : IRequest<AddressDetailDTO>
    {
        public int Id { get; set; }
    }
    public class GetAddressDetailQueryHandler : IRequestHandler<GetAddressDetailQuery, AddressDetailDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetAddressDetailQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<AddressDetailDTO> Handle(GetAddressDetailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<AddressDetailDTO>(await uow.AddressesRepository.GetById(request.Id));
        }
    }
}
