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
using Firebase.Auth;

namespace ASP.groep.L.UnitTesting.Mocks.EmployeeTasks
{
    public static class MockEmployeeTaskRepo
    {
        public static Mock<IEmployeeTasksRepository> GetEmployeeTasks()
        {
            var tasks = new List<EmployeeTask>
            {
                new EmployeeTask
                {
                    Id = 1,
                    Name = "Bomen Snoeien",
                    Description = "Bomen in zone 3 moeten dringend gesnoeid worden",
                    scheduledStart = DateTime.Now,
                    actualStart = DateTime.Now,
                    scheduledStop = new DateTime(2022, 12, 25),
                    actualStop = new DateTime(2022, 12, 25),
                    WorkScheduleId = 1,
                    ZoneId = 1,
                    //IsDone = false,
                    Priority = Priority.High
                },
                new EmployeeTask
                {
                    Id = 2,
                    Name = "Gras Maaien",
                    Description = "Het Gras in alle zones is stilaan aan het groeien, maai het",
                    scheduledStart = DateTime.Now,
                    actualStart = DateTime.Now,
                    scheduledStop = new DateTime(2022, 12, 30),
                    actualStop = new DateTime(2022, 12, 28),
                    WorkScheduleId = 1,
                    ZoneId = 1,
                    //IsDone = false,
                    Priority = Priority.Medium
                }
            };

            var mockRepo = new Mock<IEmployeeTasksRepository>();
            mockRepo.Setup(x => x.GetAll(1, 10)).ReturnsAsync(tasks);

            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int Id) =>
            {
                return tasks.FirstOrDefault(p => p.Id == Id);
            });

            mockRepo.Setup(x => x.Create(It.IsAny<EmployeeTask>())).Returns((EmployeeTask employeeTask) =>
            {
                tasks.Add(employeeTask);
                return employeeTask;
            });

            mockRepo.Setup(x => x.Update(It.IsAny<EmployeeTask>())).Returns((EmployeeTask modifiedTask) =>
            {
                int index = tasks.FindIndex(s => s.Id == modifiedTask.Id);
                if (index != -1)
                {
                    tasks[index] = modifiedTask;
                }
                return modifiedTask;
            });

            mockRepo.Setup(x => x.Delete(It.IsAny<EmployeeTask>())).Callback((EmployeeTask employeeTask) =>
            {
                int index = tasks.FindIndex(s => s.Id == employeeTask.Id);
                if (index != -1)
                    tasks.RemoveAt(index);
            });
            return mockRepo;
        }
    }
}
