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
    public class UpdateAddressCommand : IRequest<Address>
    {
        public Address Address { get; set; }
        public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Address>
        {
            private readonly IUnitofWork uow;
            public UpdateAddressCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<Address> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
            {

                if (command.Address == null)
                {
                    return default;
                }
                else
                {
                    uow.AddressesRepository.Update(command.Address);
                    return command.Address;
                }
            }
        }
    }
}
