using ConsoleAppChessLogic.Domain.Pieces;

namespace ConsoleAppChessLogic.Application.Events;

public sealed record CheckEvent(PieceColor CheckedColor) : IGameEvent;
