using ConsoleAppChessLogic.Domain.Board;
using ConsoleAppChessLogic.Domain.Pieces;

namespace ConsoleAppChessLogic.Domain.Game;

public sealed class GameState {
    public GameState(
        ChessBoard board,
        PieceColor currentTurn = PieceColor.Red,
        GameStatus status = GameStatus.Playing) {
        Board = board;
        CurrentTurn = currentTurn;
        Status = status;
    }

    public ChessBoard Board { get; internal set; }

    public PieceColor CurrentTurn { get; internal set; }

    public GameStatus Status { get; internal set; }
}
