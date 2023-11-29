using MinesweeperApi.Extensions;
using MinesweeperApi.Models;

namespace MinesweeperApi.GameMechanics;

public class MinefieldBuilder
{
    private List<List<Cell>> _field = [];
    private int _width = 0;
    private int _height = 0;

    public List<List<Cell>> Build()
    {
        List<List<Cell>> result = _field;

        Reset();

        return result;
    }

    public void Reset()
    {
        _field = [];
        _width = 0;
        _height = 0;
    }

    public MinefieldBuilder GenerateField(int width, int height)
    {
        _field = new List<List<Cell>>(height);

        while (_field.Count < height)
        {
            _field.Add(new List<Cell>(Enumerable.Repeat(new CellEmpty(), width)));
        }

        _width = width;
        _height = height;

        return this;
    }

    public MinefieldBuilder SetMines(int minesCount)
    {
        ThrowIfFieldNotGenerated();

        int total = _height * _width;
        var random = new Random();
        var emptyCellIndexes = Enumerable.Range(0, total).ToList();

        while (emptyCellIndexes.Count + minesCount != total)
        {
            int rnd = random.Next(emptyCellIndexes.Count);
            int index = emptyCellIndexes[rnd];

            _field[index / _width][index % _width] = new CellX();
            emptyCellIndexes.RemoveAt(rnd);
        }

        return this;
    }

    public MinefieldBuilder SetDigits()
    {
        ThrowIfFieldNotGenerated();

        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                if (_field[i][j].IsMine)
                {
                    continue;
                }
                var nmCount = CountNeigbourMines(i, j);
                _field[i][j] = new CellDigit(nmCount);
            }
        }
        return this;
    }

    private int CountNeigbourMines(int i, int j)
    {
        return _field.GetNeighbourIndexes(i, j).Count(n => _field[n.x][n.y].IsMine);
    }

    private void ThrowIfFieldNotGenerated()
    {
        if (_field.TotalCount() == 0)
        {
            throw new Exception($"Field not generated. Call {nameof(GenerateField)} first.");
        }
    }
}
