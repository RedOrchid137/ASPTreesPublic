using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.Mocks.WorkSchedules
{
    public static class MockWorkSchedulesRepo
    {
        public static Mock<IWorkSchedulesRepository> GetWorkSchedulesRepo()
        {
            var workSchedules = new List<WorkSchedule>
            {
                new WorkSchedule
                {
                    Id = 1,
                    EmployeeId = 1,
                    PlannerId = 1,
                    Description = "Werken volgens de planning",
                    EmployeeTasks = new List<EmployeeTask>()
                },
                new WorkSchedule
                {
                    Id = 2,
                    EmployeeId = 2,
                    PlannerId = 1,
                    Description = "Werken voor employee 2",
                    EmployeeTasks = new List<EmployeeTask>()
                }
            };
            var mockRepo = new Mock<IWorkSchedulesRepository>();
            mockRepo.Setup(x => x.GetAll(1, 10)).ReturnsAsync(workSchedules);

            mockRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int Id) =>
            {
                return workSchedules.FirstOrDefault(p => p.Id == Id);
            });

            mockRepo.Setup(x => x.Create(It.IsAny<WorkSchedule>())).Returns((WorkSchedule workSchedule) =>
            {
                workSchedules.Add(workSchedule);
                return workSchedule;
            });

            mockRepo.Setup(x => x.Update(It.IsAny<WorkSchedule>())).Returns((WorkSchedule modifiedWorkSchedule) =>
            {
                int index = workSchedules.FindIndex(s => s.Id == modifiedWorkSchedule.Id);
                if (index != -1)
                {
                    workSchedules[index] = modifiedWorkSchedule;
                }
                return modifiedWorkSchedule;
            });

            mockRepo.Setup(x => x.Delete(It.IsAny<WorkSchedule>())).Callback((WorkSchedule workSchedule) =>
            {
                int index = workSchedules.FindIndex(s => s.Id == workSchedule.Id);
                if (index != -1)
                    workSchedules.RemoveAt(index);
            });

            return mockRepo;
        }
    }
}
