using MinesweeperApi.Exceptions;
using MinesweeperApi.Models;

namespace MinesweeperApi.Middleware;

public static class ExceptionMiddleware
{
    public static WebApplication UseExceptionMiddleware(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            try
            {
                await next(context);
            }
            catch (MinesweeperException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new ErrorResponse { Error = ex.Message });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorResponse { Error = "Произошла непредвиденная ошибка" });
            }
        });
        return app;
    }
}

