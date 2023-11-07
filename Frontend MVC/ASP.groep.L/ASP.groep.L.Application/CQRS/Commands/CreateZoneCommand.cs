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
    public class CreateZoneCommand : IRequest<Zone>
    {
        public Zone Zone { get; set; }

        public class CreateZoneCommandHandler : IRequestHandler<CreateZoneCommand, Zone>
        {
            private readonly IUnitofWork uow;
            public CreateZoneCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<Zone> Handle(CreateZoneCommand command, CancellationToken cancellationToken)
            {
                command.Zone.Site = null;
                command.Zone.TreeSpecies = null;
                uow.ZonesRepository.Create(command.Zone);

                return command.Zone;
            }
        }
    }
}
