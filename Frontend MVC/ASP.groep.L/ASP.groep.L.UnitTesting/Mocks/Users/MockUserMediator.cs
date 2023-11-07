using MediatR;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.Interfaces;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ASP.groep.L.Domain;
using System.Threading;
using ASP.groep.L.Application.CQRS.Mappings;

namespace ASP.groep.L.UnitTesting.Mocks.Users
{
    public static class MockUserMediator
    {

        public static Mock<IMediator> GetMediator()
        {
            Mock<IUserRepository>_userRepo = MockUserRepository.GetUserRepo();
            Mock<IUnitofWork> _uow = new();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappings>();
            });
            IMapper _mapper = mapperConfig.CreateMapper();


            _uow.Setup(_uow => _uow.UserRepository).Returns(_userRepo.Object);
            var _mediator = new Mock<IMediator>();

            //GetAll
            _mediator.Setup(x => x.Send(It.IsAny<GetAllUsersQuery>(), CancellationToken.None))
                .Returns((GetAllUsersQuery query, CancellationToken token) =>
                {
                    var handler = new GetAllUsersQueryHandler(_uow.Object, _mapper);
                    Task<IEnumerable<User>> result = handler.Handle(query, token);
                    return result;
                });

            //GetbyId
            _mediator.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), CancellationToken.None))
                .Returns((GetUserByIdQuery query, CancellationToken token) =>
                {
                    var handler = new GetUserByIdQueryHandler(_uow.Object, _mapper);
                    Task<User> result = handler.Handle(query, token);
                    return result;
                });

            //GetbyEmail
            _mediator.Setup(x => x.Send(It.IsAny<GetUserByEmailQuery>(), CancellationToken.None))
                .Returns((GetUserByEmailQuery query, CancellationToken token) =>
                {
                    var handler = new GetUserByEmailQueryHandler(_uow.Object, _mapper);
                    Task<User> result = handler.Handle(query, token);
                    return result;
                });

            //Create
            _mediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), CancellationToken.None))
                .Returns((CreateUserCommand command, CancellationToken token) =>
                {
                    var handler = new CreateUserCommand.CreateUserCommandHandler(_uow.Object, _mapper);
                    Task<User> result = handler.Handle(command, token);
                    return result;
                });

            //Update
            _mediator.Setup(x => x.Send(It.IsAny<UpdateUserCommand>(), CancellationToken.None))
                .Returns((UpdateUserCommand command, CancellationToken token) =>
                {
                    var handler = new UpdateUserCommand.UpdateUserCommandHandler(_uow.Object, _mapper);
                    Task<User> result = handler.Handle(command, token);
                    return result;
                });

            //Delete
            _mediator.Setup(x => x.Send(It.IsAny<DeleteUserCommand>(), CancellationToken.None))
                .Returns((DeleteUserCommand command, CancellationToken token) =>
                {
                    var handler = new DeleteUserCommand.DeleteUserCommandHandler(_uow.Object, _mapper);
                    Task<int> result = handler.Handle(command, token);
                    return result;
                });

            return _mediator;
        }
    }
}
