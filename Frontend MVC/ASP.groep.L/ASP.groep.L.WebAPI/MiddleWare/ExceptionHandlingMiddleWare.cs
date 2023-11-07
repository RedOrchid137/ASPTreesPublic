
using ASP.groep.L.Infrastructure.Exceptions;
using FluentValidation;
using System.Text.Json;

namespace ASP.groep.L.WebAPI.MiddleWare
{ 

    public class ExceptionHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        
        public ExceptionHandlingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = new ErrorResponseInfo();
                response.Message = ex.Message;
                switch (ex)
                {
                    case ValidationException:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case RelationNotFoundException:
                        response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                }
                context.Response.StatusCode = response.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }

    }
}
