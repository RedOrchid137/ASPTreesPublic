using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.groep.L.Domain;
namespace ASP.groep.L.Application.CQRS.Queries
{
    public class GetAllEmployeeTasksByScheduleQuery : IRequest<IEnumerable<EmployeeTask>>
    {
        public WorkSchedule schedule { get; set; }
    }

    public class GetAllEmployeeTasksByScheduleQueryHandler : IRequestHandler<GetAllEmployeeTasksByScheduleQuery, IEnumerable<EmployeeTask>>
    {
        private readonly IUnitofWork _uow;
        private readonly IMapper _mapper;

        public GetAllEmployeeTasksByScheduleQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeTask>> Handle(GetAllEmployeeTasksByScheduleQuery request, CancellationToken cancellationToken)
        {
            var list = await _uow.EmployeeTasksRepository.GetAllBySchedule(request.schedule);
            return list;
        }
    }
}
