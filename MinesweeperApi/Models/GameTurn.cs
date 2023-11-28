namespace MinesweeperApi.Models;

public class GameTurn
{
    public Guid GameId { get; set; }
    public int Col { get; set; }
    public int Row { get; set; }
}
