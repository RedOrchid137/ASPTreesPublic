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
using ASP.groep.L.Application.CQRS.Validators.Commands.Update;
using FluentValidation.Results;

namespace ASP.groep.L.UnitTesting.CQRS.Exceptions
{

    [TestClass]
    public class Update_WorkScheduleCQRSTestExceptions
    {
        private readonly Mock<IMediator> _mediator;
        private List<EmployeeTask> standardTasks;

        private WorkSchedule standardSchedule;
        public Update_WorkScheduleCQRSTestExceptions()
        {
            _mediator = MockWorkScheduleMediator.GetMediator();

            standardTasks = new List<EmployeeTask> {
                new EmployeeTask
                {
                    Id = 1,
                    Name = "Bomen Snoeien",
                    Description = "Bomen in zone 3 moeten dringend gesnoeid worden",
                    scheduledStart = DateTime.Now,
                    scheduledStop = DateTime.Now,
                    WorkScheduleId = 1,
                    WorkSchedule = new WorkSchedule
                    {
                        Id = 1,
                        Description = "test",
                        EmployeeId = 1,
                        PlannerId = 1,
                        EmployeeTasks = new List<EmployeeTask>()
                    },
                    ZoneId = 1,
                    Zone = new Zone
                    {
                        Id = 1,
                        SiteId = 1,
                        Site = new Site
                        {
                            Id = 1
                        },
                        EmployeeTasks = new List<EmployeeTask>()
                    },
                    Priority = Priority.High
                },
                new EmployeeTask
                {
                    Id = 2,
                    Name = "Gras Maaien",
                    Description = "Het Gras in alle zones is stilaan aan het groeien, maai het",
                    scheduledStart = DateTime.Now,
                    scheduledStop = DateTime.Now,
                    WorkScheduleId = 1,
                    WorkSchedule = new WorkSchedule
                    {
                        Id = 1,
                        Description = "test",
                        EmployeeId = 1,
                        PlannerId = 1,
                        EmployeeTasks = new List<EmployeeTask>()
                    },
                    ZoneId = 1,
                    Zone = new Zone
                    {
                        Id = 1,
                        SiteId = 1,
                        Site = new Site
                        {
                            Id = 1
                        },
                        EmployeeTasks = new List<EmployeeTask>()
                    },
                    Priority = Priority.Medium
                }
            };
            standardSchedule = new WorkSchedule
            {
                Id = 1,
                Description = "test",
                EmployeeId = 1,
                PlannerId = 1,
                EmployeeTasks = standardTasks
            };
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTest_NotNullFailure()
        {
            var validator = new UpdateWorkScheduleCommandValidator();
            var command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = null;
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("WorkSchedule cannot be NULL");
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTest_TaskLengthFailure()
        {
            var validator = new UpdateWorkScheduleCommandValidator();
            var command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = standardSchedule;
            var tasks = command.WorkSchedule.EmployeeTasks.ToList();
            tasks[0].scheduledStart = DateTime.Now;
            tasks[0].scheduledStop = DateTime.Now.AddHours(10);
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Employee can only be assigned tasks of up to 8 hours");
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTestTasks_WithinWeekFailure()
        {
            var validator = new UpdateWorkScheduleCommandValidator();
            var command = new UpdateWorkScheduleCommand();        
            command.WorkSchedule = standardSchedule;
            var tasks = command.WorkSchedule.EmployeeTasks.ToList();
            tasks[0].scheduledStart = DateTime.Now;
            tasks[1].scheduledStart = DateTime.Now.AddDays(10);
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Work Schedule has to contain days within the same week");
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTestTasks_5DaysFailure()
        {
            var validator = new UpdateWorkScheduleCommandValidator();
            var command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = standardSchedule;
            var tasks = command.WorkSchedule.EmployeeTasks.ToList();
            tasks[0].scheduledStart = DateTime.Now;
            tasks[1].scheduledStart = DateTime.Now.AddDays(1);
            for (int i = 2; i < 6; i++)
            {
                tasks.Add(new EmployeeTask { scheduledStart = DateTime.Now.AddDays(i) });
            }
            command.WorkSchedule.EmployeeTasks = tasks;
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Work Schedule can only contain 5 workdays");
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTestTasks_ZoneEveryDayFailure()
        {
            var validator = new UpdateWorkScheduleCommandValidator();
            var command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = standardSchedule;
            var tasks = command.WorkSchedule.EmployeeTasks.ToList();
            tasks[0].scheduledStart = DateTime.Now;
            tasks[1].scheduledStart = DateTime.Now.AddDays(1);
            tasks.Add(new EmployeeTask { scheduledStart = DateTime.Now.AddDays(2), ZoneId=2 });
            command.WorkSchedule.EmployeeTasks = tasks;
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Each zone has to be scheduled at least once every day");
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTestTasks_AddTaskNotNullFailure()
        {
            var validator = new UpdateWorkScheduleCommandValidator();
            var command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = standardSchedule;
            var tasks = command.WorkSchedule.EmployeeTasks.ToList();
            tasks[0].scheduledStart = DateTime.Now;
            tasks[1].Name = "";
            command.WorkSchedule.EmployeeTasks = tasks;
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Task Name cannot be NULL");
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTestTasks_EmployeeSiteLimitFailure()
        {
            var validator = new UpdateWorkScheduleCommandValidator();
            var command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = standardSchedule;
            var tasks = command.WorkSchedule.EmployeeTasks.ToList();
            tasks[0].scheduledStart = DateTime.Now;
            tasks[1].Zone.Site.Id = 2;
            tasks[1].Zone.SiteId = 2;
            tasks[0].WorkSchedule.EmployeeTasks = tasks;
            tasks[1].WorkSchedule.EmployeeTasks = tasks;
            command.WorkSchedule.EmployeeTasks = tasks;
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Employee can only be employed at 1 site per week");
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTestTasks_Employee4taskLimitFailure()
        {
            var validator = new UpdateWorkScheduleCommandValidator();
            var command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = standardSchedule;
            var tasks = command.WorkSchedule.EmployeeTasks.ToList();
            tasks[0].scheduledStart = DateTime.Now;
            tasks[1].scheduledStart = DateTime.Now;
            for (int i = 2; i < 6; i++)
            {
                tasks.Add(new EmployeeTask { scheduledStart = DateTime.Now, scheduledStop = DateTime.Now, Name = "test", Zone = new Zone { SiteId = 1 }, WorkSchedule = new WorkSchedule { EmployeeTasks = tasks } });
            }
            command.WorkSchedule.EmployeeTasks = tasks;
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Employee can only have 4 tasks in a single workday");
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTestTasks_EmployeeZoneLimitFailure()
        {
            var validator = new UpdateWorkScheduleCommandValidator();
            var command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = standardSchedule;
            var tasks = command.WorkSchedule.EmployeeTasks.ToList();
            tasks[0].WorkSchedule.EmployeeId = 2;
            tasks[0].Zone.EmployeeTasks = tasks;
            tasks[1].Zone.EmployeeTasks = tasks;
            command.WorkSchedule.EmployeeTasks = tasks;
            ValidationResult res = validator.Validate(command);
            res.Errors.Count.ShouldBeGreaterThan(0);
            res.Errors[0].ErrorMessage.ShouldBe("Only one employee can be assigned to each zone every day");
        }
    }
}
