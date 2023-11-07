using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.groep.L.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ReadZones")]
    public class ZoneController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public ZoneController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        // GET: api/Zone
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pathBase = HttpContext.Request.PathBase;
            Debug.WriteLine(this.User);
            return Ok(_mapper.Map<IEnumerable<ZoneDTO>>(await _mediator.Send(new GetAllZonesQuery())));
        }

        // GET api/Zone/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetZoneByIdQuery query = new GetZoneByIdQuery();
            query.Id = id;
            return Ok(_mapper.Map<ZoneDTO>(await _mediator.Send(query)));
        }


        [Authorize(Policy = "EditZones")]
        // POST api/Zone
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Zone zone)
        {
            CreateZoneCommand command = new CreateZoneCommand();
            command.Zone = zone;
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = "EditZones")]
        // PUT api/Zone/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Zone zone)
        {
            UpdateZoneCommand command = new UpdateZoneCommand();
            command.Zone = zone;
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Policy = "EditZones")]
        // DELETE api/Zone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteZoneCommand command = new DeleteZoneCommand();
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
    }
}
