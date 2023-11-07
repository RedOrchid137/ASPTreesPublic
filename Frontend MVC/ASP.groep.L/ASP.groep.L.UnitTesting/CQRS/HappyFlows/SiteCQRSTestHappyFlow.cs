using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Domain;
using ASP.groep.L.UnitTesting.Mocks.Sites;
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
    public class SiteCQRSTestHappyFlow
    {
        private readonly Mock<IMediator> _mediator;

        public SiteCQRSTestHappyFlow()
        {
            _mediator = MockSiteMediator.GetMediator();
        }

        [TestMethod]
        public async Task GetAllSitesTestHappyFlow()
        {
            var request = new GetAllSitesQuery();
            var result = await _mediator.Object.Send(request, CancellationToken.None);
            result.ShouldBeOfType<List<Site>>();
            result.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public async Task getSiteByIdTestHappyFlow()
        {
            var request = new GetSiteByIdQuery();
            request.Id = 1;
            var result = await _mediator.Object.Send(request, CancellationToken.None);
            result.ShouldBeOfType<Site>();
            result.Name.ShouldBe("Antwerpen");
        }

        [TestMethod]
        public async Task CreateSiteTestHappyFlow()
        {
            var command = new CreateSiteCommand();
            var site = new Site
            {
                Id = 3,
                Name = "Brussel",
                Description = "Site in Brussel",
                BluePrint = "",
                BluePrintName = "SiteBrussel",
                TreeFarmId = 3,
                TreeFarm = null,
                AddressId = 3,
                Address = null,
                Zones = null
            };

            command.Site = site;
            var result = await _mediator.Object.Send(command, CancellationToken.None);
            result.ShouldBeOfType<Site>();
            result.Name.ShouldBe("Brussel");
        }

        [TestMethod]
        public async Task UpdateSiteTestHappyFlow()
        {
            var command = new UpdateSiteCommand();
            var site = new Site()
            {
                Id = 2,
                Name = "Brugge",
                Description = "Site in Brugge",
                BluePrint = "",
                BluePrintName = "SiteBrugge",
                TreeFarmId = 2,
                TreeFarm = null,
                AddressId = 2,
                Address = null,
                Zones = null
            };

            command.Site = site;
            await _mediator.Object.Send(command, CancellationToken.None);
            var query = new GetSiteByIdQuery();
            query.Id = 2;
            var res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeOfType<Site>();
            res.Name.ShouldBe("Brugge");
        }

        [TestMethod]
        public async Task DeleteSiteTestHappyFlow()
        {
            var query = new GetSiteByIdQuery();
            query.Id = 2;
            var res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeOfType<Site>();
            var command = new DeleteSiteCommand();
            command.Id = 2;
            var result = await _mediator.Object.Send(command, CancellationToken.None);
            res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeNull();
        }
    }
}
