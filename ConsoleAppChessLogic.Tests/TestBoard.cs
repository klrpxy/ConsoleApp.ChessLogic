namespace ConsoleAppChessLogic.Tests;

internal static class TestBoard {
    public static ChessPiece Red(PieceType type) => new(PieceColor.Red, type);

    public static ChessPiece Black(PieceType type) => new(PieceColor.Black, type);

    public static ChessBoard WithGenerals(
        params (BoardPosition Position, ChessPiece Piece)[] additionalPieces) {
        var pieces = new List<(BoardPosition Position, ChessPiece Piece)> {
            (new BoardPosition(4, 9), Red(PieceType.General)),
            (new BoardPosition(4, 0), Black(PieceType.General)),
            (new BoardPosition(4, 5), Red(PieceType.Soldier))
        };
        pieces.AddRange(additionalPieces);
        return ChessBoard.Create(pieces.ToArray());
    }
}
