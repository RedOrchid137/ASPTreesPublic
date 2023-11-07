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

namespace ASP.groep.L.Application.CQRS.Commands
{
    public class UpdateTreeSpeciesCommand : IRequest<TreeSpecies>
    {
        public TreeSpecies TreeSpecies { get; set; }
        public class UpdateTreeSpeciesCommandHandler : IRequestHandler<UpdateTreeSpeciesCommand, TreeSpecies>
        {
            private readonly IUnitofWork uow;
            public UpdateTreeSpeciesCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<TreeSpecies> Handle(UpdateTreeSpeciesCommand command, CancellationToken cancellationToken)
            {
                if (command.TreeSpecies == null)
                {
                    return default;
                }
                else
                {
                    uow.TreeSpeciesRepository.Update(command.TreeSpecies);
                    return command.TreeSpecies;
                }
            }
        }
    }
}
