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
    public class UpdateSiteCommand : IRequest<Site>
    {
        public Site Site { get; set; }
        public class UpdateSiteCommandHandler : IRequestHandler<UpdateSiteCommand, Site>
        {
            private readonly IUnitofWork uow;
            public UpdateSiteCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<Site> Handle(UpdateSiteCommand command, CancellationToken cancellationToken)
            {

                if (command.Site == null)
                {
                    return default;
                }
                else
                {
                    uow.SitesRepository.Update(command.Site);
                    return command.Site;
                }
            }
        }
    }

}
