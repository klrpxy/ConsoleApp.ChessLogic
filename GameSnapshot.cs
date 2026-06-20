namespace ConsoleAppChessLogic;

public sealed record GameSnapshot(
    PieceColor CurrentTurn,
    GameStatus Status,
    IReadOnlyList<PieceSnapshot> Pieces);

public sealed record PieceSnapshot(
    PieceColor Color,
    PieceType Type,
    BoardPosition Position);
