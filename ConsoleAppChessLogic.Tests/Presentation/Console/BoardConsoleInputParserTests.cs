namespace ConsoleAppChessLogic.Tests.Presentation.Console;

public sealed class BoardConsoleInputParserTests {
    private readonly BoardConsoleInputParser parser = new();

    [Fact]
    public void VisibleColumnsOneToNine_AreConvertedToInternalColumns() {
        var result = parser.Parse("2 9 3 7");

        Assert.Equal(ViewInputKind.Move, result.Kind);
        Assert.NotNull(result.Intent);
        Assert.Equal(
            new BoardPosition(1, 9),
            result.Intent.Command.From);
        Assert.Equal(
            new BoardPosition(2, 7),
            result.Intent.Command.To);
    }

    [Theory]
    [InlineData("0 9 3 7")]
    [InlineData("10 9 3 7")]
    [InlineData("2 -1 3 7")]
    [InlineData("2 9 3 10")]
    public void CoordinatesOutsideVisibleBoard_ReturnInvalid(string input) {
        var result = parser.Parse(input);

        Assert.Equal(ViewInputKind.Invalid, result.Kind);
        Assert.Null(result.Intent);
    }
}
