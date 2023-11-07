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
    [Authorize(Policy = "ReadTreeSpecies")]
    public class TreeSpeciesController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public TreeSpeciesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/TreeSpecies
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<IEnumerable<TreeSpeciesDTO>>(await _mediator.Send(new GetAllTreeSpeciesQuery())));
        }
        // GET api/TreeSpecies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetTreeSpeciesByIdQuery query = new GetTreeSpeciesByIdQuery();
            query.Id = id;
            return Ok(_mapper.Map<TreeSpeciesDTO>(await _mediator.Send(query)));
        }

        // POST api/TreeSpecies>
        [HttpPost]
        [Authorize(Policy = "EditTreeSpecies")]
        public async Task<IActionResult> Post([FromBody] TreeSpecies TreeSpecies)
        {
            CreateTreeSpeciesCommand command = new CreateTreeSpeciesCommand();
            command.TreeSpecies = TreeSpecies;
            return Ok(await _mediator.Send(command));
        }

        // PUT api/TreeSpecies/5
        [HttpPut("{id}")]
        [Authorize(Policy = "EditTreeSpecies")]
        public async Task<IActionResult> Put([FromBody] TreeSpecies TreeSpecies)
        {
            UpdateTreeSpeciesCommand command = new UpdateTreeSpeciesCommand();
            command.TreeSpecies = TreeSpecies;
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/TreeSpecies/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "EditTreeSpecies")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteTreeSpeciesCommand command = new DeleteTreeSpeciesCommand();
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
    }
}
