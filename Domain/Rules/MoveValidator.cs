using ConsoleAppChessLogic.Domain.Board;
using ConsoleAppChessLogic.Domain.Pieces;
using ConsoleAppChessLogic.Domain.Pieces.Strategies;

namespace ConsoleAppChessLogic.Domain.Rules;

public sealed class MoveValidator {
    private readonly PieceMoveStrategyRegistry strategyRegistry;

    public MoveValidator(PieceMoveStrategyRegistry strategyRegistry) {
        this.strategyRegistry = strategyRegistry;
    }

    public bool IsLegalMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        PieceColor movingColor) {
        if (!from.IsInsideBoard || !to.IsInsideBoard || from == to) {
            return false;
        }

        var piece = board[from];
        if (piece is null || piece.Value.Color != movingColor) {
            return false;
        }

        var target = board[to];
        if (target is { } targetPiece && targetPiece.Color == movingColor) {
            return false;
        }

        if (!IsValidPieceMove(board, from, to, piece.Value)) {
            return false;
        }

        var simulatedBoard = board.Clone();
        simulatedBoard.Move(from, to);
        return !IsInCheck(simulatedBoard, movingColor);
    }

    public bool IsInCheck(ChessBoard board, PieceColor color) {
        var generalPosition = board.FindGeneral(color);
        if (generalPosition is null) {
            return true;
        }

        var attacker = OpponentOf(color);
        foreach (var (from, piece) in board.GetPieces(attacker)) {
            if (IsValidPieceMove(board, from, generalPosition.Value, piece)) {
                return true;
            }
        }

        return false;
    }

    public bool HasAnyLegalMove(ChessBoard board, PieceColor color) {
        foreach (var (from, _) in board.GetPieces(color)) {
            for (var x = 0; x < 9; x++) {
                for (var y = 0; y < 10; y++) {
                    if (IsLegalMove(board, from, new BoardPosition(x, y), color)) {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static PieceColor OpponentOf(PieceColor color) =>
        color == PieceColor.Red ? PieceColor.Black : PieceColor.Red;

    private bool IsValidPieceMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece) =>
        strategyRegistry.Get(piece.Type).IsValidMove(board, from, to, piece);
}
