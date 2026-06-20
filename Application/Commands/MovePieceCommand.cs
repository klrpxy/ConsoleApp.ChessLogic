using ConsoleAppChessLogic.Domain.Board;

namespace ConsoleAppChessLogic.Application.Commands;

public sealed record MovePieceCommand(BoardPosition From, BoardPosition To);
