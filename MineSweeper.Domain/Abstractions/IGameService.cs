using MineSweeper.Domain.Models;

namespace MineSweeper.Domain.Abstractions
{
    public interface IGameService
    {
        Game Create(int height, int width, int minesCount);
        Game ExecuteGameTurn(string gameId, int row, int col);
    }
}