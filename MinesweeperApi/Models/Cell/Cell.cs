using System.Text.Json.Serialization;

using MinesweeperApi.JsonConverters;

namespace MinesweeperApi.Models;

[JsonConverter(typeof(JsonCellConverter))]
public class Cell
{
    protected Cell(char value)
    {
        Value = value;
    }

    public static Cell FromJson(string json)
    {
        char value = json.Single();

        if (value == CellValue.X)
        {
            return new CellX();
        }
        if (value == CellValue.M)
        {
            return new CellM();
        }
        if (value == CellValue.Empty)
        {
            return new CellEmpty();
        }
        if (char.IsAsciiDigit(value))
        {
            return new CellDigit(value);
        }
        throw new ArgumentException("Unknown cell json value", nameof(json));
    }

    public char Value { get; }

    public char DisplayedValue => Revealed ? Value : CellValue.Empty;

    public bool Revealed { get; protected set; }

    public void Reveal()
    {
        Revealed = true;
    }
    public bool IsMine => this is CellX;
}

