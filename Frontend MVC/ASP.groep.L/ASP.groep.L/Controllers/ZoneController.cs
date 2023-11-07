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

namespace ASP.groep.L.Controllers
{
    [Authorize(Roles = "MVC Admin")]
    public class ZoneController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public ZoneController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(GetAllZonesQuery query)
        {
            return View(await _mediator.Send(query));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetZoneByIdQuery();
            query.Id = (int)id;
            var Zone = await _mediator.Send(query);
            if (Zone == null)
            {
                return NotFound();
            }

            return View(Zone);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["SiteId"] = new SelectList(await _mediator.Send(new GetAllSitesQuery()), "Id", "Name");
            ViewData["TreeSpeciesId"] = new SelectList(await _mediator.Send(new GetAllTreeSpeciesQuery()), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,SurfaceArea,SiteId,Site,TreeSpeciesId,TreeSpecies")] Zone Zone)
        {
            var treequery = new GetTreeSpeciesByIdQuery();
            treequery.Id = Zone.TreeSpeciesId;
            var tree = await _mediator.Send(treequery);
            var sitequery = new GetSiteByIdQuery();
            sitequery.Id = Zone.SiteId;
            var site = await _mediator.Send(sitequery);
            var command = new CreateZoneCommand();
            command.Zone = Zone;
            command.Zone.TreeSpecies = tree;
            command.Zone.Site = site;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id, GetZoneByIdQuery query)
        {
            if (id == null)
            {
                return NotFound();
            }

            query.Id = (int)id;
            var Zone = await _mediator.Send(query);
            ViewData["SiteId"] = new SelectList(await _mediator.Send(new GetAllSitesQuery()), "Id", "Name");
            ViewData["TreeSpeciesId"] = new SelectList(await _mediator.Send(new GetAllTreeSpeciesQuery()), "Id", "Name");
            return View(_mapper.Map<Zone>(Zone));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Description,SurfaceArea,SiteId,TreeSpeciesId")] Zone Zone)
        {
            var treequery = new GetTreeSpeciesByIdQuery();
            treequery.Id = Zone.TreeSpeciesId;
            var tree = await _mediator.Send(treequery);
            var sitequery = new GetSiteByIdQuery();
            sitequery.Id = Zone.SiteId;
            var site = await _mediator.Send(sitequery);
            var command = new UpdateZoneCommand();
            command.Zone = Zone;
            command.Zone.TreeSpecies = tree;
            command.Zone.Site = site;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetZoneByIdQuery();
            query.Id = (int)id;
            var Zone = await _mediator.Send(query);
            if (Zone == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<Zone>(Zone));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteZoneCommand command = new DeleteZoneCommand();
            command.Id = id;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ZoneExists(int id)
        {
            if (id == null)
            {
                return false;
            }
            var query = new GetZoneByIdQuery();
            query.Id = id;
            var Zone = await _mediator.Send(query);
            if (Zone != null)
            {
                return true;
            }
            return false;
        }
    }
}
