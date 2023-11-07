using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Domain;
using ASP.groep.L.UnitTesting.Mocks.Addresses;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.CQRS.HappyFlows
{
    [TestClass]
    public class AddressCQRSTestHappyFlow
    {
        private readonly Mock<IMediator> _mediator;

        public AddressCQRSTestHappyFlow()
        {
            _mediator = MockAddressMediator.GetMediator();
        }

        [TestMethod]
        public async Task GetAllAddressesTestHappyFlow()
        {
            var request = new GetAllAddressesQuery();
            var result = await _mediator.Object.Send(request, CancellationToken.None);
            result.ShouldBeOfType<List<Address>>();
            result.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public async Task getAddressByIdTestHappyFlow()
        {
            var request = new GetAddressByIdQuery();
            request.Id = 1;
            var result = await _mediator.Object.Send(request, CancellationToken.None);
            result.ShouldBeOfType<Address>();
            result.StreetName.ShouldBe("Antwerpenstraat");
        }

        [TestMethod]
        public async Task CreateAddressTestHappyFlow()
        {
            var command = new CreateAddressCommand();
            var address = new Address
            {
                Id = 3,
                StreetName = "Bruggestraat",
                Commune = "Brussel",
                ZipCode = "1000",
                HouseNr = "34",
                Site = null
            };

            command.Address = address;
            var result = await _mediator.Object.Send(command, CancellationToken.None);
            result.ShouldBeOfType<Address>();
            result.StreetName.ShouldBe("Bruggestraat");
        }

        [TestMethod]
        public async Task UpdateAddressTestHappyFlow()
        {
            var command = new UpdateAddressCommand();
            var address = new Address()
            {
                Id = 2,
                StreetName = "Bruggestraat",
                Commune = "Brugge",
                ZipCode = "8000",
                HouseNr = "45",
                Site = null
            };

            command.Address = address;
            await _mediator.Object.Send(command, CancellationToken.None);
            var query = new GetAddressByIdQuery();
            query.Id = 2;
            var res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeOfType<Address>();
            res.StreetName.ShouldBe("Bruggestraat");
        }

        [TestMethod]
        public async Task DeleteAddressTestHappyFlow()
        {
            var query = new GetAddressByIdQuery();
            query.Id = 2;
            var res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeOfType<Address>();
            var command = new DeleteAddressCommand();
            command.Id = 2;
            var result = await _mediator.Object.Send(command, CancellationToken.None);
            res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeNull();
        }
    }
}
