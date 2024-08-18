using MineSweeper.Domain.Exceptions;
using MineSweeper.Domain.Models;

namespace MineSweeper.Application
{
    public class GameBuilder
    {
        private Cell[,]? _field;
        private int _height = 0;
        private int _width = 0;
        private int _minesCount = 0;
        public Game GetGame()
        {
            ThrowExceptionIfFieldIsNull();
            return new Game(
                    Guid.NewGuid(),
                   _field!,
                   _height,
                   _width,
                   _minesCount);
        }
        public void CreateNewEmptyField(int height, int width)
        {
            _field = new Cell[height, width];
            _height = height;
            _width = width;
        }
        public void SetMines(int minesCount)
        {
            _minesCount = minesCount;
            ThrowExceptionIfFieldIsNull();
            var random = new Random();
            var emptyCells = Enumerable.Range(0, _height * _width).ToList();

            while (minesCount > 0)
            {
                var randomNumber = random.Next(emptyCells.Count);
                var index = emptyCells[randomNumber];

                _field[index / _height, index % _width].State = CellState.X;
                minesCount--;
                emptyCells.RemoveAt(randomNumber);
            }
        }
        public void CountAdjacentMines()
        {
            ThrowExceptionIfFieldIsNull();
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (_field[i, j].IsMine)
                        continue;

                    _field[i, j].State = CellState.GetCharFromMinesCount(
                        GetAdjacentCells(i, j)
                        .Count(p => _field[p.Row, p.Col].State == CellState.X));
                }
            }
        }
        public IEnumerable<(int Row, int Col)> GetAdjacentCells(int height, int width)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0) && CheckBounds(height + i, width + j))
                        yield return new(height + i, width + j);
                }
            }
        }
        private bool CheckBounds(int row, int col)
        {
            return row >= 0 && row < _height && col >= 0 && col < _width;
        }
        private void ThrowExceptionIfFieldIsNull()
        {
            if (_field is null)
                throw new Exception($"Поле для игры не создано");
        }
    }
}
