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
    public class CreateWorkScheduleCommand : IRequest<WorkSchedule>
    {
        public WorkSchedule WorkSchedule { get; set; }

        public class CreateWorkScheduleCommandHandler : IRequestHandler<CreateWorkScheduleCommand, WorkSchedule>
        {
            private readonly IUnitofWork uow;
            public CreateWorkScheduleCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<WorkSchedule> Handle(CreateWorkScheduleCommand command, CancellationToken cancellationToken)
            {
                uow.WorkSchedulesRepository.Create(command.WorkSchedule);
                return command.WorkSchedule;
            }
        }
    }
}
