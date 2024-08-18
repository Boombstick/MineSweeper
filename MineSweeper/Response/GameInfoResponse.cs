using MineSweeper.Domain.Models;

namespace MineSweeper.Response
{
    public record GameInfoResponse(
        string game_id,
        int width,
        int height,
        int mines_count,
        bool completed,
        Cell[,] field);
}
