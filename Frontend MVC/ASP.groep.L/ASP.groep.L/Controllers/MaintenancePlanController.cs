using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Domain;
using Microsoft.AspNetCore.Mvc;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Application.CQRS.Commands;
using MediatR;
using AutoMapper;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASP.groep.L.Application.FileManagement;
using ASP.groep.L.Application.Extensions;

namespace ASP.groep.L.Controllers
{
    [Authorize(Roles = "MVC Admin")]
    public class MaintenancePlanController : Controller
    {
        IMediator _mediator;
        public MaintenancePlanController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Create(int Id)
        {
            var query = new GetTreeSpeciesByIdQuery();
            query.Id = Id;
            ViewData["TreeSpecies"] = await _mediator.Send(query);
            ViewData["Season"] = new SelectList(Enum.GetValues(typeof(Season)));
            return View();
        }

        //POST-create
        [HttpPost]
        [ValidateAntiForgeryToken] //Check if we still have a token (login token)
        public async Task<IActionResult> Create([Bind("Id,Description,Title,PlanFile,Season")] MaintenancePlan plan)
        {
            if (String.IsNullOrEmpty(plan.Link)&&plan.PlanFile != null)
            {
                Stream image = plan.PlanFile.OpenReadStream();
                byte[] bytes = new byte[plan.PlanFile.Length];
                image.Read(bytes, 0, (int)plan.PlanFile.Length);
                var memoryStream = new MemoryStream(bytes);
                plan.FileName = plan.PlanFile.FileName;
                plan.Link = await FileManagement.UploadPDF(memoryStream, plan.FileName);
                var mem = GenerateQRCode.GenerateQR(plan.Link);
                plan.QRCodeName = "QRCode" + plan.Id + plan.Season.ToString() + new Random().Next(0, 100);
                plan.QRCode = await FileManagement.UploadImage(mem, plan.QRCodeName);
            }
            var command = new CreateMaintenancePlanCommand();
            command.MaintenancePlan = plan;
            _mediator.Send(command);
            return RedirectToAction("Index","TreeSpecies");
        }
        public async Task<IActionResult> Update(int Id)
        {
            var query = new GetMaintenancePlanByIdQuery();
            query.Id = (int)Id;
            var plan = await _mediator.Send(query);
            ViewData["Season"] = new SelectList(Enum.GetValues(typeof(Season)));
            return View(plan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("Id,Description,Title,PlanFile,Season")] MaintenancePlan plan)
        {
            if (String.IsNullOrEmpty(plan.Link) && plan.PlanFile != null)
            {
                Stream image = plan.PlanFile.OpenReadStream();
                byte[] bytes = new byte[plan.PlanFile.Length];
                image.Read(bytes, 0, (int)plan.PlanFile.Length);
                var memoryStream = new MemoryStream(bytes);
                plan.FileName = plan.PlanFile.FileName;
                plan.Link = await FileManagement.UploadPDF(memoryStream, plan.FileName);
                var mem = GenerateQRCode.GenerateQR(plan.Link);
                plan.QRCodeName = "QRCode" + plan.Id + plan.Season.ToString() + new Random().Next(0, 100);
                plan.QRCode = await FileManagement.UploadImage(mem, plan.QRCodeName);
            }
            else if (!String.IsNullOrEmpty(plan.Link) && !String.IsNullOrEmpty(plan.QRCode))
            {
                await FileManagement.DeletePDF(plan.FileName);
                await FileManagement.DeleteImage(plan.QRCodeName);
                Stream image = plan.PlanFile.OpenReadStream();
                byte[] bytes = new byte[plan.PlanFile.Length];
                image.Read(bytes, 0, (int)plan.PlanFile.Length);
                var memoryStream = new MemoryStream(bytes);
                plan.FileName = plan.PlanFile.FileName;
                plan.Link = await FileManagement.UploadPDF(memoryStream, plan.FileName);
                var mem = GenerateQRCode.GenerateQR(plan.Link);
                plan.QRCodeName = "QRCode" + plan.Id + plan.Season.ToString() + new Random().Next(0, 100);
                plan.QRCode = await FileManagement.UploadImage(mem, plan.QRCodeName);
            }
            var command = new UpdateMaintenancePlanCommand();
            command.MaintenancePlan = plan;
            await _mediator.Send(command);
            return RedirectToAction("Index", "TreeSpecies");
        }
    }
}
