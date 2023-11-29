using MinesweeperApi.Exceptions;
using MinesweeperApi.GameMechanics;

namespace MinesweeperApi.Models;

public class Game
{
    public Guid GameId { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int MinesCount { get; set; }
    public bool Completed { get; set; }
    public List<List<Cell>> Field { get; set; }

    public Game(int width, int height, int minesCount)
    {
        Width = width;
        Height = height;
        MinesCount = minesCount;

        ValidateGame();

        Field = _fieldBuilder
            .GenerateField(Width, Height)
            .SetMines(MinesCount)
            .SetDigits()
            .Build();
    }

    private void ValidateGame()
    {
        if (Width > 30 || Width < 2)
        {
            throw new MinesweeperException("ширина поля должна быть не менее 2 и не более 30");
        }

        if (Height > 30 || Height < 2)
        {
            throw new MinesweeperException("высота поля должна быть не менее 2 и не более 30");
        }

        int cellCount = Width * Height;
        if (MinesCount >= cellCount || MinesCount < 1)
        {
            throw new MinesweeperException($"количество мин должно быть не менее 1 и строго менее количества ячеек {cellCount}");
        }
    }

    private static readonly MinefieldBuilder _fieldBuilder = new();
}

