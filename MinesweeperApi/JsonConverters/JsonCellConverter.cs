using System.Text.Json;
using System.Text.Json.Serialization;

using MinesweeperApi.Models;

namespace MinesweeperApi.JsonConverters;

public class JsonCellConverter : JsonConverter<Cell>
{
    public override Cell? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Cell cell, JsonSerializerOptions options)
    {
        writer.WriteStringValue(cell.DisplayedValue.ToString());
    }
}
