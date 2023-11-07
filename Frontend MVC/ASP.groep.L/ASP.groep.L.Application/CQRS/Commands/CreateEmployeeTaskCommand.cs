using ASP.groep.L.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.groep.L.Application.Interfaces;
using AutoMapper;

namespace ASP.groep.L.Application.CQRS.Commands
{
    public class CreateEmployeeTaskCommand : IRequest<EmployeeTask>
    {
        public EmployeeTask EmployeeTask { get; set; }

        public class CreateEmployeeTaskCommandHandler : IRequestHandler<CreateEmployeeTaskCommand, EmployeeTask>
        {
            private readonly IUnitofWork _uow;
            public CreateEmployeeTaskCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this._uow = uow;
            }
            public async Task<EmployeeTask> Handle(CreateEmployeeTaskCommand command, CancellationToken cancellationToken)
            {
                _uow.EmployeeTasksRepository.Create(command.EmployeeTask);
                return command.EmployeeTask;
            }
        }
    }
}
