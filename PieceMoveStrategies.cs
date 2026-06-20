namespace ConsoleAppChessLogic;

public interface IPieceMoveStrategy {
    bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece);
}

public sealed class PieceMoveStrategyRegistry {
    private readonly IReadOnlyDictionary<PieceType, IPieceMoveStrategy> strategies =
        new Dictionary<PieceType, IPieceMoveStrategy> {
            [PieceType.General] = new GeneralMoveStrategy(),
            [PieceType.Advisor] = new AdvisorMoveStrategy(),
            [PieceType.Elephant] = new ElephantMoveStrategy(),
            [PieceType.Horse] = new HorseMoveStrategy(),
            [PieceType.Chariot] = new ChariotMoveStrategy(),
            [PieceType.Cannon] = new CannonMoveStrategy(),
            [PieceType.Soldier] = new SoldierMoveStrategy()
        };

    public IPieceMoveStrategy Get(PieceType pieceType) => strategies[pieceType];
}

public sealed class GeneralMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece) {
        var target = board[to];
        var isFlyingCapture =
            target is { Type: PieceType.General } &&
            target.Value.Color != piece.Color &&
            from.X == to.X &&
            board.CountPiecesBetween(from, to) == 0;

        return isFlyingCapture ||
               (PieceMoveRules.IsInsidePalace(to, piece.Color) &&
                Math.Abs(to.X - from.X) + Math.Abs(to.Y - from.Y) == 1);
    }
}

public sealed class AdvisorMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece) =>
        PieceMoveRules.IsInsidePalace(to, piece.Color) &&
        Math.Abs(to.X - from.X) == 1 &&
        Math.Abs(to.Y - from.Y) == 1;
}

public sealed class ElephantMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece) {
        var dx = to.X - from.X;
        var dy = to.Y - from.Y;
        if (Math.Abs(dx) != 2 || Math.Abs(dy) != 2) {
            return false;
        }

        var staysOnOwnSide = piece.Color == PieceColor.Red ? to.Y >= 5 : to.Y <= 4;
        if (!staysOnOwnSide) {
            return false;
        }

        var eye = new BoardPosition(from.X + dx / 2, from.Y + dy / 2);
        return board[eye] is null;
    }
}

public sealed class HorseMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece) {
        var dx = to.X - from.X;
        var dy = to.Y - from.Y;
        var absX = Math.Abs(dx);
        var absY = Math.Abs(dy);
        if (!((absX == 1 && absY == 2) || (absX == 2 && absY == 1))) {
            return false;
        }

        var leg = absX == 2
            ? new BoardPosition(from.X + Math.Sign(dx), from.Y)
            : new BoardPosition(from.X, from.Y + Math.Sign(dy));

        return board[leg] is null;
    }
}

public sealed class ChariotMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece) =>
        PieceMoveRules.IsStraightMove(from, to) &&
        board.CountPiecesBetween(from, to) == 0;
}

public sealed class CannonMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece) {
        if (!PieceMoveRules.IsStraightMove(from, to)) {
            return false;
        }

        var piecesBetween = board.CountPiecesBetween(from, to);
        return board[to] is null
            ? piecesBetween == 0
            : piecesBetween == 1;
    }
}

public sealed class SoldierMoveStrategy : IPieceMoveStrategy {
    public bool IsValidMove(
        ChessBoard board,
        BoardPosition from,
        BoardPosition to,
        ChessPiece piece) {
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
