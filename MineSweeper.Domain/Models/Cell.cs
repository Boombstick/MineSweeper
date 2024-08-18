using MineSweeper.JsonConverters;
using Newtonsoft.Json;

namespace MineSweeper.Domain.Models
{
    [JsonConverter(typeof(JsonCellConverter))]
    public struct Cell
    {
        public char DisplayedValue => IsRevealed ? State : CellState.EmptyCell;
        [JsonIgnore]
        public char State { get; set; }
        [JsonIgnore]
        public bool IsMine => State == CellState.X;
        [JsonIgnore]
        public bool IsRevealed { get; set; }
        public Cell()
        {

        }
    }
}
