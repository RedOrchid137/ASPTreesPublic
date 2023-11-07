using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Commands
{
    public class CreateAddressCommand : IRequest<Address>
    {
        public Address Address { get; set; }

        public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Address>
        {
            private readonly IUnitofWork uow;
            public CreateAddressCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<Address> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
            {
                uow.AddressesRepository.Create(command.Address);
                return command.Address;
            }
        }
    }
}
