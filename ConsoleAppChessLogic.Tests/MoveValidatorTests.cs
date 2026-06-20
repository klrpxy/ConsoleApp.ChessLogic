namespace ConsoleAppChessLogic.Tests;

public sealed class MoveValidatorTests {
    private readonly MoveValidator validator =
        new(new PieceMoveStrategyRegistry());

    [Fact]
    public void CannotMoveOpponentsPiece() {
        var board = ChessBoard.CreateInitial();

        var result = validator.IsLegalMove(
            board,
            new BoardPosition(1, 0),
            new BoardPosition(2, 2),
            PieceColor.Red);

        Assert.False(result);
    }

    [Fact]
    public void CannotCaptureOwnPiece() {
        var board = ChessBoard.CreateInitial();

        var result = validator.IsLegalMove(
            board,
            new BoardPosition(0, 9),
            new BoardPosition(0, 6),
            PieceColor.Red);

        Assert.False(result);
    }

    [Fact]
    public void MoveThatExposesGeneralsToEachOther_IsIllegal() {
        var board = ChessBoard.Create(
            (new BoardPosition(4, 9), TestBoard.Red(PieceType.General)),
            (new BoardPosition(4, 0), TestBoard.Black(PieceType.General)),
            (new BoardPosition(4, 5), TestBoard.Red(PieceType.Chariot)));

        var result = validator.IsLegalMove(
            board,
            new BoardPosition(4, 5),
            new BoardPosition(3, 5),
            PieceColor.Red);

        Assert.False(result);
    }

    [Fact]
    public void PlayerInCheck_MustResolveCheck() {
        var board = ChessBoard.Create(
            (new BoardPosition(4, 9), TestBoard.Red(PieceType.General)),
            (new BoardPosition(3, 0), TestBoard.Black(PieceType.General)),
            (new BoardPosition(4, 5), TestBoard.Black(PieceType.Chariot)),
            (new BoardPosition(0, 9), TestBoard.Red(PieceType.Chariot)));

        var result = validator.IsLegalMove(
            board,
            new BoardPosition(0, 9),
            new BoardPosition(0, 8),
            PieceColor.Red);

        Assert.False(result);
    }
}
