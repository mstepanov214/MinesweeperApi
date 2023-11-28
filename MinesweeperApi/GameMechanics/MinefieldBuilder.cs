using MinesweeperApi.Extensions;
using MinesweeperApi.Models;

namespace MinesweeperApi.GameMechanics;

public class MinefieldBuilder
{
    private List<List<Cell>> _field = [];

    public void Reset()
    {
        _field = [];
    }

    public List<List<Cell>> Build()
    {
        List<List<Cell>> result = _field;

        Reset();

        return result;
    }

    public MinefieldBuilder GenerateField(int width, int height)
    {
        _field = new List<List<Cell>>(height);

        while (_field.Count < height)
        {
            _field.Add(new List<Cell>(Enumerable.Repeat(Cell.Empty, width)));
        }

        return this;
    }

    public MinefieldBuilder SetMines(int minesCount)
    {
        int height = _field.Count;
        int width = _field.First().Count;
        int total = height * width;

        var random = new Random();
        var emptyCellIndexes = Enumerable.Range(0, total).ToList();

        while (emptyCellIndexes.Count + minesCount != total)
        {
            int rnd = random.Next(emptyCellIndexes.Count);
            int index = emptyCellIndexes[rnd];
            _field[index / width][index % height] = Cell.X;
            emptyCellIndexes.RemoveAt(rnd);
        }

        return this;
    }

    public MinefieldBuilder SetDigits()
    {
        for (int i = 0; i < _field.Count; i++)
        {
            for (int j = 0; j < _field[i].Count; j++)
            {
                if (_field[i][j].IsMine)
                {
                    continue;
                }
                var nmCount = CountNeigbourMines(i, j);
                _field[i][j] = Cell.WithDigit(nmCount);
            }
        }
        return this;
    }

    private int CountNeigbourMines(int i, int j)
    {
        return _field.ValidNeighbours(i, j).Count(n => _field[n.x][n.y].IsMine);
    }
}
