using ConsoleApp.ChessLogic.Domain.Board;

namespace ConsoleApp.ChessLogic.Domain.Pieces.Strategies;

public sealed class AdvisorMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(ChessBoard board, BoardPosition from, BoardPosition to, ChessPiece piece) =>
        PieceMoveRules.IsInsidePalace(to, piece.Color) &&
        Math.Abs(to.X - from.X) == 1 &&
        Math.Abs(to.Y - from.Y) == 1;
}
