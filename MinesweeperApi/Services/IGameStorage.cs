using MinesweeperApi.Models;

namespace MinesweeperApi.Services;

public interface IGameStorage
{
    Game Create(CreateGameDto dto);

    Game GetById(Guid id);
}
