namespace ConsoleApp.ChessLogic.Tests.Presentation.Console;

public sealed class ConsoleMoveInputParserTests {
    private readonly ConsoleMoveInputParser parser = new();

    [Fact]
    public void ValidCoordinates_ReturnMoveIntent() {
        var result = parser.Parse("1 9 2 7");

        Assert.Equal(ViewInputKind.Move, result.Kind);
        Assert.NotNull(result.Intent);
        Assert.Equal(
            new BoardPosition(1, 9),
            result.Intent.From);
        Assert.Equal(
            new BoardPosition(2, 7),
            result.Intent.To);
    }

    [Theory]
    [InlineData("quit")]
    [InlineData("QUIT")]
    public void QuitCommand_ReturnQuit(string input) {
        var result = parser.Parse(input);

        Assert.Equal(ViewInputKind.Quit, result.Kind);
        Assert.Null(result.Intent);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1 2 3")]
    [InlineData("a b c d")]
    public void MalformedInput_ReturnInvalid(string input) {
        var result = parser.Parse(input);

        Assert.Equal(ViewInputKind.Invalid, result.Kind);
        Assert.Null(result.Intent);
    }
}
