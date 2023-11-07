using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Queries
{
    public class GetEmployeeTaskDetailQuery : IRequest<EmployeeTaskDetailDTO>
    {
        public int Id { get; set; }
    }
    public class GetEmployeeTaskDetailQueryHandler : IRequestHandler<GetEmployeeTaskDetailQuery, EmployeeTaskDetailDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetEmployeeTaskDetailQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<EmployeeTaskDetailDTO> Handle(GetEmployeeTaskDetailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<EmployeeTaskDetailDTO>(await uow.EmployeeTasksRepository.GetById(request.Id));
        }
    }
}
