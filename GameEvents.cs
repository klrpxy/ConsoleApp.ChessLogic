namespace ConsoleAppChessLogic;

public interface IGameEvent;

public sealed record PieceMovedEvent(
    PieceColor Color,
    PieceType PieceType,
    BoardPosition From,
    BoardPosition To) : IGameEvent;

public sealed record PieceCapturedEvent(
    PieceColor Color,
    PieceType PieceType) : IGameEvent;

public sealed record CheckEvent(PieceColor CheckedColor) : IGameEvent;

public sealed record GameLostEvent(PieceColor LosingColor) : IGameEvent;
