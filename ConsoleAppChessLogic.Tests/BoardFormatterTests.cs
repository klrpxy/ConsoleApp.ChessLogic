namespace ConsoleAppChessLogic.Tests;

public sealed class BoardFormatterTests {
    [Fact]
    public void InitialBoard_ContainsCoordinatesChinesePiecesAndTurn() {
        var text = BoardFormatter.Format(new GameEngine().GetSnapshot());

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

        var text = BoardFormatter.Format(snapshot);

        Assert.Contains(expectedText, text);
        Assert.DoesNotContain("请输入：", text);
    }
}
