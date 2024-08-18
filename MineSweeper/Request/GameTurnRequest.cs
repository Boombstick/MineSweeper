namespace MineSweeper.Request
{
    public record GameTurnRequest(
        string game_id,
        int col,
        int row);
}
