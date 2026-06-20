using ConsoleAppChessLogic.Domain.Pieces;

namespace ConsoleAppChessLogic.Application.Events;

public sealed record PieceCapturedEvent(
    PieceColor Color,
    PieceType PieceType) : IGameEvent;
