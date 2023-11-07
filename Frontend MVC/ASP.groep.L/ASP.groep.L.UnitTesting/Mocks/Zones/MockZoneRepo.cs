using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using ASP.groep.L.Infrastructure.Repositories;
using MediatR;
using Microsoft.Build.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.Mocks.Zones
{
    public static class MockZonesRepo
    {
        public static Mock<IZonesRepository> GetZonesRepo()
        {
            var zones = new List<Zone>
            {
                new Zone
                {
                    Id = 1,
                    Name = "Zone 1",
                    Description = "The First zone",
                    SurfaceArea = 1000.00,
                    SiteId = 1,
                    TreeSpeciesId = 1,
                },
                new Zone
                {
                    Id = 2,
                    Name = "Zone 2",
                    Description = "Zone West",
                    SurfaceArea = 134.56,
                    SiteId = 1,
                    TreeSpeciesId = 1,
                }
            };
            var mockRepo = new Mock<IZonesRepository>();
            mockRepo.Setup(x => x.GetAll(1, 10)).ReturnsAsync(zones);

            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int Id) =>
            {
                return zones.FirstOrDefault(p => p.Id == Id);
            });

            mockRepo.Setup(x => x.Create(It.IsAny<Zone>())).Returns((Zone zone) =>
            {
                zones.Add(zone);
                return zone;
            });

            mockRepo.Setup(x => x.Update(It.IsAny<Zone>())).Returns((Zone modifiedZone) =>
            {
                int index = zones.FindIndex(s => s.Id == modifiedZone.Id);
                if (index != -1)
                {
                    zones[index] = modifiedZone;
                }
                return modifiedZone;
            });

            mockRepo.Setup(x => x.Delete(It.IsAny<Zone>())).Callback((Zone zone) =>
            {
                int index = zones.FindIndex(s => s.Id == zone.Id);
                if (index != -1)
                    zones.RemoveAt(index);
            });

            return mockRepo;
        }
    }
}
