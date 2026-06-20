using ConsoleAppChessLogic.Domain.Pieces;

namespace ConsoleAppChessLogic.Application.Events;

public sealed record GameEndedEvent(PieceColor Winner) : IGameEvent;
