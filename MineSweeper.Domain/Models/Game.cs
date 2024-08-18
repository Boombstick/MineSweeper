namespace MineSweeper.Domain.Models
{
    public class Game
    {
        public Guid Id { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int MinesCount { get; private set; }
        public Cell[,] Field { get; private set; }
        public bool IsCompleted { get; set; } = false;
        public int RevealedCells { get; set; } = 0;
        public Game(Guid id, Cell[,] field, int height, int width, int minesCount)
        {
            Height = height;
            Width = width;
            MinesCount = minesCount;
            Id = id;
            Field = field;
        }

    }
}
