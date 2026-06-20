using ConsoleApp.ChessLogic.Domain.Pieces;

namespace ConsoleApp.ChessLogic.Application.Events;

public sealed record CheckEvent(PieceColor CheckedColor) : IGameEvent;
