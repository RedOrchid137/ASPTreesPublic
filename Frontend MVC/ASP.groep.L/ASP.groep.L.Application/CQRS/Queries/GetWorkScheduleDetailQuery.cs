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
    public class GetWorkScheduleDetailQuery : IRequest<WorkScheduleDetailDTO>
    {
        public int Id { get; set; }
    }
    public class GetWorkScheduleDetailQueryHandler : IRequestHandler<GetWorkScheduleDetailQuery, WorkScheduleDetailDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetWorkScheduleDetailQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<WorkScheduleDetailDTO> Handle(GetWorkScheduleDetailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<WorkScheduleDetailDTO>(await uow.WorkSchedulesRepository.GetById(request.Id));
        }
    }
}
