namespace ConsoleApp.ChessLogic.Tests.Presentation.Console.BoardView;

public sealed class BoardConsoleViewTests {
    [Fact]
    public void ShowInitial_PrintsBoard() {
        using var output = new StringWriter();
        var view = CreateView(output);

        view.ShowInitial(new GameEngine().GetSnapshot());

        Assert.Contains("1  2  3  4  5  6  7  8  9", output.ToString());
        Assert.Contains("当前回合：红方", output.ToString());
    }

    [Fact]
    public void ShowResult_PrintsExternalCoordinatesThenUpdatedBoard() {
        using var output = new StringWriter();
        var view = CreateView(output);
        var result = new GameResult(
            true,
            new IGameEvent[] {
                new PieceMovedEvent(
                    PieceColor.Red,
                    PieceType.Horse,
                    new BoardPosition(1, 9),
                    new BoardPosition(2, 7))
            });
        var snapshot = new GameSnapshot(
            PieceColor.Black,
            GameStatus.Playing,
            Array.Empty<PieceSnapshot>());

        view.ShowResult(result, snapshot);

        Assert.Contains(
            "红方马从(2,9)移动到(3,7)",
            output.ToString());
        Assert.Contains("当前回合：黑方", output.ToString());
    }

    [Fact]
    public void InvalidMove_DoesNotPrintBoardAgain() {
        using var output = new StringWriter();
        var view = CreateView(output);

        view.ShowResult(
            GameResult.Failed(),
            new GameEngine().GetSnapshot());

        Assert.Equal($"输入非法{Environment.NewLine}", output.ToString());
    }

    private static BoardConsoleView CreateView(StringWriter output) =>
        new(
            new StringReader(string.Empty),
            output,
            new BoardConsoleInputParser(),
            BoardConsoleTheme.Plain);
}
