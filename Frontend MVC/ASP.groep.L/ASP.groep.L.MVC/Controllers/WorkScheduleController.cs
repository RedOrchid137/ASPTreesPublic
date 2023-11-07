using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.groep.L.Domain;
using ASP.groep.L.Infrastructure.Contexts;
using System.Diagnostics;
using MediatR;
using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.CQRS.Queries;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ASP.groep.L.Application.CQRS.DTOS;

namespace ASP.groep.L.Controllers {
    [Authorize(Roles = "MVC Admin,MVC Planner")]
    public class WorkScheduleController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public WorkScheduleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(GetAllWorkSchedulesQuery query)
        {
            return View(await _mediator.Send(query));
        }


        public async Task<IActionResult> Details(int? id)
        {
            var query = new GetWorkScheduleByIdQuery();
            query.Id = (int)id;
            var WorkSchedule = await _mediator.Send(query);

            return View(WorkSchedule);
        }
        public async Task<IActionResult> Tasks(int? id)
        {
            var query = new GetWorkScheduleByIdQuery();
            query.Id = (int)id;
            var WorkSchedule = await _mediator.Send(query);
            ViewData["Zone"] = new SelectList(await _mediator.Send(new GetAllZonesQuery()), "Id", "Name");
            var schedulequery = new GetAllEmployeeTasksByScheduleQuery();
            schedulequery.schedule = WorkSchedule;
            ViewData["EmployeeTasks"] = await _mediator.Send(schedulequery);
            return View(WorkSchedule);
        }
        public async Task<IActionResult> Create()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            var zones = await _mediator.Send(new GetAllZonesQuery());
            ViewData["EmployeeId"] = new SelectList(users.Where<User>(u=>u.Role==Role.Ionic_Employee), "Id", "Fullname");
            ViewData["PlannerId"] = new SelectList(users.Where<User>(u => u.Role == Role.MVC_Planner), "Id", "Fullname");
            ViewData["ZoneId"] = new SelectList(zones, "Id","Name");
            return View(new WorkSchedule());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] WorkScheduleDTO schedule)
        {
            var WorkSchedule = _mapper.Map<WorkSchedule>(schedule);
            var command = new CreateWorkScheduleCommand();
            command.WorkSchedule = WorkSchedule;
            foreach (var task in command.WorkSchedule.EmployeeTasks)
            {
                var query = new GetZoneByIdQuery();
                query.Id = task.ZoneId;
                task.Zone = await _mediator.Send(query);
                task.WorkSchedule = command.WorkSchedule;
            }
            var t = _mapper.Map<WorkScheduleDTO>(await _mediator.Send(command));
            return Ok(t);
        }


        public async Task<IActionResult> Update(int? id,GetWorkScheduleByIdQuery query)
        {
            query.Id = (int)id;
            var WorkSchedule = await _mediator.Send(query);
            if (WorkSchedule == null)
            {
                return NotFound();
            }
            var users = await _mediator.Send(new GetAllUsersQuery());
            ViewData["EmployeeId"] = new SelectList(users.Where<User>(u => u.Role == Role.Ionic_Employee), "Id", "Fullname");
            ViewData["PlannerId"] = new SelectList(users.Where<User>(u => u.Role == Role.MVC_Planner), "Id", "Fullname");
            return View(WorkSchedule);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Description,Employee,Planner,EmployeeTasks,EmployeeId,PlannerId")] WorkSchedule WorkSchedule)
        {
            var command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = WorkSchedule;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetWorkScheduleByIdQuery();
            query.Id = (int)id;
            var WorkSchedule = await _mediator.Send(query);
            if (WorkSchedule == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<WorkSchedule>(WorkSchedule));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteWorkScheduleCommand command = new DeleteWorkScheduleCommand();
            command.Id = id;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WorkScheduleExists(int id)
        {
            if (id == null)
            {
                return false;
            }
            var query = new GetWorkScheduleByIdQuery();
            query.Id = id;
            var WorkSchedule = await _mediator.Send(query);
            if (WorkSchedule != null)
            {
                return true;
            }
            return false;
        }
    }
}
