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
    public class GetUserDetailQuery : IRequest<UserDetailDTO>
    {
        public int Id { get; set; }
    }
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailDTO>
    {
        private readonly IUnitofWork uow;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            this.uow = uow;
            _mapper = mapper;
        }
        public async Task<UserDetailDTO> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDetailDTO>(await uow.UserRepository.GetById(request.Id));
        }
    }
}
