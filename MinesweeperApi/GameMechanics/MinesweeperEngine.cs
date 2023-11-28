using MinesweeperApi.Exceptions;
using MinesweeperApi.Extensions;
using MinesweeperApi.Models;

namespace MinesweeperApi.Utils;

public class MinesweeperEngine
{
    private readonly Game _game;

    private List<List<Cell>> Field => _game.Field;

    private MinesweeperEngine(Game game)
    {
        _game = game;
    }

    public static MinesweeperEngine Init(Game game)
    {
        if (game.Completed)
        {
            throw new MinesweeperException("игра завершена");
        }

        return new MinesweeperEngine(game);
    }

    public Game PerformGameTurn(GameTurn turn)
    {
        ValidateGameTurn(turn);

        if (IsMine(turn.Row, turn.Col))
        {
            _game.Completed = true;
            RevealAll();
        }
        else
        {
            Reveal(turn.Row, turn.Col);

            if (IsVictory)
            {
                _game.Completed = true;
                DefuseMines();
            }
        }
        return _game;
    }

    private void ValidateGameTurn(GameTurn turn)
    {
        if (turn.Col < 0 || turn.Col > _game.Width)
        {
            throw new MinesweeperException($"колонка должна быть неотрицательной и менее ширины ${_game.Width}");
        }
        if (turn.Row < 0 || turn.Row > _game.Height)
        {
            throw new MinesweeperException($"ряд должен быть неотрицательным и менее высоты ${_game.Height}");
        }
        if (IsRevealed(turn.Row, turn.Col))
        {
            throw new MinesweeperException($"уже открытая ячейка");
        }
    }

    private void RevealAll()
    {
        for (int i = 0; i < Field.Count; i++)
        {
            for (int j = 0; j < Field[i].Count; j++)
            {
                Field[i][j].Reveal();
            }
        }
    }

    private void Reveal(int i, int j)
    {
        if (IsRevealed(i, j))
        {
            return;
        }

        Field[i][j].Reveal();

        if (Field[i][j].HasNoMinesAround)
        {
            foreach (var (x, y) in Field.GetNeighbourIndexes(i, j))
            {
                Reveal(x, y);
            }
        }
    }

    private bool IsVictory
    {
        get
        {
            return Field
                .SelectMany(row => row)
                .Count(cell => cell.IsMine && !cell.Revealed) == _game.MinesCount;
        }
    }

    private void DefuseMines()
    {
        for (int i = 0; i < Field.Count; i++)
        {
            for (int j = 0; j < Field[i].Count; j++)
            {
                if (IsMine(i, j))
                {
                    Field[i][j] = Cell.M;
                }
            }
        }
    }

    private bool IsRevealed(int i, int j)
    {
        return Field[i][j].Revealed;
    }

    private bool IsMine(int i, int j)
    {
        return Field[i][j].IsMine;
    }
}
