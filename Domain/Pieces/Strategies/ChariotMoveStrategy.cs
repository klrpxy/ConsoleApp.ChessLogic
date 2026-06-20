using ConsoleApp.ChessLogic.Domain.Board;

namespace ConsoleApp.ChessLogic.Domain.Pieces.Strategies;

public sealed class ChariotMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(ChessBoard board, BoardPosition from, BoardPosition to, ChessPiece piece) =>
        PieceMoveRules.IsStraightMove(from, to) &&
        board.CountPiecesBetween(from, to) == 0;
}
