using MinesweeperApi.Exceptions;
using MinesweeperApi.Models;

namespace MinesweeperApi;

public static class Middlewares
{

    public static WebApplication UseMiddlewares(this WebApplication app)
    {
        app.Use(ExceptionMiddleware);
        return app;
    }

    private static async Task ExceptionMiddleware(HttpContext context, RequestDelegate next)
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
    }

}

