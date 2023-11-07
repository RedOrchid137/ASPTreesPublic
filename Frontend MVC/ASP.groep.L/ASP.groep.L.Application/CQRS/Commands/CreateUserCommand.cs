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
    public class CreateUserCommand : IRequest<User>
    {
        public User User { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
        {
            private readonly IUnitofWork uow;
            public CreateUserCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
                uow.UserRepository.Create(command.User);
                return command.User;
            }
        }
    }
}
