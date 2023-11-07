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
    public class DeleteZoneCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteZoneCommandHandler : IRequestHandler<DeleteZoneCommand, int>
        {
            private readonly IUnitofWork uow;
            private readonly IMapper _mapper;
            public DeleteZoneCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
                _mapper = mapper;
            }
            public async Task<int> Handle(DeleteZoneCommand command, CancellationToken cancellationToken)
            {
                var Zone = await uow.ZonesRepository.GetById(command.Id);

                if (Zone == null)
                {
                    return default;
                }
                else
                {
                    uow.ZonesRepository.Delete(Zone);
                    return command.Id;
                }
            }
        }
    }

}
