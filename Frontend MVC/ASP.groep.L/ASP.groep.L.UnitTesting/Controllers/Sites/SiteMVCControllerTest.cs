using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Controllers;
using ASP.groep.L.Domain;
using ASP.groep.L.UnitTesting.Mocks.Sites;
using AutoMapper;
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

namespace ASP.groep.L.UnitTesting.Controllers.Sites
{
    [TestClass]
    public class SiteMVCControllerTest
    {
        private SiteController controller;
        private readonly Mock<IMediator> _mediator;
        private readonly IMapper _mapper;

        public SiteMVCControllerTest()
        {
            _mediator = MockSiteMediator.GetMediator();
            controller = new SiteController(_mediator.Object, _mapper);
        }

        [TestMethod]
        public async Task Index(GetAllSitesQuery query)
        {
            var res = await controller.Index(query);
            res.ShouldBeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public async Task Details()
        {
            var res = await controller.Details(1);
            res.ShouldBeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create()
        {
            var res = await controller.Create();
            res.ShouldBeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public async Task Update(GetSiteByIdQuery query)
        {
            var res = await controller.Update(2, query);
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
