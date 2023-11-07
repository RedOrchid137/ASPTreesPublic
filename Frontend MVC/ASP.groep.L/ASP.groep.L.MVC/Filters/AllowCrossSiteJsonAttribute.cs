using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.groep.L.Filters
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Response != null)
                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            base.OnActionExecuted(context);
        }
    }
}
