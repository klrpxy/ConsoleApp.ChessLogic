using ConsoleAppChessLogic.Domain.Board;

namespace ConsoleAppChessLogic.Domain.Pieces.Strategies;

internal static class PieceMoveRules {
    public static bool IsStraightMove(BoardPosition from, BoardPosition to) =>
        from.X == to.X || from.Y == to.Y;

    public static bool IsInsidePalace(BoardPosition position, PieceColor color) {
        if (position.X is < 3 or > 5) {
            return false;
        }

        return color == PieceColor.Red
            ? position.Y is >= 7 and <= 9
            : position.Y is >= 0 and <= 2;
    }
}
