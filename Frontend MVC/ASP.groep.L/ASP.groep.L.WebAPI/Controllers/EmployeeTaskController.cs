using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.groep.L.WebAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ReadTasks")]
    public class EmployeeTaskController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public EmployeeTaskController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/EmployeeTask>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllEmployeeTasksQuery query = new GetAllEmployeeTasksQuery();
            query.pageSize = 10;
            query.pageNr = 1;
            var res = await _mediator.Send(query);
            return Ok(_mapper.Map<IEnumerable<EmployeeTaskDetailDTO>>(res));
        }


        // GET: api/EmployeeTask/UserTasksToday>
        [HttpGet("UserTasksToday")]
        public async Task<IActionResult> UserTasksToday()
        {
            GetUserTasksTodayQuery query = new GetUserTasksTodayQuery();
            query.pageSize = 10;
            query.pageNr = 1;
            // Get the claims values
            var email = User.Claims.Where(c => c.Type == "email")
                               .Select(c => c.Value).FirstOrDefault();
            query.UserMail = email;
            var res = await _mediator.Send(query);
            return Ok(_mapper.Map<IEnumerable<EmployeeTaskDetailDTO>>(res));
        }

        // GET: api/EmployeeTask/UserTasksSchedule>
        [HttpGet("UserTasksSchedule")]
        public async Task<IActionResult> UserTasksSchedule()
        {
            GetUserTasksScheduleQuery query = new GetUserTasksScheduleQuery();
            query.pageSize = 10;
            query.pageNr = 1;
            var email = User.Claims.Where(c => c.Type == "email")
                               .Select(c => c.Value).FirstOrDefault();
            query.UserMail = email;
            var res = await _mediator.Send(query);
            return Ok(_mapper.Map<IEnumerable<EmployeeTaskDetailDTO>>(res));
        }

        // GET api/EmployeeTask/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetEmployeeTaskByIdQuery query = new GetEmployeeTaskByIdQuery();
            query.Id = id;
            return Ok(_mapper.Map<EmployeeTaskDetailDTO>(await _mediator.Send(query)));
        }

        // PUT api/EmployeeTask/Start>
        [HttpPut("Start")]
        public async Task<IActionResult> Start([FromBody] EmployeeTaskDetailDTO EmployeeTaskDTO)
        {
            UpdateEmployeeTaskCommand command = new UpdateEmployeeTaskCommand();
            command.EmployeeTask = _mapper.Map<EmployeeTask>(EmployeeTaskDTO);
            var query = new GetEmployeeTaskByIdQuery();
            query.Id = command.EmployeeTask.Id;
            command.PreviousVersion = await _mediator.Send(query);
            return Ok(await _mediator.Send(command));
        }

        // PUT api/EmployeeTask/Stop>
        [HttpPut("Stop")]
        public async Task<IActionResult> Stop([FromBody] EmployeeTaskDetailDTO EmployeeTaskDTO)
        {
            UpdateEmployeeTaskCommand command = new UpdateEmployeeTaskCommand();
            command.EmployeeTask = _mapper.Map<EmployeeTask>(EmployeeTaskDTO);
            var query = new GetEmployeeTaskByIdQuery();
            query.Id = command.EmployeeTask.Id;
            command.PreviousVersion = await _mediator.Send(query);
            return Ok(await _mediator.Send(command));
        }

        // POST api/EmployeeTask>
        [HttpPost]
        [Authorize(Policy = "EditTasks")]
        public async Task<IActionResult> Post([FromBody] EmployeeTask EmployeeTask)
        {
            CreateEmployeeTaskCommand command = new CreateEmployeeTaskCommand();
            command.EmployeeTask = EmployeeTask;
            await _mediator.Send(command);
            return Ok("Task Added");
        }

        // PUT api/EmployeeTask/5
        [HttpPut("{id}")]
        [Authorize(Policy = "EditTasks")]
        public async Task<IActionResult> Put([FromBody] EmployeeTask EmployeeTask)
        {
            UpdateEmployeeTaskCommand command = new UpdateEmployeeTaskCommand();
            command.EmployeeTask = EmployeeTask;
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/EmployeeTask/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "EditTasks")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteEmployeeTaskCommand command = new DeleteEmployeeTaskCommand();
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }



    }
}
