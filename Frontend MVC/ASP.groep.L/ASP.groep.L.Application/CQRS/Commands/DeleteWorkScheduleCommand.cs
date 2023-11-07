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
    public class DeleteWorkScheduleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteWorkScheduleCommandHandler : IRequestHandler<DeleteWorkScheduleCommand, int>
        {
            private readonly IUnitofWork uow;
            private readonly IMapper _mapper;
            public DeleteWorkScheduleCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
                _mapper = mapper;
            }
            public async Task<int> Handle(DeleteWorkScheduleCommand request, CancellationToken cancellationToken)
            {
                var WorkSchedule = await uow.WorkSchedulesRepository.GetById(request.Id);

                if (WorkSchedule == null)
                {
                    return default;
                }
                else
                {
                    uow.WorkSchedulesRepository.Delete(WorkSchedule);
                    return request.Id;
                }
            }
        }
    }
}
