using ASP.groep.L.Domain;
using ASP.groep.L.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.FileManagement;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MediatR;

namespace ASP.groep.L.Controllers
{
    [Authorize(Roles = "MVC Admin,MVC Planner")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        public HomeController(ILogger<HomeController> logger,IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }



        public async Task <IActionResult> Index()
        {
            await Init();
            return View();
        }      
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task Init()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var email = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/email")
                    .Select(c => c.Value).FirstOrDefault();
                var name = HttpContext.User.Claims.Where(c => c.Type == "name")
                    .Select(c => c.Value).FirstOrDefault();
                var query = new GetUserByEmailQuery();
                query.Email = email;
                var user = await _mediator.Send(query);
                if (user is null)
                {

                    var newUser = new User();
                    newUser.Email = email;

                    newUser.FirstName = name.Split(' ')[0];
                    newUser.LastName = "";

                    foreach (var item in new ArraySegment<string>(name.Split(' '), 1, name.Split(' ').Length - 1))
                    {
                        newUser.LastName += item + ' ';
                    }
                    var role = HttpContext.User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").FirstOrDefault().Value.ToString();

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
                    _mediator.Send(command);
                }
            }
        }    }
}