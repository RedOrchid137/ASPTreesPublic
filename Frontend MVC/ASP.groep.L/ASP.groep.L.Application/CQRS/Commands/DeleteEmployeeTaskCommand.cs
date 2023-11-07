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
    public class DeleteEmployeeTaskCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteEmployeeTaskCommandHandler : IRequestHandler<DeleteEmployeeTaskCommand, int>
        {
            private readonly IUnitofWork uow;
            private readonly IMapper _mapper;
            public DeleteEmployeeTaskCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
                _mapper = mapper;
            }
            public async Task<int> Handle(DeleteEmployeeTaskCommand request, CancellationToken cancellationToken)
            {
                var EmployeeTask = await uow.EmployeeTasksRepository.GetById(request.Id);

                if (EmployeeTask == null)
                {
                    return default;
                }
                else
                {
                    uow.EmployeeTasksRepository.Delete(EmployeeTask);
                    return request.Id;
                }
            }
        }
    }
}
