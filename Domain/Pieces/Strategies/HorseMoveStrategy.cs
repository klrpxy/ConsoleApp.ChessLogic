using ConsoleApp.ChessLogic.Domain.Board;

namespace ConsoleApp.ChessLogic.Domain.Pieces.Strategies;

public sealed class HorseMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(ChessBoard board, BoardPosition from, BoardPosition to, ChessPiece piece) {
        var dx = to.X - from.X;
        var dy = to.Y - from.Y;
        var absX = Math.Abs(dx);
        var absY = Math.Abs(dy);
        if (!((absX == 1 && absY == 2) || (absX == 2 && absY == 1))) {
            return false;
        }

        var leg = absX == 2
            ? new BoardPosition(from.X + Math.Sign(dx), from.Y)
            : new BoardPosition(from.X, from.Y + Math.Sign(dy));
        return board[leg] is null;
    }
}
