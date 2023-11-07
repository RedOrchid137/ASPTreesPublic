using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Queries
{
    public class GetUserTasksTodayQuery : IRequest<IEnumerable<EmployeeTask>>
    {
        public int pageNr { get; set; } = 1;
        public int pageSize { get; set; } = 10;

        public string UserMail { get; set; }
    }

    public class GetUserTasksTodayQueryHandler : IRequestHandler<GetUserTasksTodayQuery, IEnumerable<EmployeeTask>>
    {
        private readonly IUnitofWork _uow;
        private readonly IMapper _mapper;

        public GetUserTasksTodayQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeTask>> Handle(GetUserTasksTodayQuery request, CancellationToken cancellationToken)
        {
            
            var list = await _uow.EmployeeTasksRepository.GetAll(request.pageNr, request.pageSize);
            return list.Where(t => t.scheduledStart.Day == DateTime.Now.Day && t.WorkSchedule.Employee.Email==request.UserMail);
        }
    }
    
}

