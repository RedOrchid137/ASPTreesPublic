using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Domain;
using MediatR;

namespace ASP.groep.L.WebAPI.MiddleWare
{
    public class PostLoginMiddleWare
    {
        private readonly RequestDelegate _next;

        public PostLoginMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMediator mediator)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var email = context.User.Claims.Where(c => c.Type == "email")
                .Select(c => c.Value).FirstOrDefault();
                var query = new GetUserByEmailQuery();
                query.Email = email;
                var user = await mediator.Send(query);
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
                    await mediator.Send(command);
                }
            }
            await _next(context);
        }
    }
}
