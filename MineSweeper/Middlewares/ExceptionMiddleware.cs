using MineSweeper.Domain.Exceptions;
using MineSweeper.Response;

namespace MineSweeper.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private const string serverError = "Произошла непредвиденная ошибка";
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (GameException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new ErrorResponse(ex.Message));
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorResponse(serverError));
            }

        }

    }
}
