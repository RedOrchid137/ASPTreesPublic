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
    public class GetAddressByIdQuery : IRequest<Address>
    {
        public int Id { get; set; }
    }
    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, Address>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetAddressByIdQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<Address> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            return await uow.AddressesRepository.GetById(request.Id);
        }
    }
}
