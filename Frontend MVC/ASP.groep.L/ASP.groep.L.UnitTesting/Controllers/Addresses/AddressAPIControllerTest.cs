using ASP.groep.L.Application.CQRS.Mappings;
using ASP.groep.L.UnitTesting.Mocks.Addresses;
using ASP.groep.L.WebAPI.Controllers;
using AutoMapper;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.Controllers.Addresses
{
    public class AddressAPIControllerTest
    {
        private AddressController controller;
        private readonly Mock<IMediator> _mediator;
        private readonly IMapper _mapper;

        public AddressAPIControllerTest()
        {
            _mediator = MockAddressMediator.GetMediator();
            var config = new MapperConfiguration(e => e.AddProfile<Mappings>());
            _mapper = config.CreateMapper();
            controller = new AddressController(_mediator.Object, _mapper);
        }
    }
}
