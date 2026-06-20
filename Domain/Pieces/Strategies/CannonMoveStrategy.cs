using ConsoleApp.ChessLogic.Domain.Board;

namespace ConsoleApp.ChessLogic.Domain.Pieces.Strategies;

public sealed class CannonMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(ChessBoard board, BoardPosition from, BoardPosition to, ChessPiece piece) {
        if (!PieceMoveRules.IsStraightMove(from, to)) {
            return false;
        }

        var piecesBetween = board.CountPiecesBetween(from, to);
        return board[to] is null ? piecesBetween == 0 : piecesBetween == 1;
    }
}
