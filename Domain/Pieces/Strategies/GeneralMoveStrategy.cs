using ConsoleAppChessLogic.Domain.Board;

namespace ConsoleAppChessLogic.Domain.Pieces.Strategies;

public sealed class GeneralMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(ChessBoard board, BoardPosition from, BoardPosition to, ChessPiece piece) {
        var target = board[to];
        var isFlyingCapture =
            target is { Type: PieceType.General } &&
            target.Value.Color != piece.Color &&
            from.X == to.X &&
            board.CountPiecesBetween(from, to) == 0;

        return isFlyingCapture ||
               (PieceMoveRules.IsInsidePalace(to, piece.Color) &&
                Math.Abs(to.X - from.X) + Math.Abs(to.Y - from.Y) == 1);
    }
}
