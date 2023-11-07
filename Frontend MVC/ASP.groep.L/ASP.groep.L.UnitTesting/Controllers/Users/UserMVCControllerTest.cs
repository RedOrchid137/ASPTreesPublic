using ASP.groep.L.Controllers;
using ASP.groep.L.Domain;
using ASP.groep.L.UnitTesting.Mocks.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.Controllers.Users
{
    [TestClass]
    public class UserMVCControllerTest
    {
        private UserController controller;
        private readonly Mock<IMediator> _mediator;
        public UserMVCControllerTest()
        {
            _mediator = MockUserMediator.GetMediator();
            controller = new UserController(_mediator.Object);

        }

        [TestMethod]
        public async Task Index()
        {
            var res = await controller.Index(Role.Ionic_Employee);
            res.ShouldBeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create()
        {
            var res = controller.Create(Role.Ionic_Employee);
            res.ShouldBeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public async Task Update()
        {
            var res = await controller.Update(2);
            res.ShouldBeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public async Task Delete()
        {
            var res = await controller.Delete(2);
            res.ShouldBeOfType(typeof(ViewResult));
        }

    }
}
