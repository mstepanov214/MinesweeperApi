using MinesweeperApi.Exceptions;
using MinesweeperApi.Models;

namespace MinesweeperApi.Services;

public class GameStorage : IGameStorage
{
    private readonly Dictionary<Guid, Game> _games = [];

    public Game Create(CreateGameDto dto)
    {
        var game = new Game(
            width: dto.Width,
            height: dto.Height,
            minesCount: dto.MinesCount
        );

        game.GameId = Guid.NewGuid();

        _games.Add(game.GameId, game);

        return game;
    }

    public Game GetById(Guid id)
    {
        return _games.GetValueOrDefault(id) ?? throw new MinesweeperException($"нет игры с идентификатором {id}");
    }

    public bool Remove(Guid id)
    {
        return _games.Remove(id);
    }
}
