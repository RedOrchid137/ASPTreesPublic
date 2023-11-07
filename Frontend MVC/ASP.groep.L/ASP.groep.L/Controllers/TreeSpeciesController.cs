using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASP.groep.L.Domain;
using MediatR;
using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.CQRS.Queries;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ASP.groep.L.Application.FileManagement;
using ASP.groep.L.Application.Extensions;

namespace ASP.groep.L.Controllers
{
    [Authorize(Roles = "MVC Admin,MVC Planner")]
    public class TreeSpeciesController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;
        public TreeSpeciesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(GetAllTreeSpeciesQuery query)
        {
            return View(await _mediator.Send(query));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetTreeSpeciesByIdQuery();
            query.Id = (int)id;
            var TreeSpecies = await _mediator.Send(query);
            if (TreeSpecies == null)
            {
                return NotFound();
            }

            return View(TreeSpecies);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["MaintenancePlanID"] = new SelectList(await _mediator.Send(new GetAllMaintenancePlansQuery()), "Id", "Title");
            ViewData["Zone"] = new SelectList(await _mediator.Send(new GetAllZonesQuery()), "Id", "Name");
            ViewData["Season"] = new SelectList(Enum.GetValues(typeof(Season)));
            return View();
        }

        [Authorize(Roles = "MVC Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,PicturePath,ImageFile,MaintenancePlanID,MaintenancePlan")] TreeSpecies treeSpecies)
        {
            
            if (treeSpecies.ImageFile != null)
            {
                Stream image = treeSpecies.ImageFile.OpenReadStream();
                byte[] bytes = new byte[treeSpecies.ImageFile.Length];
                image.Read(bytes, 0, (int)treeSpecies.ImageFile.Length);
                var memoryStream = new MemoryStream(bytes);
                treeSpecies.ImageName = treeSpecies.ImageFile.FileName;
                treeSpecies.PicturePath = await FileManagement.UploadImage(memoryStream, treeSpecies.ImageName);
            }
            if (String.IsNullOrEmpty(treeSpecies.MaintenancePlan.Link) && treeSpecies.MaintenancePlan.PlanFile != null)
            {
                Stream image = treeSpecies.MaintenancePlan.PlanFile.OpenReadStream();
                byte[] bytes = new byte[treeSpecies.MaintenancePlan.PlanFile.Length];
                image.Read(bytes, 0, (int)treeSpecies.MaintenancePlan.PlanFile.Length);
                var memoryStream = new MemoryStream(bytes);
                treeSpecies.MaintenancePlan.FileName = treeSpecies.MaintenancePlan.PlanFile.FileName;
                treeSpecies.MaintenancePlan.Link = await FileManagement.UploadPDF(memoryStream, treeSpecies.MaintenancePlan.FileName);
                var mem = GenerateQRCode.GenerateQR(treeSpecies.MaintenancePlan.Link);
                treeSpecies.MaintenancePlan.QRCodeName = "QRCode" + treeSpecies.MaintenancePlan.Id + treeSpecies.MaintenancePlan.Season.ToString() + new Random().Next(0, 100);
                treeSpecies.MaintenancePlan.QRCode = await FileManagement.UploadImage(mem, treeSpecies.MaintenancePlan.QRCodeName);
            }


            var command = new CreateTreeSpeciesCommand();
            command.TreeSpecies = treeSpecies;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "MVC Admin")]
        public async Task<IActionResult> Update(int? id, GetTreeSpeciesByIdQuery query)
        {
            query.Id = (int)id;
            var TreeSpecies = await _mediator.Send(query);
            if (TreeSpecies == null)
            {
                return NotFound();
            }
            ViewData["MaintenancePlanID"] = new SelectList(await _mediator.Send(new GetAllMaintenancePlansQuery()), "Id", "Title");
            ViewData["Zone"] = new SelectList(await _mediator.Send(new GetAllZonesQuery()), "Id", "Name");
            ViewData["Season"] = new SelectList(Enum.GetValues(typeof(Season)));
            return View(TreeSpecies);
        }
        [Authorize(Roles = "MVC Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Description,PicturePath,ImageFile,MaintenancePlanID,MaintenancePlan")] TreeSpecies treeSpecies)
        {
            if (treeSpecies.ImageFile!=null)
            {
                Stream image = treeSpecies.ImageFile.OpenReadStream();
                byte[] bytes = new byte[treeSpecies.ImageFile.Length];
                image.Read(bytes, 0, (int)treeSpecies.ImageFile.Length);
                var memoryStream = new MemoryStream(bytes);
                treeSpecies.ImageName = treeSpecies.ImageFile.FileName;
                treeSpecies.PicturePath = await FileManagement.UploadImage(memoryStream, treeSpecies.ImageName);
            }
            var command = new UpdateTreeSpeciesCommand();
            command.TreeSpecies = treeSpecies;
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "MVC Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetTreeSpeciesByIdQuery();
            query.Id = (int)id;
            var TreeSpecies = await _mediator.Send(query);
            if (TreeSpecies == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TreeSpecies>(TreeSpecies));
        }

        [Authorize(Roles = "MVC Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteTreeSpeciesCommand command = new DeleteTreeSpeciesCommand();
            command.Id = id;
            var query = new GetTreeSpeciesByIdQuery();
            query.Id = (int)id;
            var treeSpecies = await _mediator.Send(query);
            if (!String.IsNullOrEmpty(treeSpecies.PicturePath)&& !String.IsNullOrEmpty(treeSpecies.ImageName))
            {
                await FileManagement.DeleteImage(treeSpecies.ImageName);

            }
            if (!String.IsNullOrEmpty(treeSpecies.MaintenancePlan.FileName) && !String.IsNullOrEmpty(treeSpecies.MaintenancePlan.QRCodeName))
            {
                await FileManagement.DeletePDF(treeSpecies.MaintenancePlan.FileName);
                await FileManagement.DeleteImage(treeSpecies.MaintenancePlan.QRCodeName);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TreeSpeciesExists(int id)
        {
            if (id == null)
            {
                return false;
            }
            var query = new GetTreeSpeciesByIdQuery();
            query.Id = id;
            var TreeSpecies = await _mediator.Send(query);
            if (TreeSpecies != null)
            {
                return true;
            }
            return false;
        }
    }
}
