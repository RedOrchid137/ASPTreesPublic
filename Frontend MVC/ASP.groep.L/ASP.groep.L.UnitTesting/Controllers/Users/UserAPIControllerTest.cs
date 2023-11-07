using ASP.groep.L.Application.CQRS.Mappings;
using ASP.groep.L.UnitTesting.Mocks.Users;
using ASP.groep.L.WebAPI.Controllers;
using AutoMapper;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.Controllers.Users
{

    public class UserAPIControllerTest
    {
        private UserController controller;
        private readonly Mock<IMediator> _mediator;
        private readonly IMapper _mapper;

        public UserAPIControllerTest()
        {
            _mediator = MockUserMediator.GetMediator();
            var config = new MapperConfiguration(e=>e.AddProfile<Mappings>());
            _mapper = config.CreateMapper();
            controller = new UserController(_mediator.Object,_mapper);
        }
    }

}
