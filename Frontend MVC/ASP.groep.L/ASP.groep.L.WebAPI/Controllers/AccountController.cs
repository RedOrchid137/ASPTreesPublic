using ASP.groep.L.Domain;
using ASP.groep.L.Models;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.WebAPI.Models;
using MediatR;
using Newtonsoft.Json;

namespace ASP.groep.L.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]

        public async Task Login()
        {
            var context = this.HttpContext;
            if (context.User.Identity.IsAuthenticated)
            {
                var email = context.User.Claims.Where(c => c.Type == "email")
                .Select(c => c.Value).FirstOrDefault();
                var query = new GetUserByEmailQuery();
                query.Email = email;
                var user = await _mediator.Send(query);
                if (user is null)
                {
                    var newUser = new User();
                    newUser.Email = email;
                    var name = context.User.Claims.Where(c => c.Type == "name")
                        .Select(c => c.Value).FirstOrDefault();
                    newUser.FirstName = name.Split(' ')[0];
                    newUser.LastName = "";

                    foreach (var item in new ArraySegment<string>(name.Split(' '), 1, name.Split(' ').Length - 1))
                    {
                        newUser.LastName += item + ' ';
                    }
                    var role = context.User.Claims.Where(c => c.Type == "role").ToString();

                    switch (role)
                    {
                        case ("MVC Admin"):
                            newUser.Role = Role.MVC_Admin;
                            break;
                        case ("MVC Planner"):
                            newUser.Role = Role.MVC_Planner;
                            newUser.PlannerWorkSchedules = new List<WorkSchedule>();
                            break;
                        default:
                            newUser.Role = Role.Ionic_Employee;
                            newUser.EmployeeWorkSchedules = new List<WorkSchedule>();
                            break;
                    }
                    var command = new CreateUserCommand();
                    command.User = newUser;
                    await _mediator.Send(command);
                }
            }
        }
    }
}
