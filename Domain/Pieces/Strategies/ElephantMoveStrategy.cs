using ConsoleApp.ChessLogic.Domain.Board;

namespace ConsoleApp.ChessLogic.Domain.Pieces.Strategies;

public sealed class ElephantMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(ChessBoard board, BoardPosition from, BoardPosition to, ChessPiece piece) {
        var dx = to.X - from.X;
        var dy = to.Y - from.Y;
        if (Math.Abs(dx) != 2 || Math.Abs(dy) != 2) {
            return false;
        }

        var staysOnOwnSide = piece.Color == PieceColor.Red ? to.Y >= 5 : to.Y <= 4;
        if (!staysOnOwnSide) {
            return false;
        }

        return board[new BoardPosition(from.X + dx / 2, from.Y + dy / 2)] is null;
    }
}
