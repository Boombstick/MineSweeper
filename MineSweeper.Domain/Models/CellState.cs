namespace MineSweeper.Domain.Models
{
    public static class CellState
    {
        public const char EmptyCell = ' ';
        public const char M = 'M';
        public const char X = 'X';
        public const char Zero = '0';

        public static char GetCharFromMinesCount(int minesCount) => (char)(minesCount + 48);
    }
}

