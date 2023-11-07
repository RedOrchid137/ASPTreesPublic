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
using ASP.groep.L.Application.FileManagement;
using ASP.groep.L.Application.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.IO.Compression;
using System.Net;
using ASP.groep.L.AdminMVC.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace ASP.groep.L.Controllers
{
    [Authorize(Roles = "MVC Admin")]
    public class SiteController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public SiteController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(GetAllSitesQuery query)
        {
            return View(await _mediator.Send(query));
        }

        public async Task<IActionResult> Details(int? id)
        {
            var query = new GetSiteDetailQuery();
            query.Id = (int)id;
            var site = await _mediator.Send(query);
            return View(site);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["TreeFarmId"] = new SelectList(await _mediator.Send(new GetAllTreeFarmsQuery()), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,BluePrint,ImageFile,TreeFarm,TreeFarmId,Address")] Site site)
        {
            if (site.ImageFile != null)
            {
                Stream image = site.ImageFile.OpenReadStream();
                byte[] bytes = new byte[site.ImageFile.Length];
                image.Read(bytes, 0, (int)site.ImageFile.Length);
                var memoryStream = new MemoryStream(bytes);
                site.BluePrint = await FileManagement.UploadImage(memoryStream, site.ImageFile.FileName);
                site.BluePrintName = site.ImageFile.FileName;
            }
            var command = new CreateSiteCommand();
            command.Site = site;
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id, GetSiteByIdQuery query)
        {
            ViewData["TreeFarmId"] = new SelectList(await _mediator.Send(new GetAllTreeFarmsQuery()), "Id", "Name");
            query.Id = (int)id;
            var site = await _mediator.Send(query);
            return View(site);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("Id,Name,Description,BluePrint,ImageFile,TreeFarm,TreeFarmId,Address")] Site site)
        {
            if (site.ImageFile != null)
            {
                Stream image = site.ImageFile.OpenReadStream();
                byte[] bytes = new byte[site.ImageFile.Length];
                image.Read(bytes, 0, (int)site.ImageFile.Length);
                var memoryStream = new MemoryStream(bytes);
                site.BluePrint = await FileManagement.UploadImage(memoryStream, site.ImageFile.FileName);
                site.BluePrintName = site.ImageFile.FileName;
            }

            var command = new UpdateSiteCommand();
            command.Site = site;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var query = new GetSiteByIdQuery();
            query.Id = (int)id;
            var site = await _mediator.Send(query);
            return View(_mapper.Map<Site>(site));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteSiteCommand command = new DeleteSiteCommand();
            command.Id = id;
            var query = new GetSiteByIdQuery();
            query.Id = (int)id;
            var site = await _mediator.Send(query);
            if (site.BluePrintName != null && site.BluePrintName != "")
            {
                await FileManagement.DeleteImage(site.BluePrintName);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SiteExists(int id)
        {
            var query = new GetSiteByIdQuery();
            query.Id = id;
            var site = await _mediator.Send(query);

            return false;
        }
    }
}
