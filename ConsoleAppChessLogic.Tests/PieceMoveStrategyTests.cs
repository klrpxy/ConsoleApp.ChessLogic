namespace ConsoleAppChessLogic.Tests;

public sealed class PieceMoveStrategyTests {
    [Fact]
    public void Horse_CanMoveInLShape() {
        var strategy = new HorseMoveStrategy();
        var board = ChessBoard.Create();

        var result = strategy.IsValidMove(
            board,
            new BoardPosition(1, 9),
            new BoardPosition(2, 7),
            TestBoard.Red(PieceType.Horse));

        Assert.True(result);
    }

    [Fact]
    public void Horse_CannotMoveInSquareShape() {
        var strategy = new HorseMoveStrategy();
        var board = ChessBoard.Create();

        var result = strategy.IsValidMove(
            board,
            new BoardPosition(1, 9),
            new BoardPosition(3, 7),
            TestBoard.Red(PieceType.Horse));

        Assert.False(result);
    }

    [Fact]
    public void Horse_CannotMoveWhenLegIsBlocked() {
        var strategy = new HorseMoveStrategy();
        var board = ChessBoard.Create(
            (new BoardPosition(1, 8), TestBoard.Red(PieceType.Soldier)));

        var result = strategy.IsValidMove(
            board,
            new BoardPosition(1, 9),
            new BoardPosition(2, 7),
            TestBoard.Red(PieceType.Horse));

        Assert.False(result);
    }

    [Fact]
    public void Elephant_CannotCrossRiver() {
        var strategy = new ElephantMoveStrategy();
        var board = ChessBoard.Create();

        var result = strategy.IsValidMove(
            board,
            new BoardPosition(2, 6),
            new BoardPosition(4, 4),
            TestBoard.Red(PieceType.Elephant));

        Assert.False(result);
    }

    [Fact]
    public void Elephant_CannotMoveWhenEyeIsBlocked() {
        var strategy = new ElephantMoveStrategy();
        var board = ChessBoard.Create(
            (new BoardPosition(3, 8), TestBoard.Red(PieceType.Soldier)));

        var result = strategy.IsValidMove(
            board,
            new BoardPosition(2, 9),
            new BoardPosition(4, 7),
            TestBoard.Red(PieceType.Elephant));

        Assert.False(result);
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(2, false)]
    public void Cannon_CaptureRequiresExactlyOneScreen(int screenCount, bool expected) {
        var pieces = new List<(BoardPosition Position, ChessPiece Piece)> {
            (new BoardPosition(0, 0), TestBoard.Black(PieceType.Horse))
        };

        if (screenCount >= 1) {
            pieces.Add((new BoardPosition(0, 3), TestBoard.Red(PieceType.Soldier)));
        }

        if (screenCount >= 2) {
            pieces.Add((new BoardPosition(0, 5), TestBoard.Black(PieceType.Soldier)));
        }

        var board = ChessBoard.Create(pieces.ToArray());
        var strategy = new CannonMoveStrategy();

        var result = strategy.IsValidMove(
            board,
            new BoardPosition(0, 7),
            new BoardPosition(0, 0),
            TestBoard.Red(PieceType.Cannon));

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Soldier_CannotMoveSidewaysBeforeCrossingRiver() {
        var strategy = new SoldierMoveStrategy();

        var result = strategy.IsValidMove(
            ChessBoard.Create(),
            new BoardPosition(0, 6),
            new BoardPosition(1, 6),
            TestBoard.Red(PieceType.Soldier));

        Assert.False(result);
    }

    [Fact]
    public void Soldier_CanMoveSidewaysAfterCrossingRiver() {
        var strategy = new SoldierMoveStrategy();

        var result = strategy.IsValidMove(
            ChessBoard.Create(),
            new BoardPosition(0, 4),
            new BoardPosition(1, 4),
            TestBoard.Red(PieceType.Soldier));

        Assert.True(result);
    }
}
