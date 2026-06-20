namespace ConsoleAppChessLogic.Tests.Presentation.Console;

public sealed class BoardFormatterTests {
    [Fact]
    public void DefaultTheme_UsesConsoleBackgroundAndWhiteBlackPieces() {
        var theme = BoardConsoleTheme.Default;

        Assert.Null(theme.Background);
        Assert.Equal(new AnsiColor(255, 255, 255), theme.Text);
        Assert.Equal(new AnsiColor(255, 255, 255), theme.BlackPieces);
        Assert.Equal(new AnsiColor(255, 60, 60), theme.RedPieces);
    }

    [Fact]
    public void DefaultTheme_DoesNotWriteBackgroundColorCode() {
        var text = BoardFormatter.Format(
            new GameEngine().GetSnapshot(),
            BoardConsoleTheme.Default);

        Assert.DoesNotContain("\u001b[48;2;", text);
        Assert.StartsWith("\u001b[38;2;255;255;255m", text);
    }

    [Fact]
    public void ColoredBoard_UsesConfiguredColorsAndResetsStyle() {
        var theme = new BoardConsoleTheme(
            Background: new AnsiColor(1, 2, 3),
            Text: new AnsiColor(4, 5, 6),
            Numbers: new AnsiColor(7, 8, 9),
            EmptyPositions: new AnsiColor(10, 11, 12),
            River: new AnsiColor(13, 14, 15),
            RedPieces: new AnsiColor(16, 17, 18),
            BlackPieces: new AnsiColor(19, 20, 21));

        var text = BoardFormatter.Format(
            new GameEngine().GetSnapshot(),
            theme);

        Assert.StartsWith(
            "\u001b[48;2;1;2;3m\u001b[38;2;4;5;6m",
            text);
        Assert.Contains("\u001b[38;2;7;8;9m1", text);
        Assert.Contains("\u001b[38;2;10;11;12m．", text);
        Assert.Contains("\u001b[38;2;13;14;15m楚河　汉界", text);
        Assert.Contains("\u001b[38;2;16;17;18m帅", text);
        Assert.Contains("\u001b[38;2;19;20;21m将", text);
        Assert.EndsWith("\u001b[0m", text);
    }

    [Fact]
    public void InitialBoard_ContainsCoordinatesChinesePiecesAndTurn() {
        var text = BoardFormatter.Format(
            new GameEngine().GetSnapshot(),
            BoardConsoleTheme.Plain);

        Assert.Contains("1  2  3  4  5  6  7  8  9", text);
        Assert.Contains(" 0   車 馬 象 士 将 士 象 馬 車", text);
        Assert.Contains(" 9   车 马 相 仕 帅 仕 相 马 车", text);
        Assert.Contains("楚河　汉界", text);
        Assert.Contains("当前回合：红方", text);
        Assert.Contains("请输入：起点列 起点行 终点列 终点行", text);
    }

    [Theory]
    [InlineData(GameStatus.RedWon, "游戏结束：红方胜利")]
    [InlineData(GameStatus.BlackWon, "游戏结束：黑方胜利")]
    public void FinishedGame_ShowsWinner(
        GameStatus status,
        string expectedText) {
        var snapshot = new GameSnapshot(
            PieceColor.Red,
            status,
            Array.Empty<PieceSnapshot>());

        var text = BoardFormatter.Format(
            snapshot,
            BoardConsoleTheme.Plain);

        Assert.Contains(expectedText, text);
        Assert.DoesNotContain("请输入：", text);
    }
}
