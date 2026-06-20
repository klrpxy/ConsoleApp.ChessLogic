using ConsoleApp.ChessLogic.Domain.Game;
using ConsoleApp.ChessLogic.Domain.Pieces;

namespace ConsoleApp.ChessLogic.Application.Snapshots;

public sealed record GameSnapshot(
    PieceColor CurrentTurn,
    GameStatus Status,
    IReadOnlyList<PieceSnapshot> Pieces);
