using MineSweeper.Domain.Models;
using Newtonsoft.Json;

namespace MineSweeper.JsonConverters
{
    public class JsonCellConverter : JsonConverter<Cell>
    {
        public override Cell ReadJson(JsonReader reader, Type objectType, Cell existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override void WriteJson(JsonWriter writer, Cell cell, JsonSerializer serializer)
        {
            writer.WriteValue(cell.DisplayedValue.ToString());
        }
    }
}
