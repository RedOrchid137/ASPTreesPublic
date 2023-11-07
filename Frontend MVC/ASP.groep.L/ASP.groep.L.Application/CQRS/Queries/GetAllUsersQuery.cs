using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Domain;

namespace ASP.groep.L.Application.CQRS.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
        public int pageNr { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IUnitofWork _uow;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await _uow.UserRepository.GetAll(request.pageNr, request.pageSize);
            return list;
        }
    }
}
