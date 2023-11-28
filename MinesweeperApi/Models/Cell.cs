using System.Text.Json.Serialization;

using MinesweeperApi.JsonConverters;

namespace MinesweeperApi.Models;

[JsonConverter(typeof(JsonCellConverter))]
public struct Cell(char value)
{
    /// <summary>
    /// Empty cell
    /// </summary>
    public static readonly Cell Empty = new Cell(CellValue.Empty);

    /// <summary>
    /// Mine cell
    /// </summary>
    public static readonly Cell X = new Cell(CellValue.X);

    /// <summary>
    /// Mine cell after victory
    /// </summary>
    public static readonly Cell M = new Cell(CellValue.M);

    /// <summary>
    /// Creates Cell with digit
    /// </summary>
    public static Cell WithDigit(int value)
    {
        if (value < 0 || value > 8)
        {
            throw new ArgumentException("Invalid cell digit value", nameof(value));
        }
        return new Cell((char)(value + 48));
    }

    public char Value { get; set; } = value;

    public readonly char DisplayedValue => Revealed ? Value : CellValue.Empty;

    public readonly bool IsMine => Value == CellValue.X || Value == CellValue.M;

    public readonly bool HasNoMinesAround => Value == CellValue.Zero;

    public bool Revealed { get; private set; }

    public void Reveal()
    {
        Revealed = true;
    }
}

static file class CellValue
{
    public const char Empty = ' ';
    public const char Zero = '0';
    public const char X = 'X';
    public const char M = 'M';
}
