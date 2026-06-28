using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace GI.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var response = JsonSerializer.Serialize(ex.Errors.Select(x => new { Message = x.ErrorMessage }));
                await context.Response.WriteAsync(response);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    InvalidOperationException => (int)HttpStatusCode.Conflict,
                    _ => (int)HttpStatusCode.InternalServerError
                };
                var errors = new List<object>();
                errors.Add(new { message = ex.Message });
                var response = JsonSerializer.Serialize(errors);
                await context.Response.WriteAsync(response);
            }
        }
    }
}