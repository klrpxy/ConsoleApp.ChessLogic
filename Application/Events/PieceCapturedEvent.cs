using ConsoleApp.ChessLogic.Domain.Pieces;

namespace ConsoleApp.ChessLogic.Application.Events;

public sealed record PieceCapturedEvent(
    PieceColor Color,
    PieceType PieceType) : IGameEvent;
