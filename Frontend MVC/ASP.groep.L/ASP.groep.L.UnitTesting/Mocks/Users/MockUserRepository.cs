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

namespace ASP.groep.L.UnitTesting.Mocks.Users
{
    public static class MockUserRepository
    {
        public static Mock<IUserRepository> GetUserRepo()
        {
            var users = new List<User>
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Bart",
                    LastName = "Bartmans",
                    Email= "employee@trees.com",
                    Role = Role.Ionic_Employee
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Kurt",
                    LastName = "Kurtmans",
                    Email = "planner@trees.com",
                    Role = Role.MVC_Planner
                }
            };
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(x => x.GetAll(1,10)).ReturnsAsync(users);

            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int Id) =>
            {
                return users.FirstOrDefault(p => p.Id == Id);
            });

            mockRepo.Setup(x => x.GetByEmail(It.IsAny<String>())).ReturnsAsync((String email) =>
            {
                return users.FirstOrDefault(p => p.Email == email);
            });


            mockRepo.Setup(x=>x.Create(It.IsAny<User>())).Returns((User user) =>
            {
                users.Add(user);
                return user;
            }       
            );
            mockRepo.Setup(x => x.Update(It.IsAny<User>())).Returns((User user) =>
            {

                int index = users.FindIndex(s => s.Id == user.Id);
                if (index != -1)
                    users[index] = user;
                return user;
            }
            );
            mockRepo.Setup(x => x.Delete(It.IsAny<User>())).Callback((User user)
            => 
            {
                int index = users.FindIndex(s => s.Id == user.Id);
                if (index != -1)
                    users.RemoveAt(index);
            });

            return mockRepo;

        }
    }
}
