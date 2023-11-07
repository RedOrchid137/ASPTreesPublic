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
    public class CreateTreeSpeciesCommand : IRequest<TreeSpecies>
    {
        public TreeSpecies TreeSpecies { get; set; }

        public class CreateTreeSpeciesCommandHandler : IRequestHandler<CreateTreeSpeciesCommand, TreeSpecies>
        {
            private readonly IUnitofWork uow;
            public CreateTreeSpeciesCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<TreeSpecies> Handle(CreateTreeSpeciesCommand command, CancellationToken cancellationToken)
            {
                uow.TreeSpeciesRepository.Create(command.TreeSpecies);
                return command.TreeSpecies;
            }
        }
    }
}
