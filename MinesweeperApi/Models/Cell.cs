using System.Text.Json.Serialization;

using MinesweeperApi.JsonConverters;

namespace MinesweeperApi.Models;

[JsonConverter(typeof(JsonCellConverter))]
public class Cell
{
    internal Cell(char value)
    {
        Value = value;
    }

    public char Value { get; }

    public char DisplayedValue => Revealed ? Value : CellValue.Empty;

    public bool IsMine => this is CellX;

    public bool Revealed { get; private set; }

    public void Reveal()
    {
        Revealed = true;
    }
}

/// <summary>
/// Empty cell
/// </summary>
public class CellE : Cell
{
    public CellE() : base(CellValue.Empty) { }
}

/// <summary>
/// Mine cell
/// </summary>
public class CellX : Cell
{
    public CellX() : base(CellValue.X) { }
}

/// <summary>
/// Mine cell after victory
/// </summary>
public class CellM : Cell
{
    public CellM() : base(CellValue.M)
    {
        Reveal();
    }
}

/// <summary>
/// Cell with digit
/// </summary>
public class CellD : Cell
{
    public CellD(int digit) : base(ToChar(digit))
    {
        HasNoMinesAround = digit == 0;
    }

    private static char ToChar(int digit)
    {
        if (digit < 0 || digit > 8)
        {
            throw new ArgumentException("Invalid cell digit value", nameof(digit));
        }
        return (char)(digit + 48);
    }

    public bool HasNoMinesAround { get; }
}

static file class CellValue
{
    public const char Empty = ' ';
    public const char Zero = '0';
    public const char X = 'X';
    public const char M = 'M';
}
