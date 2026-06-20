using ConsoleAppChessLogic.Domain.Board;

namespace ConsoleAppChessLogic.Application.Commands;

public sealed record MoveChessIntent(
    BoardPosition From,
    BoardPosition To);
