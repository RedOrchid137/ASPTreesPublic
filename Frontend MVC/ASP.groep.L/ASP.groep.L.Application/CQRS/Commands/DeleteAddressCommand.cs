using ASP.groep.L.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Commands
{
    public class DeleteAddressCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, int>
        {
            private readonly IUnitofWork uow;
            public DeleteAddressCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<int> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
            {
                var Address = await uow.AddressesRepository.GetById(request.Id);

                if (Address == null)
                {
                    return default;
                }
                else
                {
                    uow.AddressesRepository.Delete(Address);
                    return request.Id;
                }
            }
        }
    }
}
