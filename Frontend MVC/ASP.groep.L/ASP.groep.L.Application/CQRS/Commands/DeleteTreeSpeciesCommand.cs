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
    public class DeleteTreeSpeciesCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteTreeSpeciesCommandHandler : IRequestHandler<DeleteTreeSpeciesCommand, int>
        {
            private readonly IUnitofWork uow;
            private readonly IMapper _mapper;
            public DeleteTreeSpeciesCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
                _mapper = mapper;
            }
            public async Task<int> Handle(DeleteTreeSpeciesCommand command, CancellationToken cancellationToken)
            {
                var TreeSpecies = await uow.TreeSpeciesRepository.GetById(command.Id);

                if (TreeSpecies == null)
                {
                    return default;
                }
                else
                {
                    uow.TreeSpeciesRepository.Delete(TreeSpecies);
                    return command.Id;
                }
            }
        }
    }

}
