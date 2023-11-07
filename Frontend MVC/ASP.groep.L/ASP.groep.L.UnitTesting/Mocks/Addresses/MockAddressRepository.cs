using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.Mocks.Addresses
{
    public static class MockAddressRepository
    {
        public static Mock<IAddressesRepository> GetAddressRepo()
        {
            var addresses = new List<Address>
            {
                new Address
                {
                    Id = 1,
                    StreetName = "Antwerpenstraat",
                    Commune = "Antwerpen",
                    ZipCode = "2000",
                    HouseNr = "12",
                    Site = null
                },
                new Address
                {
                    Id = 2,
                    StreetName = "Gentstraat",
                    Commune = "Gent",
                    ZipCode = "9000",
                    HouseNr = "23",
                    Site = null
                },
            };

            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(x => x.GetAll(1, 10)).ReturnsAsync(addresses);

            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int Id) =>
            {
                return addresses.FirstOrDefault(p => p.Id == Id);
            });

            mockRepo.Setup(x => x.Create(It.IsAny<Address>())).Returns((Address address) =>
            {
                addresses.Add(address);
                return address;
            });

            mockRepo.Setup(x => x.Update(It.IsAny<Address>())).Returns((Address address) =>
            {
                int index = addresses.FindIndex(s => s.Id == address.Id);
                if (index != -1)
                    addresses[index] = address;
                return address;
            });

            mockRepo.Setup(x => x.Delete(It.IsAny<Address>())).Callback((Address address) =>
            {
                int index = addresses.FindIndex(s => s.Id == address.Id);
                if (index != -1)
                    addresses.RemoveAt(index);
            });

            return mockRepo;
        }
    }
}
