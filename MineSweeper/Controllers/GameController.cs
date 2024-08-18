using Newtonsoft.Json;
using MineSweeper.Request;
using MineSweeper.Response;
using Microsoft.AspNetCore.Mvc;
using MineSweeper.Domain.Abstractions;

namespace MineSweeper.Controllers
{

    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Route("/new")]
        [HttpPost]
        public IActionResult NewGame(NewGameRequest request)
        {

            var game = _gameService.Create(request.height, request.width, request.mines_count);
            var response = new GameInfoResponse(game.Id.ToString(), game.Width, game.Height, game.MinesCount, game.IsCompleted, game.Field);
            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            return Ok(json);
        }
        [Route("/turn")]
        [HttpPost]
        public IActionResult GameTurn(GameTurnRequest request)
        {
            var game = _gameService.ExecuteGameTurn(request.game_id, request.row, request.col);
            var response = new GameInfoResponse(game.Id.ToString(), game.Width, game.Height, game.MinesCount, game.IsCompleted, game.Field);
            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            return Ok(json);
        }

    }
}
