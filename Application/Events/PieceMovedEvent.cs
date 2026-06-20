using ConsoleAppChessLogic.Domain.Board;
using ConsoleAppChessLogic.Domain.Pieces;

namespace ConsoleAppChessLogic.Application.Events;

public sealed record PieceMovedEvent(
    PieceColor Color,
    PieceType PieceType,
    BoardPosition From,
    BoardPosition To) : IGameEvent;
