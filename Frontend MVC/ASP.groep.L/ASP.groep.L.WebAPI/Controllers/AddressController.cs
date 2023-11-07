using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.groep.L.WebAPI.Controllers
{
    [Authorize (Roles = "MVC Admin,MVC Planner,Ionic Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public AddressController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        // GET: api/Address
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<IEnumerable<AddressDTO>>(await _mediator.Send(new GetAllAddressesQuery())));
        }

        // GET api/Address/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetAddressByIdQuery query = new GetAddressByIdQuery();
            query.Id = id;
            return Ok(_mapper.Map<AddressDTO>(await _mediator.Send(query)));
        }

        // POST api/Address
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Address address)
        {
            CreateAddressCommand command = new CreateAddressCommand();
            command.Address = address;
            return Ok(await _mediator.Send(command));
        }


        // PUT api/Address/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Address address)
        {
            UpdateAddressCommand command = new UpdateAddressCommand();
            command.Address = address;
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/Address/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteAddressCommand command = new DeleteAddressCommand();
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
    }
}
