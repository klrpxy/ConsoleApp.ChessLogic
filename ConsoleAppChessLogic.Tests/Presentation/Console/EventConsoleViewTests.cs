namespace ConsoleAppChessLogic.Tests.Presentation.Console;

public sealed class EventConsoleViewTests {
    [Fact]
    public void ShowResult_PrintsEventsInOrder() {
        using var output = new StringWriter();
        var view = new EventConsoleView(
            new StringReader(string.Empty),
            output,
            new ConsoleMoveInputParser());
        var result = new GameResult(
            MoveResult.Success,
            new IGameEvent[] {
                new PieceMovedEvent(
                    PieceColor.Red,
                    PieceType.Cannon,
                    new BoardPosition(1, 7),
                    new BoardPosition(1, 0)),
                new PieceCapturedEvent(
                    PieceColor.Black,
                    PieceType.Horse)
            });

        view.ShowResult(result, EmptySnapshot());

        Assert.Equal(
            $"红方炮从(1,7)移动到(1,0){Environment.NewLine}" +
            $"黑方马被吃{Environment.NewLine}",
            output.ToString());
    }

    [Fact]
    public void ShowResult_PrintsInvalidInputForRejectedMove() {
        using var output = new StringWriter();
        var view = new EventConsoleView(
            new StringReader(string.Empty),
            output,
            new ConsoleMoveInputParser());

        view.ShowResult(
            GameResult.WithoutEvents(MoveResult.InvalidInput),
            EmptySnapshot());

        Assert.Equal($"输入非法{Environment.NewLine}", output.ToString());
    }

    private static GameSnapshot EmptySnapshot() =>
        new(PieceColor.Red, GameStatus.Playing, Array.Empty<PieceSnapshot>());
}
