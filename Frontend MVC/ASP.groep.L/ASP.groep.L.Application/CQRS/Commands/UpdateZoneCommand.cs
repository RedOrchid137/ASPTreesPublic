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
    public class UpdateZoneCommand : IRequest<Zone>
    {
        public Zone Zone { get; set; }
        public class UpdateZoneCommandHandler : IRequestHandler<UpdateZoneCommand, Zone>
        {
            private readonly IUnitofWork uow;
            public UpdateZoneCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<Zone> Handle(UpdateZoneCommand command, CancellationToken cancellationToken)
            {

                if (command.Zone == null)
                {
                    return default;
                }
                else
                {
                    command.Zone.Site = null;
                    command.Zone.TreeSpecies = null;
                    uow.ZonesRepository.Update(command.Zone);
                    return command.Zone;
                }
            }
        }
    }

}
