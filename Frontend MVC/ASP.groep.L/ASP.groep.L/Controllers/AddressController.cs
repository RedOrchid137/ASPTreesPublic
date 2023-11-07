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
    public class AddressController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public AddressController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(GetAllAddressesQuery query)
        {
            return View(await _mediator.Send(query));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetAddressByIdQuery();
            query.Id = (int)id;
            var Address = await _mediator.Send(query);
            if (Address == null)
            {
                return NotFound();
            }

            return View(Address);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["SiteId"] = new SelectList(await _mediator.Send(new GetAllSitesQuery()), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StreetName,Commune,ZipCode,HouseNr,Site")] Address Address)
        {
            var command = new CreateAddressCommand();
            command.Address = Address;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id, GetAddressByIdQuery query)
        {
            if (id == null)
            {
                return NotFound();
            }

            query.Id = (int)id;
            var Address = await _mediator.Send(query);
            ViewData["SiteId"] = new SelectList(await _mediator.Send(new GetAllSitesQuery()), "Id", "Name");
            return View(_mapper.Map<Address>(Address));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,StreetName,Commune,ZipCode,HouseNr,Site")] Address Address)
        {
            var command = new UpdateAddressCommand();
            command.Address = Address;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetAddressByIdQuery();
            query.Id = (int)id;
            var Address = await _mediator.Send(query);
            if (Address == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<Address>(Address));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteAddressCommand command = new DeleteAddressCommand();
            command.Id = id;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AddressExists(int id)
        {
            if (id == null)
            {
                return false;
            }
            var query = new GetAddressByIdQuery();
            query.Id = id;
            var Address = await _mediator.Send(query);
            if (Address != null)
            {
                return true;
            }
            return false;
        }
    }
}
