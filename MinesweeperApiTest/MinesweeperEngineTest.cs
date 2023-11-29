using MinesweeperApi.Extensions;
using MinesweeperApi.GameMechanics;
using MinesweeperApi.Models;

namespace MinesweeperApiTest;

[TestClass]
public class MinesweeperEngineTest
{
    private static Game _game = null!;

    [TestInitialize()]
    public void Initialize()
    {
        _game = new Game(width: 4, height: 4, minesCount: 2);

        _game.Field = [
            [new CellX(), new CellD(2), new CellD(1), new CellD(1)],
            [new CellD(1), new CellD(2), new CellX(), new CellD(1)],
            [new CellD(0), new CellD(1), new CellD(1), new CellD(1)],
            [new CellD(0), new CellD(0), new CellD(0), new CellD(0)]
        ];
    }

    [TestMethod]
    public void MinesweeperEngine_Lose()
    {
        var engine = MinesweeperEngine.Init(_game);

        var turn = new GameTurn()
        {
            Row = 0,
            Col = 0,
        };

        var resultGame = engine.PerformGameTurn(turn);

        Assert.IsTrue(resultGame.Completed);
        Assert.IsTrue(resultGame.Field.SelectMany(row => row).All(cell => cell.Revealed));
    }

    [TestMethod]
    public void MinesweeperEngine_Win()
    {
        var engine = MinesweeperEngine.Init(_game);

        var result1 = engine.PerformGameTurn(new GameTurn()
        {
            Row = 3,
            Col = 1
        });
        Assert.IsFalse(result1.Completed);

        var result2 = engine.PerformGameTurn(new GameTurn()
        {
            Row = 0,
            Col = 1
        });
        Assert.IsFalse(result2.Completed);

        var result3 = engine.PerformGameTurn(new GameTurn()
        {
            Row = 0,
            Col = 2
        });
        Assert.IsFalse(result3.Completed);

        var result4 = engine.PerformGameTurn(new GameTurn()
        {
            Row = 0,
            Col = 3
        });
        Assert.IsFalse(result4.Completed);

        var result5 = engine.PerformGameTurn(new GameTurn()
        {
            Row = 1,
            Col = 3
        });
        Assert.IsTrue(result5.Completed);
        Assert.AreEqual(result5.Field.TotalCount(cell => cell.Revealed), _game.Width * _game.Height);
        Assert.AreEqual(result5.Field.TotalCount(cell => cell is CellM), _game.MinesCount);
    }
}
