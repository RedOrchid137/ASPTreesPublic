using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Commands
{
    public class UpdateWorkScheduleCommand : IRequest<WorkSchedule>
    {
        public WorkSchedule WorkSchedule { get; set; }
        public class UpdateWorkScheduleCommandHandler : IRequestHandler<UpdateWorkScheduleCommand, WorkSchedule>
        {
            private readonly IUnitofWork uow;
            public UpdateWorkScheduleCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<WorkSchedule> Handle(UpdateWorkScheduleCommand command, CancellationToken cancellationToken)
            {

                if (command.WorkSchedule == null)
                {
                    return default;
                }
                else
                {
                    uow.WorkSchedulesRepository.Update(command.WorkSchedule);
                    return command.WorkSchedule;
                }
            }
        }
    }
}
