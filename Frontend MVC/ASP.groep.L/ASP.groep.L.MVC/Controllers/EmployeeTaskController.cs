using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Domain;
using Microsoft.AspNetCore.Mvc;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Application.CQRS.Commands;
using MediatR;
using AutoMapper;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.groep.L.Controllers
{
    [Authorize(Roles = "MVC Planner,MVC Admin,Ionic Employee")]
    public class EmployeeTaskController : Controller
    {
        IMediator _mediator;
        IMapper _mapper;
        public EmployeeTaskController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var User = this.User;
            GetAllEmployeeTasksQuery query = new GetAllEmployeeTasksQuery();
            IEnumerable<EmployeeTask> EmployeeTasks = await _mediator.Send(query);
            return Ok();
        }
        [Authorize(Roles = "MVC Planner,MVC Admin")]
        public async Task<IActionResult> Create(int Id)
        {
            ViewData["ZoneId"] = new SelectList(await _mediator.Send(new GetAllZonesQuery()), "Id", "Name");
            ViewData["Priority"] = new SelectList(Enum.GetValues(typeof(Priority)));
            ViewData["WorkScheduleId"] = Id;
            return View();
        }
        //POST-create
        [HttpPost]
        public EmployeeTaskDTO TestPost([FromBody]EmployeeTaskDTO task)
        {
            return task;
        }



        [Authorize(Roles = "MVC Planner,MVC Admin")]
        //POST-create
        [HttpPost]
        [ValidateAntiForgeryToken] //Check if we still have a token (login token)
        public async Task<IActionResult> Create([FromBody] EmployeeTaskDTO task)
        {
            var Task = _mapper.Map<EmployeeTask>(task);
            var zoneQuery = new GetZoneByIdQuery();
            zoneQuery.Id = Task.ZoneId;
            var zone = await _mediator.Send(zoneQuery);
            var command = new CreateEmployeeTaskCommand();
            command.EmployeeTask = Task;
            command.EmployeeTask.Zone = zone;
            return Ok(_mapper.Map<EmployeeTaskDTO>(await _mediator.Send(command)));
        } 

        [Authorize(Roles = "MVC Planner,MVC Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var query = new GetEmployeeTaskByIdQuery();
            query.Id = Id;
            var task = await _mediator.Send(query);
            var command = new DeleteEmployeeTaskCommand();
            command.Id = Id;
            await _mediator.Send(command);
            return RedirectToAction("Tasks", "WorkSchedule", new { id = task.WorkScheduleId });
        }

        [Authorize(Roles = "MVC Planner,MVC Admin")]
        public async Task<IActionResult> Update(int Id, GetEmployeeTaskByIdQuery query)
        {
            query.Id = (int)Id;
            var Task = await _mediator.Send(query);
            ViewData["ZoneId"] = new SelectList(await _mediator.Send(new GetAllZonesQuery()), "Id", "Name");
            ViewData["WorkScheduleId"] = Task.WorkScheduleId;
            ViewData["Priority"] = new SelectList(Enum.GetValues(typeof(Priority)));
            return View(Task);
        }

        [Authorize(Roles = "MVC Planner,MVC Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("Id,Description,Name,scheduledStart,scheduledStop,WorkScheduleId,WorkSchedule,ZoneId,Zone,Priority")] EmployeeTask Task)
        {
            var wsQuery = new GetWorkScheduleByIdQuery();
            var zoneQuery = new GetZoneByIdQuery();

            wsQuery.Id = (int)Task.WorkScheduleId;
            zoneQuery.Id = Task.ZoneId;
            var schedule = await _mediator.Send(wsQuery);
            var zone = await _mediator.Send(zoneQuery);
            var command = new UpdateEmployeeTaskCommand();
            command.EmployeeTask = Task;
            command.EmployeeTask.WorkSchedule = schedule;
            command.EmployeeTask.Zone = zone;
            await _mediator.Send(command);
            return RedirectToAction("Tasks", "WorkSchedule", new { id = Task.WorkScheduleId });
        }
    }
}
