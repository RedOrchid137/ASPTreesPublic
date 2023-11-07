using ASP.groep.L.Application.CQRS.Mappings;
using ASP.groep.L.UnitTesting.Mocks.Sites;
using ASP.groep.L.WebAPI.Controllers;
using AutoMapper;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.Controllers.Sites
{
    public class SiteAPIControllerTest
    {
        private SiteController controller;
        private readonly Mock<IMediator> _mediator;
        private readonly IMapper _mapper;

        public SiteAPIControllerTest()
        {
            _mediator = MockSiteMediator.GetMediator();
            var config = new MapperConfiguration(e => e.AddProfile<Mappings>());
            _mapper = config.CreateMapper();
            controller = new SiteController(_mediator.Object, _mapper);
        }
    }
}
