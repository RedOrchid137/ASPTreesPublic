using ASP.groep.L.Application.CQRS.Queries;
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
    public class UpdateEmployeeTaskCommand : IRequest<EmployeeTask>
    {
        public EmployeeTask EmployeeTask { get; set; }
        public EmployeeTask PreviousVersion { get; set; }
        public class UpdateEmployeeTaskCommandHandler : IRequestHandler<UpdateEmployeeTaskCommand, EmployeeTask>
        {
            private readonly IUnitofWork uow;
            public UpdateEmployeeTaskCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<EmployeeTask> Handle(UpdateEmployeeTaskCommand command, CancellationToken cancellationToken)
            {

                if (command.EmployeeTask == null)
                {
                    return default;
                }
                else
                {                   
                    uow.EmployeeTasksRepository.Update(command.EmployeeTask);
                    return command.EmployeeTask;
                }
            }
        }
    }
}
