using ConsoleAppChessLogic.Domain.Board;

namespace ConsoleAppChessLogic.Domain.Pieces.Strategies;

public interface IPieceMoveStrategy {
    bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece);
}
