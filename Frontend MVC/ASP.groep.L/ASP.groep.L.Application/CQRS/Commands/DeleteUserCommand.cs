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
    public class DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
        {
            private readonly IUnitofWork uow;
            private readonly IMapper _mapper;
            public DeleteUserCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
                _mapper = mapper;
            }
            public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var Employee = await uow.UserRepository.GetById(request.Id);

                if (Employee == null)
                {
                    return default;
                }
                else
                {
                    uow.UserRepository.Delete(Employee);
                    return request.Id;
                }
            }
        }
    }
}
