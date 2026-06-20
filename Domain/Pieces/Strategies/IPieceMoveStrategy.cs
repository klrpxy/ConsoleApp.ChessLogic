using ConsoleApp.ChessLogic.Domain.Board;

namespace ConsoleApp.ChessLogic.Domain.Pieces.Strategies;

public interface IPieceMoveStrategy {
    bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece);
}
