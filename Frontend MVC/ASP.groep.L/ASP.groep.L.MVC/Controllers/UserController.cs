using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Domain;
using Microsoft.AspNetCore.Mvc;
using ASP.groep.L.Application.CQRS.Commands;
using MediatR;
using AutoMapper;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.groep.L.Controllers
{
    [Authorize(Roles = "MVC Admin")]
    public class UserController : Controller
    {
        IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index(Role role)
        {
            GetAllUsersQuery query = new GetAllUsersQuery();
            IEnumerable<User> Users = await _mediator.Send(query);
            if (role == Role.Ionic_Employee)
            {
                return View("Employees",Users.Where<User>(u => u.Role == role));
            }
            return View("Planners", Users.Where<User>(u => u.Role == role));
        }

        [HttpGet]
        public IActionResult Create(Role role)
        {
            ViewData["CurRole"] = role;
            ViewData["Role"] = new SelectList(Enum.GetValues(typeof(Role)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            var command = new CreateUserCommand();
            command.User = user;
            _mediator.Send(command);
            return RedirectToAction(nameof(Index), new { role = user.Role });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetUserByIdQuery();
            query.Id = (int)id;
            var User = await _mediator.Send(query);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(int Id)
        {
            var command = new DeleteUserCommand();
            command.Id = Id;
            var query = new GetUserByIdQuery();
            query.Id = Id;
            var role = (await _mediator.Send(query)).Role;
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index), new { role=role });

        }

        public async Task<IActionResult>Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = new GetUserByIdQuery();
            query.Id = (int)id;
            var User = await _mediator.Send(query);
            if (User == null)
            {
                return NotFound();
            }
            ViewData["Role"] = new SelectList(Enum.GetValues(typeof(Role)));
            return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(User user)
        {
            var command = new UpdateUserCommand();
            command.User = user;
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index),new {role=user.Role});
        }
        private async Task<bool> UserExists(int id)
        {
            var query = new GetUserByIdQuery();
            query.Id = id;
            if (await _mediator.Send(query) != null)
            {
                return true;
            }
            return false;
        }
    }
}
