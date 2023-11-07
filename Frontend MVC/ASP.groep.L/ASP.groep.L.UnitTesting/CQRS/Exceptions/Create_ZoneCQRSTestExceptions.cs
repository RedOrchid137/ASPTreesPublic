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
using ASP.groep.L.UnitTesting.Mocks.WorkSchedules;
using ASP.groep.L.Application.CQRS.Validators.Commands.Create;
using FluentValidation.Results;

namespace ASP.groep.L.UnitTesting.CQRS.Exceptions
{

    [TestClass]
    public class Create_ZoneCQRSTestExceptions
    {
        private readonly Mock<IMediator> _mediator;

        private Zone standardZone;
        public Create_ZoneCQRSTestExceptions()
        {
            _mediator = MockWorkScheduleMediator.GetMediator();
            standardZone = new Zone
            {
                Id = 1,
                Description = "test",
                Name = "test",
                SiteId = 1,
                TreeSpeciesId = 1,
                SurfaceArea = 15000,
                Site = new Site()
                {
                    Id = 1,
                    Zones = new List<Zone>()
                    {
                        new Zone()
                        {
                            Id = 1,
                            TreeSpeciesId = 1
                        },
                        new Zone()
                        {
                            Id = 2,
                            TreeSpeciesId = 1
                        },
                        new Zone()
                        {
                            Id = 3,
                            TreeSpeciesId = 1
                        }
                    }
                },
            };
        }
        [TestMethod]
        public async Task CreateZoneTest_NotNullFailure()
        {
            var validator = new CreateZoneCommandValidator();
            var command = new CreateZoneCommand();
            command.Zone = null;
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Zone cannot be NULL");
        }
        [TestMethod]
        public async Task CreateZoneTest_TreeSpecies3LimitFailure()
        {
            var validator = new CreateZoneCommandValidator();
            var command = new CreateZoneCommand();
            command.Zone = standardZone;
            command.Zone.Id = 4;
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Tree can only belong to 3 zones in the same site");
        }
    }
}
