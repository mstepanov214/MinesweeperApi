using System.Net.Mime;

using MinesweeperApi.GameMechanics;
using MinesweeperApi.Models;
using MinesweeperApi.Services;

namespace MinesweeperApi.Endpoints;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapPost("/new", CreateNewGame)
            .Accepts<CreateGameDto>(MediaTypeNames.Application.Json)
            .Produces<Game>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

        app.MapPost("/turn", PerformGameTurn)
            .Accepts<GameTurn>(MediaTypeNames.Application.Json)
            .Produces<Game>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

        return app;
    }

    private static IResult CreateNewGame(CreateGameDto dto, IGameStorage storage)
    {
        var game = storage.Create(dto);

        return Results.Ok(game);
    }

    private static IResult PerformGameTurn(GameTurn turn, IGameStorage storage)
    {
        var game = storage.GetById(turn.GameId);
        var engine = MinesweeperEngine.Init(game);
        var resultGame = engine.PickCell(turn.Row, turn.Col);

        if (resultGame.Completed)
        {
            Task.Delay(TimeSpan.FromMinutes(10))
                .ContinueWith(_ => storage.Remove(resultGame.GameId));
        }
        return Results.Ok(resultGame);
    }
}
