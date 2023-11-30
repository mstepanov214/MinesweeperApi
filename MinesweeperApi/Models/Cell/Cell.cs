using System.Text.Json.Serialization;

using MinesweeperApi.JsonConverters;

namespace MinesweeperApi.Models;

[JsonConverter(typeof(JsonCellConverter))]
public abstract class Cell(char value)
{
    public char Value { get; } = value;

    public char DisplayedValue => Revealed ? Value : CellValue.Empty;

    public bool Revealed { get; protected set; }

    public void Reveal()
    {
        Revealed = true;
    }
    public bool IsMine => this is CellX;
}

