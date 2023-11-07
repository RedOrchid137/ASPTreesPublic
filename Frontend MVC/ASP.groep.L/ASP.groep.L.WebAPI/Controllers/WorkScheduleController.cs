using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Domain;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.groep.L.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ReadSchedules")]
    public class WorkScheduleController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public WorkScheduleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        // GET: api/WorkSchedule
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<IEnumerable<WorkScheduleDTO>>(await _mediator.Send(new GetAllWorkSchedulesQuery())));
        }

        // GET api/WorkSchedule/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetWorkScheduleByIdQuery query = new GetWorkScheduleByIdQuery();
            query.Id = id;
            return Ok(_mapper.Map<WorkScheduleDTO>(await _mediator.Send(query)));
        }

        // POST api/WorkSchedule
        [HttpPost]
        [Authorize(Policy = "EditSchedules")]
        public async Task<IActionResult> Post([FromBody] WorkSchedule schedule)
        {
            CreateWorkScheduleCommand command = new CreateWorkScheduleCommand();
            command.WorkSchedule = schedule;
            return Ok(await _mediator.Send(command));
        }


        // PUT api/WorkSchedule/5
        [HttpPut("{id}")]
        [Authorize(Policy = "EditSchedules")]
        public async Task<IActionResult> Put([FromBody] WorkSchedule schedule)
        {
            UpdateWorkScheduleCommand command = new UpdateWorkScheduleCommand();
            command.WorkSchedule = schedule;
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/WorkSchedule/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "EditSchedules")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteWorkScheduleCommand command = new DeleteWorkScheduleCommand();
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
    }
}
