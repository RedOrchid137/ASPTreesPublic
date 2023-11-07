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
    public class DeleteSiteCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteSiteCommandHandler : IRequestHandler<DeleteSiteCommand, int>
        {
            private readonly IUnitofWork _uow;
            private readonly IMapper _mapper;
            public DeleteSiteCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }
            public async Task<int> Handle(DeleteSiteCommand command, CancellationToken cancellationToken)
            {
                var Site = await _uow.SitesRepository.GetById(command.Id);

                if (Site == null)
                {
                    return default;
                }
                else
                {
                    _uow.SitesRepository.Delete(Site);
                    return command.Id;
                }
            }
        }
    }

}
