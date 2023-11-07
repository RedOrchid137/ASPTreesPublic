using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.Interfaces;
using AutoFixture;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ASP.groep.L.Domain;
using ASP.groep.L.UnitTesting.Mocks;
using Shouldly;
using System.Threading;
using ASP.groep.L.Application.CQRS.Mappings;
using ASP.groep.L.UnitTesting.Mocks.Users;

namespace ASP.groep.L.UnitTesting.CQRS.HappyFlows
{

    [TestClass]
    public class UserCQRSTestHappyFlow
    {
        private readonly Mock<IMediator> _mediator;

        public UserCQRSTestHappyFlow()
        {
            _mediator = MockUserMediator.GetMediator();
        }

        [TestMethod]
        public async Task GetAllUsersTestHappyFlow()
        {
            var request = new GetAllUsersQuery();
            var result = await _mediator.Object.Send(request,CancellationToken.None);
            result.ShouldBeOfType<List<User>>();
            result.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public async Task GetUserByIdTestHappyFlow()
        {
            var request = new GetUserByIdQuery();
            request.Id = 1;
            var result = await _mediator.Object.Send(request, CancellationToken.None);
            result.ShouldBeOfType<User>();
            result.Fullname.ShouldBe("Bart Bartmans");
            result.Role.ShouldBe(Role.Ionic_Employee);
        }

        [TestMethod]
        public async Task GetUserByEmailTestHappyFlow()
        {
            var request = new GetUserByEmailQuery();
            request.Email = "employee@trees.com";
            var result = await _mediator.Object.Send(request, CancellationToken.None);
            result.ShouldBeOfType<User>();
            result.Fullname.ShouldBe("Bart Bartmans");
            result.Role.ShouldBe(Role.Ionic_Employee);
        }
        [TestMethod]
        public async Task CreateUserTestHappyFlow()
        {
            var command = new CreateUserCommand();
            var user = new User()
            {
                Id = 3,
                FirstName = "Thijs",
                LastName = "Thijsmans",
                Email = "thijs.thijsmans@trees.com",
                Role = Role.MVC_Admin
            };
            command.User = user;
            var result = await _mediator.Object.Send(command, CancellationToken.None);
            result.ShouldBeOfType<User>();
            result.Fullname.ShouldBe("Thijs Thijsmans");
            result.Role.ShouldBe(Role.MVC_Admin);
        }
        [TestMethod]
        public async Task UpdateUserTestHappyFlow()
        {
            var command = new UpdateUserCommand();
            var user = new User()
            {
                Id = 2,
                FirstName = "Kurt",
                LastName = "Kurtmans",
                Email = "kurt@trees.com",
                Role = Role.MVC_Admin
            };
            command.User = user;
            await _mediator.Object.Send(command, CancellationToken.None);
            var query = new GetUserByIdQuery();
            query.Id = 2;
            var res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeOfType<User>();
            res.Email.ShouldBe("kurt@trees.com");
            res.Role.ShouldBe(Role.MVC_Admin);
        }
        [TestMethod]
        public async Task DeleteUserTestHappyFlow()
        {
            var query = new GetUserByIdQuery();
            query.Id = 2;
            var res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeOfType<User>();
            var command = new DeleteUserCommand();
            command.Id = 2;
            var result = await _mediator.Object.Send(command, CancellationToken.None);
            res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeNull();
        }
    }
}
