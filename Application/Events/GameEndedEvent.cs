using ConsoleApp.ChessLogic.Domain.Pieces;

namespace ConsoleApp.ChessLogic.Application.Events;

public sealed record GameEndedEvent(PieceColor Winner) : IGameEvent;
