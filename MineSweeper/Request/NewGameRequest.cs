namespace MineSweeper.Request
{
    public record NewGameRequest(
        int height, 
        int width, 
        int mines_count);
}
