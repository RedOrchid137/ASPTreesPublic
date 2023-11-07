using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Commands
{
    public class UpdateUserCommand : IRequest<User>
    {
        public User User { get; set; }
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
        {
            private readonly IUnitofWork uow;
            public UpdateUserCommandHandler(IUnitofWork uow, IMapper mapper)
            {
                this.uow = uow;
            }
            public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
            {

                if (command.User == null)
                {
                    return default;
                }
                else
                {
                    uow.UserRepository.Update(command.User);
                    return command.User;
                }
            }
        }
    }
}
