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
    [Authorize(Roles = "MVC Admin")]
    public class SiteController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public SiteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        // GET: api/Site
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<IEnumerable<SiteDTO>>(await _mediator.Send(new GetAllSitesQuery())));
        }

        // GET api/Site/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GetSiteByIdQuery query = new GetSiteByIdQuery();
            query.Id = id;
            return Ok(_mapper.Map<SiteDTO>(await _mediator.Send(query)));
        }

        // POST api/Site
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Site site)
        {
            CreateSiteCommand command = new CreateSiteCommand();
            command.Site = site;
            return Ok(await _mediator.Send(command));
        }


        // PUT api/Site/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Site site)
        {
            UpdateSiteCommand command = new UpdateSiteCommand();
            command.Site = site;
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/Site/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteSiteCommand command = new DeleteSiteCommand();
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
    }
}
