using ConsoleAppChessLogic.Application.Events;
using ConsoleAppChessLogic.Domain.Pieces;

namespace ConsoleAppChessLogic.Presentation.Console.Common;

public static class GameEventFormatter {
    public static string Format(
        IGameEvent gameEvent,
        int columnOffset = 0) =>
        gameEvent switch {
            PieceMovedEvent e =>
                $"{FormatColor(e.Color)}方{FormatPiece(e.PieceType)}从" +
                $"({e.From.X + columnOffset},{e.From.Y})移动到" +
                $"({e.To.X + columnOffset},{e.To.Y})",
            PieceCapturedEvent e =>
                $"{FormatColor(e.Color)}方{FormatPiece(e.PieceType)}被吃",
            CheckEvent e =>
                $"{FormatColor(e.CheckedColor)}方被将军",
            GameLostEvent e =>
                $"{FormatColor(e.LosingColor)}方输了",
            _ => throw new ArgumentOutOfRangeException(nameof(gameEvent))
        };

    private static string FormatColor(PieceColor color) =>
        color == PieceColor.Red ? "红" : "黑";

    private static string FormatPiece(PieceType pieceType) =>
        pieceType switch {
            PieceType.General => "将",
            PieceType.Advisor => "士",
            PieceType.Elephant => "象",
            PieceType.Horse => "马",
            PieceType.Chariot => "车",
            PieceType.Cannon => "炮",
            PieceType.Soldier => "兵",
            _ => throw new ArgumentOutOfRangeException(nameof(pieceType))
        };
}
