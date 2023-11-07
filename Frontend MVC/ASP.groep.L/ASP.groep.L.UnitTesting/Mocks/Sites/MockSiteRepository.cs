using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.Mocks.Sites
{
    public static class MockSiteRepository
    {
        public static Mock<ISitesRepository> GetSiteRepo()
        {
            var sites = new List<Site>
            {
                new Site
                {
                    Id = 1,
                    Name = "Antwerpen",
                    Description = "Site in Antwerpen",
                    BluePrint = "",
                    BluePrintName = "SiteAntwerpen",
                    TreeFarmId = 1,
                    TreeFarm = null,
                    AddressId = 1,
                    Address = null,
                    Zones = null
                },
                new Site
                {
                    Id = 2,
                    Name = "Gent",
                    Description = "Site in Gent",
                    BluePrint = "",
                    BluePrintName = "SiteGent",
                    TreeFarmId = 2,
                    TreeFarm = null,
                    AddressId = 2,
                    Address = null,
                    Zones = null
                },
            };

            var mockRepo = new Mock<ISitesRepository>();
            mockRepo.Setup(x => x.GetAll(1,10)).ReturnsAsync(sites);

            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int Id) =>
            {
                return sites.FirstOrDefault(p => p.Id == Id);
            });

            mockRepo.Setup(x => x.Create(It.IsAny<Site>())).Returns((Site site) =>
            {
                sites.Add(site);
                return site;
            });

            mockRepo.Setup(x => x.Update(It.IsAny<Site>())).Returns((Site site) =>
            {
                int index = sites.FindIndex(s => s.Id == site.Id);
                if (index != -1)
                    sites[index] = site;
                return site;
            });

            mockRepo.Setup(x => x.Delete(It.IsAny<Site>())).Callback((Site site) =>
            {
                int index = sites.FindIndex(s => s.Id == site.Id);
                if (index != -1)
                    sites.RemoveAt(index);
            });

            return mockRepo;
        }
    }
}
