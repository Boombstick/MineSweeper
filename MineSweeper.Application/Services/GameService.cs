using Microsoft.Extensions.Caching.Memory;
using MineSweeper.Domain.Abstractions;
using MineSweeper.Domain.Exceptions;
using MineSweeper.Domain.Models;

namespace MineSweeper.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IMemoryCache _cache;
        public GameService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Game Create(int height, int width, int minesCount)
        {
            var gameManager = new GameManager(height, width, minesCount);
            _cache.Set(gameManager.Id.ToString(), gameManager);
            return gameManager.GetGame();

        }
        public Game ExecuteGameTurn(string gameId, int row, int col)
        {
            if (!_cache.TryGetValue(gameId, out GameManager manager))
                throw new GameException("Игра не найдена");
            var game = manager.PickCell(row, col);
            _cache.Set(manager.Id.ToString(), manager);
            return game;
        }

    }
}
