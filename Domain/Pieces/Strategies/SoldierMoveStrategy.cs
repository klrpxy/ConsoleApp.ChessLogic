using ConsoleAppChessLogic.Domain.Board;

namespace ConsoleAppChessLogic.Domain.Pieces.Strategies;

public sealed class SoldierMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(ChessBoard board, BoardPosition from, BoardPosition to, ChessPiece piece) {
        var dx = to.X - from.X;
        var dy = to.Y - from.Y;
        var forward = piece.Color == PieceColor.Red ? -1 : 1;
        if (dx == 0 && dy == forward) {
            return true;
        }

        var crossedRiver = piece.Color == PieceColor.Red ? from.Y <= 4 : from.Y >= 5;
        return crossedRiver && Math.Abs(dx) == 1 && dy == 0;
    }
}
