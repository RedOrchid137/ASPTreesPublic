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

namespace ASP.groep.L.UnitTesting.CQRS.HappyFlows
{

    [TestClass]
    public class WorkScheduleCQRSTestHappyFlow
    {
        private readonly Mock<IMediator> _mediator;

        public WorkScheduleCQRSTestHappyFlow()
        {
            _mediator = MockWorkScheduleMediator.GetMediator();
        }

        [TestMethod]
        public async Task GetAllWorkSchedulesTestHappyFlow()
        {
            var request = new GetAllWorkSchedulesQuery();
            var result = await _mediator.Object.Send(request,CancellationToken.None);
            result.ShouldBeOfType<List<WorkSchedule>>();
            result.Count().ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public async Task GetWorkScheduleByIdTestHappyFlow()
        {
            var request = new GetWorkScheduleByIdQuery();
            request.Id = 1;
            var result = await _mediator.Object.Send(request, CancellationToken.None);
            result.ShouldBeOfType<WorkSchedule>();
            result.Description.ShouldBe("Werken volgens de planning");
            result.EmployeeId.ShouldBe(1);
            result.PlannerId.ShouldBe(1);


            var req2 = new GetWorkScheduleByIdQuery();
            req2.Id = 2;
            var res2 = await _mediator.Object.Send(req2, CancellationToken.None);
            res2.ShouldBeOfType<WorkSchedule>();
            res2.EmployeeId.ShouldBe(2);
            res2.PlannerId.ShouldBe(1);
            res2.Description.ShouldBe("Werken voor employee 2");
        }
        [TestMethod]
        public async Task CreateWorkScheduleTestHappyFlow()
        {
            var command = new CreateWorkScheduleCommand();
            var WorkSchedule = new WorkSchedule()
            {
                Id = 3,
                EmployeeId = 3,
                PlannerId = 4,
                Description = "Werken volgens de planning",
                EmployeeTasks = new List<EmployeeTask>()
            };
            command.WorkSchedule = WorkSchedule;
            var result = await _mediator.Object.Send(command, CancellationToken.None);
            result.ShouldBeOfType<WorkSchedule>();
            result.Description.ShouldBe("Werken volgens de planning");
            result.Id.ShouldBe(3);
            result.EmployeeId.ShouldBe(3);
            result.PlannerId.ShouldBe(4);
        }
        [TestMethod]
        public async Task UpdateWorkScheduleTestHappyFlow()
        {
            var command = new UpdateWorkScheduleCommand();
            var WorkSchedule = new WorkSchedule()
            {
                Id = 3,
                EmployeeId = 5,
                PlannerId = 6,
                Description = "Werken",
                EmployeeTasks = new List<EmployeeTask>()
            };
            command.WorkSchedule = WorkSchedule;
            var result = await _mediator.Object.Send(command, CancellationToken.None);
            result.ShouldBeOfType<WorkSchedule>();
            result.Description.ShouldBe("Werken");
            result.Id.ShouldBe(3);
            result.EmployeeId.ShouldBe(5);
            result.PlannerId.ShouldBe(6);
        }
        [TestMethod]
        public async Task DeleteWorkScheduleTestHappyFlow()
        {
            var query = new GetWorkScheduleByIdQuery();
            query.Id = 2;
            var res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeOfType<WorkSchedule>();
            var command = new DeleteWorkScheduleCommand();
            command.Id = 2;
            var result = await _mediator.Object.Send(command, CancellationToken.None);
            res = await _mediator.Object.Send(query, CancellationToken.None);
            res.ShouldBeNull();
        }
    }
}
