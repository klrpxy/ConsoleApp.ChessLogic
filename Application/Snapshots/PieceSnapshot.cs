using ConsoleAppChessLogic.Domain.Board;
using ConsoleAppChessLogic.Domain.Pieces;

namespace ConsoleAppChessLogic.Application.Snapshots;

public sealed record PieceSnapshot(
    PieceColor Color,
    PieceType Type,
    BoardPosition Position);
