using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.groep.L.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "MVC Admin")]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/User/Employees>
        [HttpGet("Employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            GetAllUsersQuery query = new GetAllUsersQuery();
            query.pageSize = 10;
            query.pageNr = 1;
            return Ok(_mapper.Map<IEnumerable<UserDTO>>((await _mediator.Send(new GetAllUsersQuery())).Where<User>(u => u.Role == Role.Ionic_Employee)));
        }
        // GET: api/User/Planners>
        [HttpGet("Planners")]
        public async Task<IActionResult> GetAllPlanners()
        {
            GetAllUsersQuery query = new GetAllUsersQuery();
            query.pageSize = 10;
            query.pageNr = 1;
            return Ok(_mapper.Map<IEnumerable<UserDTO>>((await _mediator.Send(new GetAllUsersQuery())).Where<User>(u => u.Role == Role.MVC_Planner)));
        }
        // GET: api/User/Admins>
        [HttpGet("Admins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            GetAllUsersQuery query = new GetAllUsersQuery();
            query.pageSize = 10;
            query.pageNr = 1;
            return Ok(_mapper.Map<IEnumerable<UserDTO>>((await _mediator.Send(new GetAllUsersQuery())).Where<User>(u => u.Role == Role.MVC_Admin)));
        }
        // GET api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetUserByIdQuery query = new GetUserByIdQuery();
            query.Id = id;
            return Ok(_mapper.Map<UserDTO>(await _mediator.Send(query)));
        }

        // POST api/User>
        [HttpPost]
        [Authorize(Roles = "MVC Admin")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            CreateUserCommand command = new CreateUserCommand();
            command.User = user;
            return Ok(await _mediator.Send(command));
        }

        // PUT api/Employee/5
        [HttpPut("{id}")]
        [Authorize(Roles = "MVC Admin")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            UpdateUserCommand command = new UpdateUserCommand();
            command.User = user;
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/Employee/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "MVC Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteUserCommand command = new DeleteUserCommand();
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
    }
}
