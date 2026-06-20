using ConsoleAppChessLogic.Domain.Game;
using ConsoleAppChessLogic.Domain.Pieces;

namespace ConsoleAppChessLogic.Application.Snapshots;

public sealed record GameSnapshot(
    PieceColor CurrentTurn,
    GameStatus Status,
    IReadOnlyList<PieceSnapshot> Pieces);
