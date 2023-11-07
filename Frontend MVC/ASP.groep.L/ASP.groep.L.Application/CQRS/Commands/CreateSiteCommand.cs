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
    public class CreateSiteCommand : IRequest<Site>
    {
        public Site Site { get; set; }

        public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand, Site>
        {
            private readonly IUnitofWork uow;
            public CreateSiteCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<Site> Handle(CreateSiteCommand command, CancellationToken cancellationToken)
            {
                uow.SitesRepository.Create(command.Site);
                return command.Site;
            }
        }
    }
}
