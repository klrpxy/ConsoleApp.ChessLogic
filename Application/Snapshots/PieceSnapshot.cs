using ConsoleApp.ChessLogic.Domain.Board;
using ConsoleApp.ChessLogic.Domain.Pieces;

namespace ConsoleApp.ChessLogic.Application.Snapshots;

public sealed record PieceSnapshot(
    PieceColor Color,
    PieceType Type,
    BoardPosition Position);
