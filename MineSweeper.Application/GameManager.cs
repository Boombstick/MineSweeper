using MineSweeper.Domain.Exceptions;
using MineSweeper.Domain.Models;

namespace MineSweeper.Application
{
    public class GameManager
    {
        private Game _game;
        public Guid Id => _game.Id;
        private GameBuilder _builder;
        public GameManager(int fieldHeight, int fieldWidth, int fieldMinesCount)
        {
            _builder = new GameBuilder();
            _game = CreateNewGame(fieldHeight, fieldWidth, fieldMinesCount);
        }
        public Game CreateNewGame(int height, int width, int minesCount)
        {
            ValidateGameSettings(height, width, minesCount);
            _builder.CreateNewEmptyField(height, width);
            _builder.SetMines(minesCount);
            _builder.CountAdjacentMines();
            var game = _builder.GetGame();
            InstallGame(game);
            return game;
        }
        public Game GetGame() => _game;
        public Game PickCell(int row, int col)
        {
            if (_game.IsCompleted)
                throw new GameException("Игра уже завершена");
            
            if (CellIsRevealed(row, col))
                throw new GameException("Эта ячейка уже открыта");

            if (CellIsMine(row, col))
            {
                _game.IsCompleted = true;
                RevealAll();
            }
            else
            {
                Reveal(row, col);
                if (GameEndedWithAVictory())
                {
                    _game.IsCompleted = true;
                    RevealMines();
                }
            }
            return _game;
        }
        private void RevealAll()
        {
            for (int i = 0; i < _game.Height; i++)
                for (int j = 0; j < _game.Width; j++)
                    _game.Field[i, j].IsRevealed = true;

        }
        private void RevealMines()
        {
            for (int i = 0; i < _game.Height; i++)
                for (int j = 0; j < _game.Width; j++)
                    if (CellIsMine(i, j))
                    {
                        _game.Field[i, j].State = CellState.M;
                        _game.Field[i, j].IsRevealed = true;
                    }
        }
        private void Reveal(int row, int col)
        {
            if (CellIsRevealed(row, col))
                return;

            _game.Field[row, col].IsRevealed = true;
            _game.RevealedCells++;
            if (HasMinesAround(row, col))
                foreach (var (r, c) in _builder.GetAdjacentCells(row, col))
                    Reveal(r, c);
        }
        private void InstallGame(Game game) => _game = game;
        private bool GameEndedWithAVictory() => _game.RevealedCells == _game.Width * _game.Height - _game.MinesCount;
        private bool CellIsRevealed(int row, int col) => _game.Field[row, col].IsRevealed;
        private bool CellIsMine(int row, int col) => _game.Field[row, col].IsMine;
        private bool HasMinesAround(int row, int col) => _game.Field[row, col].State == CellState.Zero;


        private void ValidateGameSettings(int height, int width, int minesCount)
        {
            if (height > 30 || height < 2)
                throw new GameException("Высота поля должна быть не более 30 и не менее 2");

            if (width > 30 || width < 2)
                throw new GameException("Ширина поля должна быть не более 30 и не менее 2");

            var cellCount = height * width;
            if (minesCount >= cellCount || minesCount < 1)
                throw new GameException($"Количество мин должно быть меньше количества ячеек {cellCount},но не меньше 1");

        }
    }
}
