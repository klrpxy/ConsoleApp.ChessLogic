using ConsoleApp.ChessLogic.Domain.Board;
using ConsoleApp.ChessLogic.Domain.Pieces;

namespace ConsoleApp.ChessLogic.Application.Events;

public sealed record PieceMovedEvent(
    PieceColor Color,
    PieceType PieceType,
    BoardPosition From,
    BoardPosition To) : IGameEvent;
