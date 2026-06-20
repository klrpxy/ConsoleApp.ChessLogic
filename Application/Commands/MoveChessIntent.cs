using ConsoleApp.ChessLogic.Domain.Board;

namespace ConsoleApp.ChessLogic.Application.Commands;

public sealed record MoveChessIntent(
    BoardPosition From,
    BoardPosition To);
